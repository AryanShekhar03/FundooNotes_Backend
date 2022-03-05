using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")] //Route is  matching incoming HTTP requests.
    [ApiController] //To enable Routing Requirements.
    public class CollabController : ControllerBase //To handle http request
    {
        private readonly ICollabBL collabBL;  // readonly can only be assigned a value from within the constructor(s) of a class.
        private readonly FundooContext fundooContext;

        public CollabController(ICollabBL collabBL, FundooContext fundooContext)
        {
            this.collabBL = collabBL;
            this.fundooContext = fundooContext;
        }

        [HttpPost("AddCollab")]
        public IActionResult AddCollab(CollabModel collabModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (this.collabBL.AddCollab(collabModel, userId))
                {
                    return this.Ok(new { Status = true, Message = "Note Shared successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = " Not have permission" });

                }

            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }

        }

        [HttpDelete("Remove")]
        public IActionResult ReomoveCollab(long collabID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "UserId").Value);
                var delete = this.collabBL.RemoveCollab(collabID);
                if (delete != null)
                {
                    return this.Ok(new { status = 200, isSuccess = true, message = "Member removed from collaboration ", data = collabID });
                }
                else
                {
                    return this.NotFound(new { isSuccess = false, message = "Member not removed from collaboration." });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = 401, isSuccess = false, Message = e.Message, InnerException = e.InnerException });
            }
        }


        [HttpGet("Detail")]
        public IActionResult GetCollabsByNoteId(long NotesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var notesid = this.collabBL.GetCollabsByNoteId(NotesId);
                if (notesid != null)
                {
                    return this.Ok(new { Status = true, Message = "Collaboration Found", data = notesid });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Collaboration not Found " });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }

        }
    }
}


