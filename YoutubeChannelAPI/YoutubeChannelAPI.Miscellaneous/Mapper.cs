using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeChannel.DataAccess.Models;


namespace YoutubeChannelAPI
{
    public class Mapper
    {
        public YoutubeChannelAPI.Models.Admin Map(Admin admin){
            return new YoutubeChannelAPI.Models.Admin
            {
                AdminId = admin.AdminId,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                Password = admin.Password

            };
        }

        public Admin Map(YoutubeChannelAPI.Models.Admin admin)
        {
            return new Admin
            {
                AdminId = admin.AdminId,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                Password = admin.Password
            };
        }

        public YoutubeChannelAPI.Models.Video Map(Video video)
        {
            return new YoutubeChannelAPI.Models.Video
            {
                VideoId = video.VideoId,
                Title = video.Title,
                UploadDate = video.UploadDate,
                Media = video.Media
            };
        }

        public Video Map(YoutubeChannelAPI.Models.Video video)
        {
            return new Video
            {
                VideoId = video.VideoId,
                Title = video.Title,
                UploadDate = video.UploadDate,
                Media = video.Media
            };
        }
    }
}
