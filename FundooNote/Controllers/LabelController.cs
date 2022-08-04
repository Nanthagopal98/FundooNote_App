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
    }
}
