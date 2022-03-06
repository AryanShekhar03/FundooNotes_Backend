using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class LabelController : ControllerBase
    {

        private readonly ILabelBL labelBL;
        private readonly FundooContext fundooContext;

        public LabelController(ILabelBL labelBL, FundooContext fundooContext)
        {
              
            this.labelBL = labelBL;
            this.fundooContext = fundooContext;
        }


        [HttpPost("Create")]
        public IActionResult AddLabel(LabelModel labelModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var labelNote = this.fundooContext.NotesTable.Where(x => x.NotesId == labelModel.NotesId).SingleOrDefault();
                if (labelNote.UserId == userId)
                {
                    var result = this.labelBL.AddLabel(labelModel);
                    if (result)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Label created ", data = labelModel.LabelName });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Label not created" });
                    }
                }
                return this.Unauthorized(new { status = 401, isSuccess = false, Message = "Unauthorized User!" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 400, isSuccess = false, Message = e.InnerException.Message });
            }
        }

        [HttpGet("Detail")]
        public IActionResult GetlabelByNotesId(long NotesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var notesid = this.labelBL.GetlabelByNotesId(NotesId);
                if (notesid != null)
                {
                    return this.Ok(new { Status = true, Message = "Label Found successfully", data = notesid });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Label  not Found " });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteLabel(long labelID)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (this.labelBL.DeleteLabel(labelID))
                {
                    return this.Ok(new { Success = true, message = "label Deleted successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Such label Found" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, message = ex.InnerException.Message });
                
            }
        }

        [HttpPut("Update")]
        public IActionResult UpdateLabel(LabelModel labelModel, long labelID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(Y => Y.Type == "UserID").Value);
                var result = this.labelBL.UpdateLabel(labelModel ,labelID);
                if (result != null) 
                {
                    return this.Ok(new { status = 200, isSuccess = true, message = "Label Updated Successfully", data = result });

                }
                else
                {
                    return this.BadRequest(new { status = false, message = "Label not found" });

                }

            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Status = 401, isSuccess = false, Message = ex.InnerException.Message });
            }
        }

    }
}
