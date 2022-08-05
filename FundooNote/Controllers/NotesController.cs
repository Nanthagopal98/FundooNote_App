using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteBL iNoteBL;
        private readonly FundooContext fundooContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public NotesController(INoteBL iNoteBL, FundooContext fundooContext, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.iNoteBL = iNoteBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.DeleteNotes(notesId, userId);
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
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.UpdateNotes(notesUpdateModel, notesId, userId);
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
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.Archive(noteId, userId);
                if(result == true)
                {
                    return Ok(new { success = true, message = "Note Archieved", data = result });
                }
                else if(result == false)
                {
                    return Ok(new { success = true, message = "Note UnArchieved", data = result });
                }
                return NotFound(new { success = false, message = "Faild To Archieve" });
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
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.Pin(notesId, userId);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Notes Pinned", data = result });
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "Notes UnPinned", data = result });
                }
                return NotFound(new { success = false, message = "Faild Pinning" });
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
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.Trash(notesId, userId);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Notes Trashed", data = result });
                }
                else if(result == false)
                {
                    return Ok(new { success = true, message = "Notes Restored", data = result });
                }
                return NotFound(new { success = false, message = "Trash Api Faild" });               
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
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iNoteBL.Color(notesId, color, userId);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Color Changed", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Color change Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("AddImage")]
        public IActionResult AddImage(string filePath, long notesId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
            var result = iNoteBL.UploadImage(filePath, notesId, userId);
            if(result != null)
            {
                return Ok(new { success = true, message = "Uploaded Success", data = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Upload Failed" });
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = fundooContext.NotesTable.ToList();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
    }
}
