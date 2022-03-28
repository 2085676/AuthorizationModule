using AuthorizationModule.Models;
using AuthorizationModule.Services;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthorizationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService service)
        {
            _authService = service;
        }

        [HttpPost]
        public ActionResult<AuthResponse> Login([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                throw new Exception("Please enter email or password");
            AuthResponse res = _authService.Login(user);
            if (string.IsNullOrEmpty(res.Token))
                throw new Exception("Failed to login");
            return Ok(res);
        }
    }
}
