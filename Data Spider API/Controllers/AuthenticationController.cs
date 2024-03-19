﻿using Data_Spider_API.Models;
using Data_Spider_API.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Data_Spider_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserClaimsPrincipalFactory<AppUser> _claimsPrincipalFactory;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
            IUserClaimsPrincipalFactory<AppUser> claimsPrincipalFactory, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _configuration = configuration;
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
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginVM uvm, [FromForm] IFormCollection formData)
        {
            var user = await _userManager.FindByNameAsync(uvm.EmailAddress);

            if (user != null && await _userManager.CheckPasswordAsync(user, uvm.Password))
            {
                try
                {
                    var token = GenerateJWTToken(user);
                    return Ok(new { token });
                }
                catch (Exception ex)
                {
                    // Log the specific exception details for troubleshooting
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while generating the JWT token.");
                }
            }

            return Unauthorized("Invalid credentials");
        }

        //Method to generate JWT Token
        [HttpGet]
        private async Task<string> GenerateJWTToken(AppUser user)
        {
            var claims = await GetAllValidClaims(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(90),
                SigningCredentials = credentials,
                Issuer = _configuration["Tokens:Issuer"],
                Audience = _configuration["Tokens:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        //Method to get all valid claims
        private async Task<List<Claim>> GetAllValidClaims(AppUser user)
        {
            var _options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),

            };

            //Getting claims that we have assigned to the user
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            //Get user role and add it to claims
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);

                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }

        //Logout Endpoint
        [HttpPost]
        [Route("Logout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Ok();
        }

        //Forgot Password Endpoint

    }
}
