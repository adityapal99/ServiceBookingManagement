using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        public readonly IAuthenticationService _authService;
        public readonly ILogger _logger;

        public AuthenticationController(IAuthenticationService _auth, ILogger logger)
        {
            _logger = logger;
            _authService = _auth;
        }


        [HttpPost("login")]
        public ActionResult<AuthTokenPayload> Login([FromBody] AppUser user)
        {
            return Ok(_authService.GetAuthToken(user));
        }
    }
}
