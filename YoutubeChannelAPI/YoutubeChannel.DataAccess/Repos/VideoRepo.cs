using System;
using System.Collections.Generic;
using System.Text;
using YoutubeChannel.DataAccess.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YoutubeChannel.DataAccess.Repos
{
    public class VideoRepo : IRepo<Video>
    {
        private readonly YoutubeChannelDBContext context;

        public VideoRepo(YoutubeChannelDBContext context)
        {
            this.context = context;
        }
        public bool Delete(int id)
        {
            int result = Convert.ToInt32(context.Videos.FromSqlRaw("YT.sp_deleteVideo_videos {0}", id));
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Video> Get()
        {
            return context.Videos.ToList();
        }

        public Video GetOne(int id)
        {
             return context.Videos.FirstOrDefault(x => x.VideoId == id);
            
        }

        public bool Post(Video item)
        {
            int result = Convert.ToInt32(context.Videos.FromSqlRaw("YT.sp_InsertVideo_videos {0}, {1}", item.Title, item.Media));
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public bool Put(Video item)
        {
            int result = Convert.ToInt32(context.Videos.FromSqlRaw("YT.sp_updateVideo_videos {0}", item.Title));
            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}
