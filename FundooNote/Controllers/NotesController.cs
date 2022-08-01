using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Linq;
using System.Security.Claims;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteBL iNoteBL;
        public NotesController(INoteBL iNoteBL)
        {
            this.iNoteBL = iNoteBL;
        }
        [Authorize]
        [HttpPost]
        [Route("CreateNotes")]
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
        [Authorize]
        [HttpGet]
        [Route("GetNotes")]
        public IActionResult DisplayNotes(long notesId)
        {
            try
            {
                var result = iNoteBL.DisplayNotes(notesId);
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
    }
}
