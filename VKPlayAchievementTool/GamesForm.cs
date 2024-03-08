using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VKPlayAchievementTool
{
    public partial class GamesForm : Form
    {
        Dictionary<string, Dictionary<string, string>> GCIni;
        HttpClient SharedHttpClient;
        HttpClientHandler SharedHttpClientHandler;
        CookieContainer SharedCookieContainer;
        ImageList GamesImageList;

        public void InitializeUserInfo()
        {
            SharedHttpClientHandler = new HttpClientHandler();
            SharedCookieContainer = new CookieContainer();

            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var iniPath = Path.Combine(folder, "GameCenter", "GameCenter.ini");
            var lines = File.ReadAllLines(iniPath);
            var ini = IniFile.ParseFrom(lines);
            GCIni = ini;

            var userNick = ini["Main"]["CurrentUserNick"];
            var userId = ini["Main"]["MyComUserUid"];
            var avatarPath = ini["Main"]["CurrentUserAvatarFileName"];

            UserNameLabel.Text = userNick;
            UserIdLabel.Text = $"ID: {userId}";

            // don't lock the image file, lol
            using (var imageFile = Image.FromFile(avatarPath))
                AvatarPictureBox.Image = new Bitmap(imageFile);

            var gcIdsToFetch = new List<string>();
            foreach (var kvps in ini["GamePersIds"])
            {
                var key = kvps.Key;
                var gcId = key.Substring(0, key.IndexOf('_'));
                gcIdsToFetch.Add(gcId);
            }

            string encryptedKey;
            var keyPath = Path.Combine(folder, "GameCenter", "Cache", "Chrome", "LocalPrefs.json");
            var keyBytes = File.ReadAllBytes(keyPath);
            using (var ms = new MemoryStream(keyBytes))
            using (var json = JsonDocument.Parse(ms))
            {
                encryptedKey = json.RootElement.GetProperty("os_crypt").GetProperty("encrypted_key").GetString();
            }

            var binKey = Convert.FromBase64String(encryptedKey).Skip(5).ToArray();
            var decryptedKey = ProtectedData.Unprotect(binKey, null, DataProtectionScope.CurrentUser);

            var cookiesDbPath = Path.Combine(folder, "GameCenter", "Cache", "Chrome", "Network", "Cookies");
            var connString = $"Data Source={cookiesDbPath};Version=3;";
            using (var conn = new SQLiteConnection(connString).OpenAndReturn())
            using (var cmd = new SQLiteCommand("SELECT * FROM cookies", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var hostKey = (string)reader["host_key"];
                    var name = (string)reader["name"];
                    var encryptedValue = (byte[])reader["encrypted_value"];
                    var path = (string)reader["path"];
                    var expiresUtc = (long)reader["expires_utc"];
                    var isSecure = (long)reader["is_secure"];
                    var isHttpOnly = (long)reader["is_httponly"];

                    var nonce = new byte[12];
                    var ciphertext = new byte[encryptedValue.Length - 3 - nonce.Length];
                    Array.Copy(encryptedValue, 3, nonce, 0, nonce.Length);
                    Array.Copy(encryptedValue, 3 + nonce.Length, ciphertext, 0, ciphertext.Length);
                    var cipher = new GcmBlockCipher(new AesEngine());
                    var parameters = new AeadParameters(new KeyParameter(decryptedKey), 128, nonce, null);
                    cipher.Init(false, parameters);
                    var plainBytes = new byte[cipher.GetOutputSize(ciphertext.Length)];
                    var length = cipher.ProcessBytes(ciphertext, 0, ciphertext.Length, plainBytes, 0);
                    cipher.DoFinal(plainBytes, length);
                    var value = HttpAchievement.UTF8NoBOM.GetString(plainBytes).TrimEnd('\r', '\n', '\0');

                    var cookie = new Cookie();
                    cookie.Domain = hostKey;
                    cookie.Name = name;
                    cookie.Value = value;
                    cookie.Path = path;
                    cookie.Expires = DateTime.FromFileTimeUtc(expiresUtc * 10);
                    cookie.Secure = isSecure != 0;
                    cookie.HttpOnly = isHttpOnly != 0;
                    SharedCookieContainer.Add(cookie);
                }
            }

            SharedHttpClientHandler.CookieContainer = SharedCookieContainer;
            SharedHttpClient = new HttpClient(SharedHttpClientHandler);
            // set up httpclient for vk play requests
            SharedHttpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.84 Downloader/17670 MyComGameCenter/1767 Safari/537.36");
            SharedHttpClient.DefaultRequestHeaders.Add("Sec-Downloader", "1767");

            GamesImageList = new ImageList();
            GamesImageList.ColorDepth = ColorDepth.Depth32Bit;
            GamesImageList.ImageSize = new Size(630 / 5, 380 / 5);
            GamesListView.SmallImageList = GamesImageList;
            GamesListView.LargeImageList = GamesImageList;
            GamesListView.Enabled = false;
            GamesBackgroundWorker.RunWorkerAsync(gcIdsToFetch);
        }

        public GamesForm()
        {
            InitializeComponent();
            InitializeUserInfo();
        }

        class GameDescriptor
        {
            public string gcId, gameName;
            public Bitmap gameIcon;
        }

        private void ParseGameDescriptor(List<GameDescriptor> gd)
        {
            GamesListView.BeginUpdate();
            foreach (var desc in gd)
            {
                GamesImageList.Images.Add(desc.gameIcon);
                var imageIndex = GamesImageList.Images.Count - 1;

                var lvi = new ListViewItem(desc.gameName, imageIndex);
                lvi.Tag = desc;
                GamesListView.Items.Add(lvi);
            }
            GamesListView.EndUpdate();
            GamesListView.Enabled = true;
        }

        private async Task<List<GameDescriptor>> FetchGames(List<string> gcIds)
        {
            var games = new List<GameDescriptor>();

            foreach (var gcId in gcIds)
            {
                var url = $"https://api.vkplay.ru/play/games/get/?full_cost_info=1&check_register=1&gc_id={gcId}";
                GameJson game;
                try
                {
                    using (var jsonStream = await SharedHttpClient.GetStreamAsync(url))
                    {
                        game = await JsonSerializer.DeserializeAsync<GameJson>(jsonStream);
                    }
                }
                catch
                {
                    Debug.WriteLine($"Failed JSON for GC ID {gcId}");
                    continue;
                }

                // only care about client games...
                if (!game.type.Contains("download") || game.is_browser)
                    continue;
                
                byte[] picture;
                try
                {
                    picture = await SharedHttpClient.GetByteArrayAsync(game.picture);
                }
                catch
                {
                    Debug.WriteLine($"Failed PNG for GC ID {gcId}");
                    continue;
                }

                Bitmap bm;
                using (var ms = new MemoryStream(picture))
                using (var img = Image.FromStream(ms))
                    bm = new Bitmap(img);

                var desc = new GameDescriptor();
                desc.gcId = gcId;
                desc.gameName = game.name;
                desc.gameIcon = bm;
                games.Add(desc);
            }

            return games;
        }

        private void GamesBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var gcIds = (List<string>)e.Argument;
            // this is safe to do since we are on a background thread...
            e.Result = FetchGames(gcIds).GetAwaiter().GetResult();
        }

        private void GamesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ParseGameDescriptor((List<GameDescriptor>)e.Result);
        }

        private void OpenGameForm(GameDescriptor gd)
        {
            using (var dialog = new AchForm(SharedHttpClient))
            {
                dialog.GcId = gd.gcId;
                dialog.InitializeAchievementData();
                dialog.ShowDialog(this);
            }
        }

        private void GamesListView_ItemActivate(object sender, EventArgs e)
        {
            var selItems = GamesListView.SelectedItems;
            if (selItems.Count <= 0)
                return;

            OpenGameForm((GameDescriptor)selItems[0].Tag);
        }
    }
}
