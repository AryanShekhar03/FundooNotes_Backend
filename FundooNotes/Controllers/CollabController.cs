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
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
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

        [HttpGet("DetailS")]
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
