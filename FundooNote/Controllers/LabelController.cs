using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;
using System;
using System.Linq;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL iLabelBL;
        public LabelController(ILabelBL iLabelBL)
        {
            this.iLabelBL = iLabelBL;
        }

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
    }
}
