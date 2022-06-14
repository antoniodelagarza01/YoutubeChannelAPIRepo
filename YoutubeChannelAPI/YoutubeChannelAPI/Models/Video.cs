using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubeChannelAPI.Models
{
    public class Video
    {
        public int VideoId { get; set; }
        public string Title { get; set; }
        public DateTime UploadDate { get; set; }
        public byte[] Media { get; set; }
    }
}
