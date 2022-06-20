using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeChannelAPI.DataAccess.Models;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Data;

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

            //input parameter
            SqlParameter adminId = new SqlParameter("@adminId", id);

            //output parameter
            SqlParameter returnCode = new SqlParameter("@returnCode", SqlDbType.Int);
            returnCode.Direction = ParameterDirection.Output;


            var result = context.Database.ExecuteSqlRaw("exec @returnCode = YT.sp_deleteAdmin_admin @adminId", returnCode, adminId);

            if (Convert.ToInt32(returnCode.Value) == 1)
            {
                context.SaveChanges();
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
                var result = context.Admins.FirstOrDefault(x => x.AdminId == id);
                return result;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool Post(Admin item)
        {

            //input parameters
            SqlParameter firstName = new SqlParameter("@firstName", item.FirstName);
            SqlParameter lastName = new SqlParameter("@lastName", item.LastName);
            SqlParameter email = new SqlParameter("@email", item.Email);
            SqlParameter password = new SqlParameter("@password", item.Password);

            //output parameters
            SqlParameter returnCode = new SqlParameter("@returnCode", SqlDbType.Int);
            returnCode.Direction = ParameterDirection.Output;


            var result = context.Database.ExecuteSqlRaw("exec @returnCode = YT.sp_insertAdmin_admin @firstName, @lastName, @email, @password", returnCode, firstName, lastName, email, password);
            if (Convert.ToInt32(returnCode.Value) == 1)
            {
                return true;
            }
            return false;
        }

        public bool Put(Admin item)
        {
            //input parameters
            SqlParameter adminId = new SqlParameter("@adminId", item.AdminId);
            SqlParameter firstName = new SqlParameter("@firstName", item.FirstName);
            SqlParameter lastName = new SqlParameter("@lastName", item.LastName);
            SqlParameter password = new SqlParameter("@password", item.Password);

            //output parameters
            SqlParameter returnCode = new SqlParameter("@returnCode", SqlDbType.Int);
            returnCode.Direction = ParameterDirection.Output;

            var result = context.Database.ExecuteSqlRaw("exec @returnCode = YT.sp_updateAdmin_admin @adminId, @firstName, @lastName, @password", returnCode, adminId, firstName, lastName, password);
            if (Convert.ToInt32(returnCode.Value) == 1)
            {
                return true;
            }
            return false;
        }
    }
}
