using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;

        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }

        [HttpPost]
        [Route("CreateNotes")]
        public IActionResult CreateNotes(NotesModel notesmodel)
        
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
    }
}
