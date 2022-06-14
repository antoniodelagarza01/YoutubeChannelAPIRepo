using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeChannelAPI.DataAccess.Models;


namespace YoutubeChannelAPI
{
    public static class Mapper
    {
        public static YoutubeChannelAPI.Models.Admin Map(Admin admin){

            return new YoutubeChannelAPI.Models.Admin
            {
                AdminId = admin.AdminId,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                Password = admin.Password

            };
        }

        public static Admin Map(YoutubeChannelAPI.Models.Admin admin)
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

        public static YoutubeChannelAPI.Models.Video Map(Video video)
        {
            return new YoutubeChannelAPI.Models.Video
            {
                VideoId = video.VideoId,
                Title = video.Title,
                UploadDate = video.UploadDate,
                Media = video.Media
            };
        }

        public static Video Map(YoutubeChannelAPI.Models.Video video)
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
