using System;
using System.Collections.Generic;

#nullable disable

namespace YoutubeChannel.DataAccess.Models
{
    public partial class Video
    {
        public int VideoId { get; set; }
        public string Title { get; set; }
        public DateTime UploadDate { get; set; }
        public byte[] Media { get; set; }
    }
}
