using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")] //Route is  matching incoming HTTP requests.
    [ApiController] //To enable Routing Requirements.
    [Authorize] //user to grant and restrict permissions on Web pages.
    public class NotesController : ControllerBase //To handle http request
    {
        private readonly INotesBL notesBL;// readonly can only be assigned a value from within the constructor(s) of a class.
        private readonly FundooContext fundoocontext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        public NotesController(INotesBL notesBL, FundooContext fundoocontext, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.fundoocontext = fundoocontext;
            this.distributedCache = distributedCache;
        }

        [HttpPost]
        [Route("CreateNotes")]
        public IActionResult CreateNotes(NotesModel notesmodel) //IActionResult lets you return both data and HTTP codes.

        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.notesBL.CreateNote(notesmodel, userId);
                return this.Ok(new { success = true, message = "Notes Added Successful", data = result });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.InnerException });
            }
        }

        [HttpGet]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "AllNOtes";
            string serializedAllNotes;
            var AllNotes = new List<Notes>();
            var redisAllNotes = await distributedCache.GetAsync(cacheKey);
            if (redisAllNotes != null)
            {
                serializedAllNotes = Encoding.UTF8.GetString(redisAllNotes);
                AllNotes = JsonConvert.DeserializeObject<List<Notes>>(serializedAllNotes);
            }
            else
            {
                AllNotes = await fundoocontext.NotesTable.ToListAsync();
                serializedAllNotes = JsonConvert.SerializeObject(AllNotes);
                redisAllNotes = Encoding.UTF8.GetBytes(serializedAllNotes);
                    var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisAllNotes, options);
            }
            return Ok(AllNotes);
        }

        [HttpGet("GET")]
        public IActionResult GetAllNotes(long userId)
        {
            try
            {
                var notes = notesBL.GetAllNotes(userId);
                if (notes != null)
                {
                    return this.Ok(new { isSuccess = true, message = " All notes found Successfully", data = notes });

                }
                else
                {
                    return this.NotFound(new { isSuccess = false, message = "No Notes Found" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = 401, isSuccess = false, Message = e.Message, InnerException = e.InnerException });
            }



        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllUserNotes()
        {
            try
            {
                IEnumerable<Notes> notes = this.notesBL.GetAllUserNotes();
                return Ok(notes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpPut("Update")]


        public IActionResult UpdateNote(NotesModel notesUpdateModel, long NotesID)
        {

            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "UserId").Value);
                var result = this.notesBL.UpdateNote(notesUpdateModel, NotesID);
                if (result != null)
                {
                    return this.Ok(new { isSuccess = true, message = "Notes Updated Successfully", data = result });
                }
                else
                {
                    return this.NotFound(new { isSuccess = false, message = "No Notes Found" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = 401, isSuccess = false, Message = e.Message, InnerException = e.InnerException });
            }

        }

        [HttpDelete("DeleteNotes")]
        public IActionResult DeleteNotes(long NotesID)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (this.notesBL.DeleteNotes(NotesID))
                {
                    return this.Ok(new { Success = true, message = "Delete successfull" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Registration Found" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }

        [HttpPut("Archieve")]
        public IActionResult ArchieveNotes(long NotesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.notesBL.ArchieveNotes(NotesId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = result });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }


        [HttpPut("Pinned")]
        public IActionResult Pinned(long NotesID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.notesBL.Pinned(NotesID);
                if (result != null)
                {
                    return this.Ok(new { Status = true, message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = result });
                }

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
        [HttpPut("TrashNotes")]
        public IActionResult TrashNotes(long NotesID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.TrashNotes(NotesID);
                if (result != null)
                {
                    return this.Ok(new { Status = true, message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = result });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }
        [HttpPut("Color")]
        public IActionResult NotesColor(long NotesId, string color)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.notesBL.Notescolor(NotesId, color);
                if (result != color)
                {
                    return this.Ok(new { Status = true, message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = result });
                }

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }

        [HttpPut("Image")]
        public IActionResult Image(long NotesID, IFormFile Image)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (this.notesBL.Image(NotesID, Image))
                {
                    return this.Ok(new { Status = true, message = "success" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "failed" });
                }


            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }

        }
    }

}
