using Microsoft.AspNetCore.Mvc;
using TrialApi.Model;

namespace TrialApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        List<Login> users = new() {
             new Login {UserName = "a" , Password = "1234"},
             new Login {UserName ="b" ,Password = "1235"}
        };

        [HttpPost(Name = "login")]
        public async Task<IActionResult> In([FromBody]Login user)
        {
            var test = await TestUser(user);

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

        private async Task<(bool isExist ,Login? user)> TestUser(Login user)
        {
            if(!string.IsNullOrEmpty(user?.UserName) || !string.IsNullOrEmpty(user?.Password))
            {
                var result = users.FirstOrDefault(c => c.UserName == user.UserName && c.Password == user.Password);
                    return (result is Login ? true : false, result);
            }
            return (false, null);
        }
    }
}
