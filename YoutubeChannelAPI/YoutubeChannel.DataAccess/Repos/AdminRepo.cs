using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeChannel.DataAccess.Models;
using System.Linq;

namespace YoutubeChannel.DataAccess.Repos
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
            int result = Convert.ToInt32(context.Videos.FromSqlRaw("YT.sp_deleteAdmin_admin {0}", id));
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Admin> Get()
        {
            return context.Admins.ToList();
        }

        public Admin GetOne(int id)
        {
            return context.Admins.FirstOrDefault(x => x.AdminId == id);
        }

        public bool Post(Admin item)
        {
            int result = Convert.ToInt32(context.Videos.FromSqlRaw("YT.sp_insertAdmin_admin {0}, {1}, {2}, {3}", item.FirstName, item.LastName, item.Email, item.Password));
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public bool Put(Admin item)
        {
            int result = Convert.ToInt32(context.Videos.FromSqlRaw("YT.sp_updateAdmin_admin {0}, {1}, {2}, {3}", item.AdminId, item.FirstName, item.FirstName, item.Password));
            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}
