using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Models;
using UserMicroservice.Repository.IRepository;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UsersController : ControllerBase
    {
        private readonly IAppUserRepository _userRep;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(UsersController));
        public UsersController(IAppUserRepository userRep)
        {
            _userRep = userRep;
        }

        /*/// <summary>
        /// Get List Of users in Database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200,Type =typeof(List<AppUser>))]
        public IActionResult GetUsers()
        {
            var userlist = _userRep.GetUsers();
            return Ok(userlist);
        }


        /// <summary>
        /// Get user details by Id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("{userid:int}",Name ="GetUser")]
        [ProducesResponseType(200, Type = typeof(AppUser))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetUser(int userid)
        {
            var user = _userRep.GetUser(userid);
            if(user==null)
            {
                return NotFound();
            }
            return Ok(new {status=StatusCode(200),msg="All Ok",payload=user});
        }

        /// <summary>
        /// Create User in Database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AppUser))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateUser([FromBody]AppUser user  )
        {
            if(user==null)
            {
                return BadRequest(ModelState);
            }
            if(_userRep.UserExists(user.Email))
            {
                ModelState.AddModelError("", "User Already Exists");
                return StatusCode(404, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userobjcreate = _userRep.CreateUser(user);
            if(!userobjcreate)
            {
                ModelState.AddModelError("", $"Something went wrong {user.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetUser", new { userid = user.Id }, user);
         }

        /// <summary>
        /// Update User by UserId
        /// </summary>
        /// <param name="userid">Id of The User</param>
        /// <param name="user">User Details Object</param>
        /// <returns></returns>

        [HttpPatch("{userid:int}",Name ="UpdateUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUser(int userid,[FromBody] AppUser user)
        {
            if(user==null||user.Id!=userid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRep.UserExistsId(userid))
            {
                return NotFound();
            }

            if (!_userRep.UpdateUser(user))
            {
                ModelState.AddModelError("", $"Something went wrong{user.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete User From Database
        /// </summary>
        /// <param name="userid">Id of The User</param>
        /// <returns></returns>

        [HttpDelete("{userid:int}",Name ="DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int userid)
        {
            if(!_userRep.UserExistsId(userid))
            {
                return NotFound();
            }
            var getuser = _userRep.GetUser(userid);
            if(!_userRep.DeleteUser(getuser))
            {
                ModelState.AddModelError("", $"Something went wrong for {getuser.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }



    }
}*/


[HttpGet]
public async Task<ActionResult> GetUsers()
{
    try
    {
        _log4net.Info("GetAppUsers Method Called");
        IEnumerable<AppUser> AppUsers = await _userRep.GetUsers();
        if(AppUsers.Count()==0)
                {
                    return NoContent();
                }
                //return Ok(new { status = 200, msg = "All AppUsers", payload = AppUsers });
                return Ok(StatusCodes.Status200OK);
    }
    catch
    {
        _log4net.Error("Database error");
        return StatusCode(500);
    }
}

// GET: api/AppUsers/5
[HttpGet("{id}")]
public async Task<ActionResult<AppUser>> GetAppUser(int id)
{
    try
    {
        _log4net.Info("GetAppUser Method Called");
        var AppUser = await _userRep.GetUserById(id);

        if (AppUser == null)
        {
            return NotFound();
        }

        return Ok(new { status = 200, msg = "AppUser Found", payload = AppUser });
    }
    catch
    {
        _log4net.Error("Database error");
        return StatusCode(500);
    }
}

// PUT: api/AppUsers/5
[HttpPut("{id}")]
public async Task<IActionResult> PutAppUser(int id, AppUser AppUser)
{
    _log4net.Info("PutAppUser Method called");
    if (id != AppUser.Id)
    {
        return BadRequest();
    }
    try
    {
        await _userRep.UpdateUser(id, AppUser);
        return Ok(new { status = 200, msg = "Update Successful", payload = AppUser });
    }
    catch
    {
        _log4net.Error("Databse error");
        return StatusCode(500);
    }
}

// POST: api/AppUsers
[HttpPost]
public async Task<ActionResult<AppUser>> PostAppUser([FromBody] AppUser AppUser)
{
    try
    {
        if (ModelState.IsValid)
        {
            _log4net.Info("PostAppUser Method Called");
            AppUser AppUserWithId = await _userRep.CreateUser(AppUser);
            return CreatedAtAction("PostAppUser", new { status = 200, msg = "AppUser Added", payload = AppUserWithId });
        }
        else
        {
            _log4net.Info("Model is not valid in PostAppUser");
            return BadRequest();
        }
    }
    catch (Exception e)
    {
        _log4net.Error("Database Error"+e.Message);
        return StatusCode(500);
    }
}

// DELETE: api/AppUsers/5
[HttpDelete("{id}")]
public async Task<ActionResult<AppUser>> DeleteAppUser(int id)
{
    try
    {
        _log4net.Info("DeleteAppUser Method Called");
        var AppUser = await _userRep.DeleteUserbyId(id);
        if (AppUser == null)
        {
            return NotFound();
        }

        return Ok(new { status = 200, msg = "Deleted Successfully", payload = AppUser });
    }
    catch
    {
        _log4net.Error("Database Error");
        return StatusCode(500);
    }
}

        
    }
}