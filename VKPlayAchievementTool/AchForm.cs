using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VKPlayAchievementTool
{
    public partial class AchForm : Form
    {
        public string GcId { get; set; }

        ImageList AchievementIcons;

        HttpAchievement.Item[] GlobalList;

        List<MemoryStream> IconStreams;

        HttpClient MyHttpClient;

        public AchForm(HttpClient cl)
        {
            InitializeComponent();
            MyHttpClient = cl;
            IconStreams = new List<MemoryStream>();
            AchievementIcons = new ImageList();
            AchievementIcons.ImageSize = new Size(32, 32);
            AchievementIcons.ColorDepth = ColorDepth.Depth32Bit;
            MainListView.SmallImageList = AchievementIcons;
            MainListView.LargeImageList = AchievementIcons;
        }

        public void FreeAchievementData()
        {
            MainListView.BeginUpdate();
            MainListView.Items.Clear();
            foreach (Image img in AchievementIcons.Images)
            {
                img?.Dispose();
            }
            foreach (var s in IconStreams)
            {
                s?.Dispose();
            }
            AchievementIcons.Images.Clear();
            MainListView.EndUpdate();
        }

        void ReinitializeAchievementData(Tuple<HttpAchievement, Dictionary<string, byte[]>> data)
        {
            FreeAchievementData();
            MainListView.BeginUpdate();
            var list = data.Item1.items;
            var icons = data.Item2;
            GlobalList = list;
            foreach (var ach in list)
            {
                var lvi = new ListViewItem();
                lvi.Tag = ach;

                if (icons.TryGetValue(ach.picture, out var raw))
                {
                    var ms = new MemoryStream(raw, false);
                    var img = Image.FromStream(ms);
                    IconStreams.Add(ms);
                    AchievementIcons.Images.Add(img);
                    var imgIndex = AchievementIcons.Images.Count - 1;
                    lvi.ImageIndex = imgIndex;
                }

                lvi.SubItems.Add(ach.name).Tag = ach;

                if (ach.max_progress == 0)
                {
                    lvi.SubItems.Add(ach.is_completed ? "Открыто" : "Не открыто").Tag = ach;
                }
                else
                {
                    lvi.SubItems.Add($"{ach.progress} из {ach.max_progress}").Tag = ach;
                }

                lvi.SubItems.Add(ach.descr).Tag = ach;
                lvi.SubItems.Add(ach.apiname).Tag = ach;

                string hideText;
                if (!ach.is_hidden && !ach.is_hidden_until_get)
                    hideText = "Нет";
                else if (ach.is_hidden && ach.is_hidden_until_get)
                    hideText = "Полностью (условие + список)";
                else if (ach.is_hidden && !ach.is_hidden_until_get)
                    hideText = "Только условие";
                else
                    hideText = "Только из списка";

                lvi.SubItems.Add(hideText).Tag = ach;

                MainListView.Items.Add(lvi);
            }
            MainListView.EndUpdate();
            MainListView.Enabled = true;
            UnlockAllButton.Enabled = true;
            ResetAllButton.Enabled = true;
        }

        public void InitializeAchievementData()
        {
            LoadBackgroundWorker.RunWorkerAsync(GcId);
            MainListView.Enabled = false;
            UnlockAllButton.Enabled = false;
            ResetAllButton.Enabled = false;
            //var list = DummyProcess.Execute(GcId);
            //ReinitializeAchievementData(list);
        }

        private void StoreStats(bool bUnlockAll)
        {
            var statsData = new HttpStoreStats.Builder();
            foreach (var ach in GlobalList)
            {
                // поставить текущий прогресс в max_progress
                // и completed в true
                // если max_progress 0, то progress и так должен быть 0...
                if (bUnlockAll)
                {
                    statsData.Add(ach.ach_id, ach.max_progress, true);
                }
                else
                {
                    statsData.Add(ach.ach_id, 0, false);
                }
            }

            var result = statsData.Finalize();
            MainListView.Enabled = false;
            UnlockAllButton.Enabled = false;
            ResetAllButton.Enabled = false;
            StoreBackgroundWorker.RunWorkerAsync(result);
        }

        private void UnlockAllButton_Click(object sender, EventArgs e)
        {
            StoreStats(true);
        }

        private void ResetAllButton_Click(object sender, EventArgs e)
        {
            StoreStats(false);
        }

        private void LoadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var gcId = (string)e.Argument;
            var url = $"https://api.vkplay.ru/steam/ISteamUserStats/GetAchievements/v1/?gc_id={gcId}&format=gc";
            using (var jsonStream = MyHttpClient.GetStreamAsync(url).GetAwaiter().GetResult())
            {
                var data = JsonSerializer.DeserializeAsync<HttpAchievement>(jsonStream).GetAwaiter().GetResult();
                var iconCache = new Dictionary<string, byte[]>();

                Array.Sort(data.items); // according to the priority
                foreach (var i in data.items)
                {
                    if (!string.IsNullOrEmpty(i.picture))
                        iconCache[i.picture] = MyHttpClient.GetByteArrayAsync(i.picture).GetAwaiter().GetResult();

                    if (!string.IsNullOrEmpty(i.picture_locked))
                        iconCache[i.picture_locked] = MyHttpClient.GetByteArrayAsync(i.picture_locked).GetAwaiter().GetResult();
                }

                e.Result = Tuple.Create(data, iconCache);
            }
        }

        private void LoadBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ReinitializeAchievementData((Tuple<HttpAchievement, Dictionary<string, byte[]>>)e.Result);
        }

        private void StoreBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var data = (HttpStoreStats[])e.Argument;
            // serialize on the background thread for more efficiency lolz
            var json = JsonSerializer.Serialize(data);

            var url = "https://api.vkplay.ru/steam/ISteamUserStats/SetAchievements/v1/";

            using (var content = new MultipartFormDataContent())
            using (var gcIdContent = new StringContent(GcId))
            using (var formatContent = new StringContent("gc"))
            using (var dataContent = new StringContent(json))
            {
                content.Add(gcIdContent, "gc_id");
                content.Add(formatContent, "format");
                content.Add(dataContent, "data");
                MyHttpClient.PostAsync(url, content).GetAwaiter().GetResult()?.Dispose();
            }
        }

        private void StoreBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InitializeAchievementData();
        }

        private void AchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            FreeAchievementData();
            AchievementIcons?.Dispose();
        }

        private ListViewItem chosenItem;

        private void MainListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (MainListView.GetItemAt(e.X, e.Y) is ListViewItem item)
                {
                    chosenItem = item;
                    AchContextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void SingleStoreStats(long ach, int progress, bool comp)
        {
            var statsData = new HttpStoreStats.Builder();
            foreach (var i in GlobalList)
            {
                if (i.ach_id != ach)
                    statsData.Add(i.ach_id, i.progress, i.is_completed);
                else
                    statsData.Add(ach, progress, comp);
            }
            
            var result = statsData.Finalize();
            MainListView.Enabled = false;
            UnlockAllButton.Enabled = false;
            ResetAllButton.Enabled = false;
            StoreBackgroundWorker.RunWorkerAsync(result);
        }

        private void SetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chosenItem == null)
                return;
            var ach = (HttpAchievement.Item)chosenItem.Tag;
            if (ach == null)
                return;

            var txtalready = $"{ach.name} уже разблокировано";
            if (ach.is_completed)
            {
                MessageBox.Show(this, txtalready, "Здесь могла бы быть ваша реклама", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var txt = $"Разблокировать достижение {ach.name}?";
            var res = MessageBox.Show(this, txt, "Здесь могла бы быть ваша реклама", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res != DialogResult.Yes)
                return;

            SingleStoreStats(ach.ach_id, ach.max_progress, true);
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chosenItem == null)
                return;
            var ach = (HttpAchievement.Item)chosenItem.Tag;
            if (ach == null)
                return;

            var txtalready = $"{ach.name} и так не разблокировано";
            if (!ach.is_completed)
            {
                MessageBox.Show(this, txtalready, "Здесь могла бы быть ваша реклама", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var txt = $"Сбросить достижение {ach.name}?";
            var res = MessageBox.Show(this, txt, "Здесь могла бы быть ваша реклама", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res != DialogResult.Yes)
                return;

            SingleStoreStats(ach.ach_id, 0, false);
        }

        private void SetProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chosenItem == null)
                return;
            var ach = (HttpAchievement.Item)chosenItem.Tag;
            if (ach == null)
                return;

            if (ach.max_progress <= 0)
            {
                MessageBox.Show(this, "Невозможно установить прогресс у ачивки без прогресса", "Ачивка не стата", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int progressValue;
            using (var dlg = new ProgressDialog())
            {
                dlg.ProgressNumericUpDown.Maximum = ach.max_progress;
                var res = dlg.ShowDialog(this);
                if (res != DialogResult.Yes)
                    return;
                progressValue = Convert.ToInt32(dlg.ProgressNumericUpDown.Value);
            }

            SingleStoreStats(ach.ach_id, progressValue, progressValue == ach.max_progress);
        }
    }
}
