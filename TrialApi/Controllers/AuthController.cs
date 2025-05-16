using Microsoft.AspNetCore.Mvc;
using TrialApi.Model;
using Trial.Application.Interfaces;
using Azure.Core.Pipeline;
using Trial.Domain.Common.Enums;
using Trial.Application.DTO;
using Trial.Domain.Common;

namespace TrialApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUser _user;
        public AuthController(IUser user)
        {
            _user = user;
        }

        List<Login> users = new() {
             new Login {UserName = "a" , Password = "1234"},
             new Login {UserName ="b" ,Password = "1235"}
        };

        [HttpPost(Name = nameof(Basic.Login))]
        public async Task<IActionResult> In([FromBody]Login? user)
        {
            var result = await _user.Login(user);

            if (test.isExist)
            {
                test.user.Id = Guid.NewGuid();
                return Ok(test.user);
            }
            else
            {
                return NotFound();
            }
        } 
    }
}
