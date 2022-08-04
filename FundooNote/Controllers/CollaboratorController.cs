using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBL icollaboratorBL;
        public CollaboratorController(ICollaboratorBL icollaboratorBL)
        {
            this.icollaboratorBL = icollaboratorBL;
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(CollaboratorModel collaBoratorModel)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
            var result = icollaboratorBL.Create(collaBoratorModel, userId);
            if(result != null)
            {
                return Ok(new { success = true, message = "Collaborator Creted", data = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Creation Failed" });
            }
        }
    }
}
