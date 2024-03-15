using Data_Spider_API.Models;
using Data_Spider_API.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<AppUser> _claimsPrincipalFactory;

        public AuthenticationController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
            IUserClaimsPrincipalFactory<AppUser> claimsPrincipalFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
        }

        [HttpPost]
        [Route("Register")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Register(UserVM uvm, [FromForm] IFormCollection formData)
        {
            bool validEmail = uvm.Email.Contains('@');
            if (!validEmail)
            {
                return BadRequest("Enter A Valid Email Address");
            }

            var user = await _userManager.FindByEmailAsync(uvm.Email);
            if (user == null)
            {
                //Creating User as an instance of AppUser
                user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = uvm.Email,
                    Email = uvm.Email,
                    FirstName = uvm.FirstName,
                    LastName = uvm.LastName,
                };

                //Add user to AppUser
                var result = await _userManager.CreateAsync(user, uvm.Password);

                if (result.Errors.Count() > 0)
                    return StatusCode(StatusCodes.Status500InternalServerError, result);

                /*
                try
                {
                    await SendEmail(user.Email, user.FirstName, user.LastName, uvm.Password, user.Id);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Error sending register email");
                } */

                return Ok("You Have Been Successfully Registered. Login To Continue Setting Up Your Profile.");
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden, "This Email Address Already Exists in The System. Go to Login or Register Using A Different Email Address");
            }
        }

        //Login Endpoint

        //Forgot Password Endpoint

        //Logout Endpoint
    }
}
