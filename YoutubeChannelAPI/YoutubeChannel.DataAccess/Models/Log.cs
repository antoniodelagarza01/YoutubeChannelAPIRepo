using System;
using System.Collections.Generic;

#nullable disable

namespace YoutubeChannelAPI.DataAccess.Models
{
    public partial class Log
    {
        public int LogId { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime LogDate { get; set; }
    }
}
