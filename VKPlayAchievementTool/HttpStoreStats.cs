using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKPlayAchievementTool
{
    // WebServiceTApiAppPortal.log :)
    internal class HttpStoreStats
    {
        public long ach_id { get; set; }
        public int progress { get; set; }
        public int completed { get; set; }

        public class Builder
        {
            readonly List<HttpStoreStats> items = new List<HttpStoreStats>();

            public Builder Add(long ach_id, int progress, bool completed)
            {
                items.Add(new HttpStoreStats()
                {
                    ach_id = ach_id,
                    progress = progress,
                    completed = completed ? 1 : 0
                });
                return this;
            }

            public HttpStoreStats[] Finalize()
            {
                var array = items.ToArray();
                items.Clear();
                return array;
            }
        }
    }
}
