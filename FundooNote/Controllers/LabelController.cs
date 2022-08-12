using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL iLabelBL;
        private readonly FundooContext fundooContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<NotesController> logger;
        public LabelController(ILabelBL iLabelBL, FundooContext fundooContext, IMemoryCache memoryCache, IDistributedCache distributedCache,
            ILogger<NotesController> logger)
        {
            this.iLabelBL = iLabelBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.logger = logger;
        }
        // ghostdoc extension installed, and type '///' it automatically creates below content


        /// <summary>
        /// Create Label API
        /// </summary>
        /// <param name="labelModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(LabelModel labelModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iLabelBL.Create(labelModel, userId);
                if (result != null)
                {
                    return Ok(new { success = true, meassage = "Label Created", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, meassage = "Label Creation Failed" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update label API
        /// </summary>
        /// <param name="labelId"></param>
        /// <param name="labelModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(long labelId, LabelModel labelModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iLabelBL.Update(userId, labelId, labelModel);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Update Done", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, messsage = "Update Failed" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Get labels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
            var result = iLabelBL.Get(userId);
            if(result != null)
            {
                return Ok(new { success = true, message = "Data Retrieved", data = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Retrieve Data Failed" });
            }
        }
        /// <summary>
        /// Deleete label
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(long labelId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = iLabelBL.Delete(userId, labelId);
                if(result != false)
                {
                    return Ok(new { success = true, message = "Deleted Successfully", data = result });
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
        /// <summary>
        /// Store data in cache memory and for faster data retrival 
        /// </summary>
        /// <returns></returns>
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                LabelList = fundooContext.LabelTable.ToList();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }
    }
}
