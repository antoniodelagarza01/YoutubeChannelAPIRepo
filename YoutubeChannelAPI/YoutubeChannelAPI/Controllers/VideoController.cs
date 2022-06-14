using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeChannelAPI.DataAccess.Models;
using YoutubeChannelAPI.DataAccess.Repos;
using System.Web;

namespace YoutubeChannelAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IRepo<Video> repo;

        public VideoController(IRepo<Video> repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult GetVideos()
        {
            try
            {
                var result = repo.Get().ToList();
                if (result.Count == 0)
                {
                    return NotFound("No Videos");
                }


                List<Models.Video> list = new List<Models.Video>();
                foreach (var video in result)
                {
                    list.Add(Mapper.Map(video));
                }

                return Ok(list);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong while retrieving Videos");

            }




        }

        [HttpGet("{id}", Name = "GetVideos")]
        public IActionResult GetVideos(int id)
        {
            try
            {
                var result = repo.GetOne(id);
                if (result == null)
                {
                    return NotFound("No video found with given information");
                }
                Models.Video video;
                video = Mapper.Map(result);
                return Ok(video);
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while retrieving video");
            }

        }

        [HttpPost]
        public IActionResult PostVideo([FromForm] Models.Video video)
        {
            try
            {
                Video vid;

                var files = HttpContext.Request.Form.Files;
                if (files.Count() == 0)
                {
                    return BadRequest("No media provided");
                }
                byte[] media;

                using (var filestream = files[0].OpenReadStream())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        filestream.CopyTo(memoryStream);
                        media = memoryStream.ToArray();
                    }
                }
                video.Media = media;
                vid = Mapper.Map(video);

                bool flag = repo.Post(vid);
                if (!flag)
                {
                    return NotFound("An error happended when posing video");
                }

                return Ok("Video posted successfully");
            }
            catch (Exception)
            {

                return BadRequest("Bad data provided");
            }
        }


        [HttpPut]
        public IActionResult PutVideo([FromBody] Models.Video video)
        {
            try
            {
                Video vid;
                vid = Mapper.Map(video);

                bool flag = repo.Put(vid);
                if (!flag)
                {
                    return NotFound("An error happended when updating video");
                }

                return Ok("Video updated successfully");
            }
            catch (Exception)
            {

                return BadRequest("Bad data provided");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVideo(int id)
        {
            try
            {
                bool flag = repo.Delete(id); ;
                if (!flag)
                {
                    return NotFound("Could not delete video");
                }

                return Ok("Video deleted successfully");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong while deleting video");
              
            }
        }


    }
}
