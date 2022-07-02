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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserMicroservice.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Database _db;
        private readonly ILogger<UserController> _logger;


        public UserController(ILogger<UserController> logger, Database db)
        {
            this._db = db;
            this._logger = logger;
        }

        // GET: /user
        [SwaggerResponse(200, "Fetched Users", typeof(Response<List<AppUser>>))]
        [SwaggerResponse(404, "User Not Found", typeof(Response))]
        [HttpGet, Authorize]
        public async Task<ActionResult<Response<List<AppUser>>>> Get()
        {
            #nullable enable
            List<AppUser>? users = await _db.AppUsers.ToListAsync();

            if(users == null)
            {
                _logger.LogError("No Users Exist");
                return NotFound(new Response("Users not found", false));
            }

            _logger.LogInformation("Sending Users List");
            return Ok(new Response("Users Found", true, users));
        }

        // GET /user/5
        [SwaggerResponse(404, "User Not Found", typeof(Response))]
        [SwaggerResponse(200, "Fetched User Details", typeof(Response<AppUser>))]
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Response<AppUser>>> Get(int id)
        {
            AppUser user = await _db.AppUsers.FindAsync(id);

            if(user == null)
            {
                return NotFound(new Response("User not found", false));
            }

            return Ok(new Response("User found", true, user));
        }

        // POST /user/
        [SwaggerResponse(501, "Server Error", typeof(Response))]
        [SwaggerResponse(201, "Created", typeof(Response<AppUser>))]
        [HttpPost]
        public async Task<ActionResult<Response<AppUser>>> Post([FromBody] AppUser user)
        {
            await _db.AppUsers.AddAsync(user);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return StatusCode(501, new Response("Database Error", false));
            }

            return CreatedAtAction(nameof(Get), new { id = user.Id }, new Response<AppUser>("User Added", true, user));
        }

        // PUT /user/5
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(404, "Not Found", typeof(Response))]
        [SwaggerResponse(202, "User Updated", typeof(Response<AppUser>))]
        [HttpPut("{id}"), Authorize()]
        public async Task<ActionResult<Response<AppUser>>> Put(int id, [FromBody] AppUser user)
        {
            if(user.Id != id)
            {
                return BadRequest(new Response("Ids Dont match", false));
            }

            _db.Entry(user).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if((await _db.AppUsers.FindAsync(id)) == null)
                {
                    return NotFound(new Response("User Not Found", false));
                }

                throw;
            }

            return Accepted(new Response<AppUser>("User Data Updated", true, user));
        }

        // DELETE /user/5
        [SwaggerResponse(200, "Get Authentication Token", typeof(Response))]
        [SwaggerResponse(404, "User Not Found", typeof(Response))]
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            var user = await _db.AppUsers.FindAsync(id);
            if(user == null)
            {
                return NotFound(new Response("User Doesnot Exists", false));
            }

            _db.Entry(user).State = EntityState.Deleted;
            _db.AppUsers.Remove(user);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(new Response("User Deleted", true, user));
        }

        // POST /user/login
        [SwaggerResponse(200, "Get Authentication Token", typeof(Response<AuthTokenPayload?>))]
        [SwaggerResponse(401, "Unautherized", typeof(Response))]
        [SwaggerResponse(502, "Internal Server Error", typeof(Response))]
        [HttpPost("login")]
        public async Task<ActionResult<Response<AuthTokenPayload?>>> Login(LoginRequest login)
        {
            AppUser user = await _db.AppUsers.Where(u => u.Email == login.Email && u.Password == login.Password).FirstOrDefaultAsync();

            if(user == default)
            {
                return Unauthorized(new Response("Wrong email or password!", false));
            }

            HttpClient _httpClient = new HttpClient();

            // Preparing User Data to be passed through the API
            var myContent = JsonSerializer.SerializeToUtf8Bytes(user);
            ByteArrayContent content = new ByteArrayContent(myContent);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            // Preparation Complete. Sending object "Content"

            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:44771/api/auth/login", content);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            var responseReader = await response.Content.ReadAsStreamAsync();
            AuthTokenPayload? authTokenPayload = await JsonSerializer.DeserializeAsync<AuthTokenPayload>(responseReader);

            if(authTokenPayload == null)
            {
                return StatusCode(502, new Response("Error Creating Auth Token", false));
            }

            return Ok(new Response<AuthTokenPayload>("Logged in Successfully", true, authTokenPayload));
        }
    }
}
