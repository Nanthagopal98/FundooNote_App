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
using System.Text;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBL icollaboratorBL;
        private readonly FundooContext fundooContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public CollaboratorController(ICollaboratorBL icollaboratorBL, FundooContext fundooContext, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.icollaboratorBL = icollaboratorBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(CollaboratorModel collaBoratorModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = icollaboratorBL.Create(collaBoratorModel, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collaborator Creted", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Creation Failed" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete (long ColabId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = icollaboratorBL.Delete(ColabId, userId);
                if (result != false)
                {
                    return Ok(new { success = true, message = "Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Delete Failed" });
                }
            }
            catch (Exception)
            {
                 
                throw;
            }
        }
        [HttpGet]
        [Route("Get")]
        public IActionResult Get(long notesId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
            var result = icollaboratorBL.Get(notesId, userId);
            if(result != null)
            {
                return Ok(new { success = true, message = "Data Retrieved Successfully", data = result });
            }
            else
            {
                return BadRequest(new { successs = false, message = "Data Retrieve Failed" });
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "CollaboratorList";
            string serializedCollabList;
            var CollaboratorList = new List<CollaboratorEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                CollaboratorList = JsonConvert.DeserializeObject<List<CollaboratorEntity>>(serializedCollabList);
            }
            else
            {
                CollaboratorList = fundooContext.CollaboratorTable.ToList();
                serializedCollabList = JsonConvert.SerializeObject(CollaboratorList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(CollaboratorList);
        }
    }
}
