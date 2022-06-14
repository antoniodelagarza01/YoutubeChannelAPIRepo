using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeChannelAPI.DataAccess.Models;
using System.Linq;

namespace YoutubeChannelAPI.DataAccess.Repos
{
    public class AdminRepo : IRepo<Admin>
    {

        private readonly YoutubeChannelDBContext context;

        public AdminRepo(YoutubeChannelDBContext context)
        {
            this.context = context;
        }
        public bool Delete(int id)
        {
            int result = Convert.ToInt32(context.Videos.FromSqlInterpolated($"exec YT.sp_deleteAdmin_admin {id}"));
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Admin> Get()
        {
            try
            {
                var result = context.Admins;
                return result.ToList();
            }
            catch (Exception)
            {

                return null;
            }
           
        }

        public Admin GetOne(int id)
        {
            try
            {
                var result = context.Admins.FirstOrDefault(x => x.AdminId == 1);
                return result;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool Post(Admin item)
        {
            int result = Convert.ToInt32(context.Videos.FromSqlInterpolated($"exec YT.sp_insertAdmin_admin {item.FirstName}, {item.LastName}, {item.Email}, {item.Password}"));
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public bool Put(Admin item)
        {
            int result = Convert.ToInt32(context.Videos.FromSqlInterpolated($"exec YT.sp_updateAdmin_admin {item.AdminId}, {item.FirstName}, {item.LastName}, {item.Password}"));
            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}
