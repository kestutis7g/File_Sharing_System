using Microsoft.AspNetCore.Mvc;
using Forum.Core.Interfaces;
using Forum.API.Models;
using Forum.Core.Aggregates.GroupUser.Entities;
using Forum.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Forum.API.Controllers;

[ApiController]
[Route("api/groupUser")]
public class GroupUserController : BaseController
{
    public GroupUserController(IGroupUserService groupUserService)
    {
        GroupUserService = groupUserService ?? throw new ArgumentNullException(nameof(groupUserService));
    }

    private IGroupUserService GroupUserService { get; }

    // GET api/groupUser
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<GroupUserModel>>> GetGroupUserList()
    {
        var groupUserList = await GroupUserService.GetGroupUserList();
        if (groupUserList is null)
        {
            return NotFound();
        }
        return Ok(groupUserList);
    }

    // GET api/groupUser/{id}
    [HttpGet("{groupId:Guid}/{userId:Guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<GroupUserEntity>> GetGroupUserByIds([FromRoute] Guid groupId, [FromRoute] Guid userId)
    {
        var groupUser = await GroupUserService.GetGroupUserByIds(groupId, userId);
        if (groupUser is null)
        {
            return NoContent();
        }
        return Ok(groupUser);
    }
    // GET api/groupUser/{id}
    [HttpGet("groupId/{id:Guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<GroupUserEntity>>> GetGroupUsersByGroupId([FromRoute] Guid id)
    {
        var groupUser = await GroupUserService.GetGroupUsersByGroupId(id);
        if (groupUser is null)
        {
            return NoContent();
        }
        return Ok(groupUser);
    }
    // GET api/groupUser/{id}
    [HttpGet("userId/{id:Guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<GroupUserEntity>>> GetGroupsByUserId([FromRoute] Guid id)
    {
        var groupUser = await GroupUserService.GetGroupsByUserId(id);
        if (groupUser is null)
        {
            return NoContent();
        }
        return Ok(groupUser);
    }

    // POST api/groupUser
    [HttpPost]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> CreateGroupUser([FromBody] GroupUserModel request)
    {
        try
        {
            await GroupUserService.CreateGroupUser(Mapper.Map<GroupUserEntity>(request));
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

    // // PUT api/groupUser
    // [HttpPut("{id:Guid}")]
    // [Authorize(Roles = "Admin, User")]
    // public async Task<ActionResult> UpdateGroupUser([FromRoute] Guid id, [FromBody] GroupUserModel request)
    // {
    //     try
    //     {
    //         await GroupUserService.UpdateGroupUser(id, Mapper.Map<GroupUserEntity>(request));
    //         return NoContent();
    //     }
    //     catch (DbUpdateException ex)
    //     {
    //         return StatusCode(400, ex.InnerException.Message);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, ex.InnerException.Message);
    //     }
    // }


    // Delete api/groupUser/{id}
    [HttpDelete("{groupId:Guid}/{userId:Guid}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> DeleteGroupUserByIds([FromRoute] Guid groupId, [FromRoute] Guid userId)
    {
        var groupUser = await GroupUserService.GetGroupUserByIds(groupId, userId);
        if (groupUser is null)
        {
            return NotFound();
        }

        await GroupUserService.DeleteGroupUser(groupUser);

        return NoContent();
    }
}
