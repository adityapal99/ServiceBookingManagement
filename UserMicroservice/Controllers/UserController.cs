using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Swashbuckle.AspNetCore.Annotations;

using UserMicroservice.Models;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using UserMicroservice.Repository;
using UserMicroservice.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
#nullable enable
namespace UserMicroservice.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Database _db;
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;


        public UserController(ILogger<UserController> logger, Database db, IUserRepository userRepository)
        {
            this._db = db;
            this._logger = logger;
            this._userRepository = userRepository;
        }

        // GET: /user
        [SwaggerResponse(200, "Fetched Users", typeof(Response<List<AppUser>>))]
        [SwaggerResponse(404, "User Not Found", typeof(Response))]
        [HttpGet, Authorize]
        public async Task<ActionResult<Response<List<AppUser>>>> Get()
        {
            return Ok(new Response(
                "Users Found", 
                true, 
                await _userRepository.GetAllUsers()
                ));
        }

        // GET /user/5
        [SwaggerResponse(404, "User Not Found", typeof(Response))]
        [SwaggerResponse(200, "Fetched User Details", typeof(Response<AppUser>))]
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Response<AppUser>>> Get(int id)
        {
            AppUser user = await _userRepository.GetAppUser(id);

            if(user == null) return NotFound(new Response("User not found", false));

            return Ok(new Response("User found", true, user));
        }

        // POST /user/
        [SwaggerResponse(501, "Server Error", typeof(Response))]
        [SwaggerResponse(201, "Created", typeof(Response<AppUser>))]
        [HttpPost]
        public async Task<ActionResult<Response<AppUser>>> Post([FromBody] AppUser user)
        {
            AppUser insertedUser = await _userRepository.InsertUser(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, new Response<AppUser>("User Added", true, insertedUser));
        }

        // PUT /user/5
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(404, "Not Found", typeof(Response))]
        [SwaggerResponse(202, "User Updated", typeof(Response<AppUser>))]
        [HttpPut("{id}"), Authorize()]
        public async Task<ActionResult<Response<AppUser>>> Put(int id, [FromBody] AppUser user)
        {
            if(user.Id != id) return BadRequest(new Response("Ids Dont match", false));

            AppUser updatedUser = await _userRepository.UpdateUser(user);

            if (updatedUser == null) return NotFound(new Response("User Not Found", false));

            return Accepted(new Response<AppUser>("User Data Updated", true, updatedUser));
        }

        // DELETE /user/5
        [SwaggerResponse(200, "Get Authentication Token", typeof(Response))]
        [SwaggerResponse(404, "User Not Found", typeof(Response))]
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            bool deleted = await _userRepository.DeleteUser(id);

            if (!deleted) return NotFound(new Response("User not found", false));

            return Ok(new Response("User Deleted", true));
        }

        // POST /user/login
        [SwaggerResponse(200, "Get Authentication Token", typeof(Response<AuthTokenPayload?>))]
        [SwaggerResponse(401, "Unautherized", typeof(Response))]
        [SwaggerResponse(502, "Internal Server Error", typeof(Response))]
        [HttpPost("login")]
        public async Task<ActionResult<Response<AuthTokenPayload?>>> Login(LoginRequest login)
        {
            AuthTokenPayload token;
            try
            {
                token = await _userRepository.LoginUser(login);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(new Response(e.Message, false));
            }
            catch(ConnectedServiceException e)
            {
                return StatusCode(502, new Response(e.Message, false));
            }
            catch
            {
                throw;
            }
            return Ok(new Response<AuthTokenPayload>("Logged in Successfully", true, token));
        }
    }
}
