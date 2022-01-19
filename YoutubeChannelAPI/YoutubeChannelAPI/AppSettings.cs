using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubeChannelAPI
{
    public class AppSettings
    {
        public const string SectionName = "AppSettings";

        public string IV { get; set; }

        public string Key { get; set; }
    }
}
