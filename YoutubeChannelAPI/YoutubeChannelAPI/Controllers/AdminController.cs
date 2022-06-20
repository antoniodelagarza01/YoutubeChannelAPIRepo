using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeChannelAPI.DataAccess.Repos;
using YoutubeChannelAPI.DataAccess.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace YoutubeChannelAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRepo<Admin> repo;
        private readonly IOptions<AppSettings> appSettings;

        public AdminController(IRepo<Admin> repo, IOptions<AppSettings> appSettings)
        {
            this.repo = repo;
            this.appSettings = appSettings;
        }

        [HttpGet]
        public IActionResult GetAdmins()
        {
            try
            {
                var result = repo.Get().ToList();
                if (result.Count == 0)
                {
                    return NotFound("No Admins");
                }

                List<Models.Admin> admins = new List<Models.Admin>();
                foreach (var admin in result)
                {

                    admin.Email = EncryptorDecryptor.EncryptorDecryptor.Decrypt(admin.Email, appSettings.Value.IV, appSettings.Value.Key);
                    admin.Password = EncryptorDecryptor.EncryptorDecryptor.Decrypt(admin.Password, appSettings.Value.IV, appSettings.Value.Key);
                    admins.Add(Mapper.Map(admin));
                }
                return Ok(admins);
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while retrieving Admin");
            }
        }

        [HttpGet("{id}", Name = "GetAdmins")]
        public IActionResult GetAdmins(int id)
        {
            try
            {
                var result = repo.GetOne(id);
                if(result == null)
                {
                    return NotFound("No admin found with given information");
                }

                result.Email = EncryptorDecryptor.EncryptorDecryptor.Decrypt(result.Email, appSettings.Value.IV, appSettings.Value.Key);
                result.Password = EncryptorDecryptor.EncryptorDecryptor.Decrypt(result.Password, appSettings.Value.IV, appSettings.Value.Key);
                Models.Admin admin;
                admin = Mapper.Map(result);
                return Ok(admin);
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while retreiving admin");
            }
        }

        [HttpPost]
        public IActionResult PostAdmin([FromBody] Models.Admin admin)
        {
            try
            {
                Admin ad;

                string email = admin.Email;
                string password = admin.Password;

                email = EncryptorDecryptor.EncryptorDecryptor.Encrypt(email, appSettings.Value.IV, appSettings.Value.Key);
                password = EncryptorDecryptor.EncryptorDecryptor.Encrypt(password, appSettings.Value.IV, appSettings.Value.Key);

                admin.Email = email;
                admin.Password = password;
                ad = Mapper.Map(admin);

                bool flag = repo.Post(ad);
                if (!flag)
                {
                    return NotFound("An error happened while posting admin");
                }

                return Ok("Admin created successfully");


            }
            catch (Exception)
            {
                return BadRequest("Bad data provided");
                
            }
        }

        [HttpPut]
        public IActionResult PutAdmin([FromBody] Models.Admin admin)
        {
            try
            {
                Admin ad;

                string password = admin.Password;

               
                password = EncryptorDecryptor.EncryptorDecryptor.Encrypt(password, appSettings.Value.IV, appSettings.Value.Key);

             
                admin.Password = password;

                ad = Mapper.Map(admin);

                bool flag = repo.Put(ad);
                if (!flag)
                {
                    return NotFound("An error happened while updating admin");

                }

                return Ok("Admin updated succcessfully");

            }
            catch (Exception)
            {

                return BadRequest("Bad data provided");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            try
            {
                bool flag = repo.Delete(id);
                if (!flag)
                {
                    return NotFound("Could not delete admin");
                }

                return Ok("Admin deleted successfully");

            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while deleting admin");
            }
        }
    }
}
