using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Linq;
using System.Security.Claims;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteBL iNoteBL;
        private readonly FundooContext fundooContext;
        public NotesController(INoteBL iNoteBL, FundooContext fundooContext)
        {
            this.iNoteBL = iNoteBL;
            this.fundooContext = fundooContext;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var notes = iNoteBL.CreateNotes(notesModel, userId);
                if (notes != null)
                {
                    return Ok(new { success = true, message = "Notes Added Successfully", data = notes });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed To Add Notes" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult DisplayNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.DisplayNotes(userId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Data Retrieved", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to Retrieve Data" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteNotes(long notesId)
        {
            try
            {
                var result = iNoteBL.DeleteNotes(notesId);
                if(result != false)
                {
                    return Ok(new { success = true, message = "Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed To Delete Notes" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNotes(NotesUpdateModel notesUpdateModel, long notesId)
        {
            try
            {
                var result = iNoteBL.UpdateNotes(notesUpdateModel, notesId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Notes Updated Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { successs = false, message = "Failed To Update Notes" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("Archive")]
        public IActionResult Archive(long noteId)
        {
            try
            {
                var result = iNoteBL.Archive(noteId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Note Archiev Processes", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Faild To Archieve" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("Pin")]
        public IActionResult Pin(long notesId)
        {
            try
            {
                var result = iNoteBL.Pin(notesId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Pin or Unpin Notes Done", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Faild Pinning" });
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpPut]
        [Route("Trash")]
        public IActionResult Trash(long notesId)
        {
            try
            {
                var result = iNoteBL.Trash(notesId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Trash Performed", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Faild Pinning" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("Color")]
        public IActionResult Color(long notesId, string color)
        {
            try
            {
                var result = iNoteBL.Color(notesId, color);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Color Changer", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Faild Pinning" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
