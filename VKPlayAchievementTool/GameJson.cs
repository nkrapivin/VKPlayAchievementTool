using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKPlayAchievementTool
{
    internal class GameJson
    {
        public string name { get; set; }
        public string picture { get; set; }
        public string[] feature_flags { get; set; }
        public bool is_browser { get; set; }
        public string type { get; set; }
    }
}
