using CoffeeTrackerApi.Models;
using CoffeeTrackerApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailService _emailService;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, EmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet("sendemail")]
        public IActionResult SendEmail(string message)
        {
            _emailService.SendEmail(message);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Logout([FromBody] object empty)
        {
            if (empty is not null)
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }
            // _userManager.GenerateEmailConfirmationTokenAsync(user);

            return Unauthorized();
        }


    }
}
