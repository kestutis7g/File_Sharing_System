using Forum.API.Models;
using Forum.Core.Interfaces;
using Forum.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoRestSimonas.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api")]
public class AuthController : ControllerBase
{


    public AuthController(IUserService userService)
    {
        UserService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    private IUserService UserService { get; }
    
    // [HttpPost]
    // [Route("user/register")]
    // public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    // {
    //     var user = await _userManager.FindByNameAsync(registerUserDto.UserName);
    //     if (user != null)
    //         return BadRequest("Request invalid.");

    //     var newUser = new RestUser
    //     {
    //         Email = registerUserDto.Email,
    //         UserName = registerUserDto.UserName
    //     };
    //     var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
    //     if (!createUserResult.Succeeded)
    //         return StatusCode(StatusCodes.Status500InternalServerError, createUserResult.Errors);
    //         //return BadRequest("Could not create a user.");
            

    //     await _userManager.AddToRoleAsync(newUser, ForumRoles.User);
        
    //     return CreatedAtAction(nameof(Register), new UserDto(newUser.Id, newUser.UserName, newUser.Email));
    // }

    // [HttpPost]
    // [Route("user/login")]
    // public async Task<ActionResult> Login(LoginDto loginDto)
    // {
    //     var user = await _userManager.FindByNameAsync(loginDto.UserName);
    //     if (user == null)
    //         return BadRequest("User name or password is invalid.");
        
    //     var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
    //     if (!isPasswordValid)
    //         return BadRequest("User name or password is invalid.");
        
    //     // valid user
    //     var roles = await _userManager.GetRolesAsync(user);
    //     var accessToken = _jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
        
    //     return Ok(new SuccessfulLoginDto(accessToken));
    // }
}