using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EncryptorDecryptor;
using YoutubeChannelAPI.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using YoutubeChannelAPI;
using YoutubeChannelAPI.DataAccess.Repos;

namespace YoutubeChannelAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string allowSelectedOrigins = "_allowSelectedOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<AppSettings>(Configuration.GetSection(AppSettings.SectionName));
            services.AddOptions();

            string iv = Configuration.GetSection("AppSettings")["IV"];
            string key = Configuration.GetSection("AppSettings")["Key"];
            string encryptedConnectionString = Configuration.GetConnectionString("YoutubeChannelDB");
            string decryptedConnectionString = EncryptorDecryptor.EncryptorDecryptor.Decrypt(encryptedConnectionString, iv, key);
            services.AddDbContext<YoutubeChannelDBContext>(options => options.UseSqlServer(decryptedConnectionString));
            services.AddTransient<IRepo<Admin>, AdminRepo>();
            services.AddTransient<IRepo<Video>, VideoRepo>();
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(allowSelectedOrigins, b => b.WithOrigins("https://localhost:44394/"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(allowSelectedOrigins);

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
            });
        }
    }
}
