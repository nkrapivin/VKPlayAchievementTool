using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKPlayAchievementTool
{
    // https://api.vkplay.ru/steam/ISteamUserStats/GetAchievements/v1/?gc_id=0.2028253&format=gc
    internal class HttpAchievement
    {
        public static readonly Encoding UTF8NoBOM = new UTF8Encoding(false, true);

        public class Item : IComparable
        {
            public long ach_id { get; set; }
            public string data { get; set; }
            public int progress { get; set; }
            public bool is_completed { get; set; }
            public long date { get; set; }
            public string name { get; set; }
            public string descr { get; set; }
            public long game { get; set; }
            public string picture { get; set; }
            public string picture_locked { get; set; }
            public int priority { get; set; }
            public bool is_hidden { get; set; }
            public bool is_hidden_until_get { get; set; }
            public int max_progress { get; set; }
            public bool is_published { get; set; }
            public string apiname { get; set; }

            public int CompareTo(object obj)
            {
                return priority.CompareTo(((Item)obj).priority);
            }
        }

        public string status { get; set; }
        public Item[] items { get; set; }
    }
}
