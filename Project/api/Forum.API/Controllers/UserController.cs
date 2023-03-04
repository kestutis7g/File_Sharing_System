using Microsoft.AspNetCore.Mvc;
using Forum.Core.Interfaces;
using Forum.API.Models;
using Forum.Core.Aggregates.User.Entities;
using Forum.Core.Services;
using Microsoft.EntityFrameworkCore;
using Forum.API.Auth;
using Microsoft.AspNetCore.Authorization;
using System.Security.Permissions;
using NLog.Fluent;
using Forum.API.Auth.Model;
using System.Net;
using System.Net.Http.Headers;

namespace Forum.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : BaseController
{
    public UserController(IUserService userService)
    {
        UserService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    private IUserService UserService { get; }

    [HttpPost]
    [Route("Login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] LoginRequestModel request)
    {
       var user = await UserService.GetUserByLogin(request.Login);
       if (user is null)
       {
           return Unauthorized();
       }

       var jwtAuthenticationManager = new JWTAuthenticationManager();
       var authResult = jwtAuthenticationManager.Authenticate(request, user);
       if (authResult == null)
           return Unauthorized();
       else
           return Ok(authResult);
    }

    
    //[HttpPost]
    //[Route("SumValues")]
    //[Authorize(Roles = "Admin")]
    //public IActionResult Sum([FromQuery(Name = "Value1")] int value1, [FromQuery(Name = "Value2")] int value2)
    //{
    //   var result = value1 + value2;
    //   return Ok(result);
    //}

    // GET api/user
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ICollection<UserModel>>> GetUserList()
    {
       var userList = await UserService.GetUserList();
       //if (userList is null)
       //{
       //    return NotFound();
       //}
       return Ok(userList);
    }


    // GET api/user/{id}
    [HttpGet("id/{id:Guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<UserEntity>> GetUserById([FromRoute] Guid id)
    {
       var user = await UserService.GetUserById(id);
        if (user is null)
        {
            return NotFound();
        }
        user.Password = null;
        return Ok(user);
    }

    // GET api/user/{id}
    [HttpGet("login/{login}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<UserModel>> GetUserByLogin([FromRoute] string login)
    {
       var user = await UserService.GetUserByLogin(login);
       if (user is null)
       {
           return NotFound();
       }
       user.Password = null;
       return Ok(user);
    }


    // POST api/user
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> CreateUser([FromBody] UserModel request)
    {
       try
       {
           await UserService.CreateUser(Mapper.Map<UserEntity>(request));
           request.Password = null;
           return Created(nameof(request), request);
       }
       catch (DbUpdateException ex)
       {
           return StatusCode(400, ex.InnerException.Message);
       }
       catch (Exception ex)
       {
           return StatusCode(500, ex.InnerException.Message);
       }

    }

    // PUT api/user
    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserModel request)
    {
       try
       {
           await UserService.UpdateUser(id, Mapper.Map<UserEntity>(request));
           request.Password = null;

           return NoContent();
       }
       catch (DbUpdateException ex)
       {
           return StatusCode(400, ex.InnerException.Message);
       }
       catch (Exception ex)
       {
           return StatusCode(500, ex.InnerException.Message);
       }
    }


    // Delete api/user/{id}
    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> DeleteUserById([FromRoute] Guid id)
    {
       var user = await UserService.GetUserById(id);
       if (user is null)
       {
           return NotFound();
       }

       await UserService.DeleteUser(id);

       return NoContent();
    }

    // Delete api/user/{id}
    [HttpDelete("{id:Guid}/hard")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> HardDeleteCommentById([FromRoute] Guid id)
    {
       var user = await UserService.GetUserById(id);
       if (user is null)
       {
           return NotFound();
       }

       await UserService.HardDeleteUser(id);

       return NoContent();
    }
}
