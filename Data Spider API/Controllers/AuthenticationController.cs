using Data_Spider_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Data_Spider_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
    }
}
