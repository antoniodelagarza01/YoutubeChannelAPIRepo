using System;
using System.Collections.Generic;
using System.Text;
using YoutubeChannelAPI.DataAccess.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace YoutubeChannelAPI.DataAccess.Repos
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
            //input parameter
            SqlParameter videoId = new SqlParameter("@videoId", id);

            //output parameter
            SqlParameter returnCode = new SqlParameter("@returnCode", SqlDbType.Int);
            returnCode.Direction = ParameterDirection.Output;
          

            var result = context.Database.ExecuteSqlRaw("exec @returnCode = YT.sp_deleteVideo_videos @videoId",returnCode, videoId);

            if (Convert.ToInt32(returnCode.Value) == 1)
            {
                context.SaveChanges();
                return true;
            }

            return false;

        }

        public IEnumerable<Video> Get()
        {
            var result = context.Videos;
            return result.ToList();
        }

        public Video GetOne(int id)
        {
            var result = context.Videos.FirstOrDefault(x => x.VideoId == id);
            return result;

        }

        public bool Post(Video item)
        {
            //input parameters
            SqlParameter title = new SqlParameter("@title", item.Title);
            SqlParameter media = new SqlParameter("@media", item.Media);

            //output parameter
            SqlParameter returnCode = new SqlParameter("returnCode", SqlDbType.Int);
            returnCode.Direction = ParameterDirection.Output;

            var result = context.Database.ExecuteSqlRaw("exec @returnCode = YT.sp_InsertVideo_videos @title, @media", returnCode, title, media);

            if (Convert.ToInt32(returnCode.Value) == 1)
            {
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Put(Video item)
        {

            //input parameters 
            SqlParameter videoId = new SqlParameter("@videoId", item.VideoId);
            SqlParameter title = new SqlParameter("@title", item.Title);

            //output parameter
            SqlParameter returnCode = new SqlParameter("@returnCode", SqlDbType.Int);
            returnCode.Direction = ParameterDirection.Output;

            var result = context.Database.ExecuteSqlRaw("exec @returnCode = YT.sp_updateVideo_videos @videoId, @title", returnCode, videoId, title);
            if (Convert.ToInt32(returnCode.Value) == 1)
            {
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
