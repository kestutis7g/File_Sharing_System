using Microsoft.AspNetCore.Mvc;
using Forum.Core.Interfaces;
using Forum.API.Models;
using Forum.Core.Aggregates.Group.Entities;
using Forum.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Forum.API.Controllers;

[ApiController]
[Route("api/group")]
public class GroupController : BaseController
{
    public GroupController(IGroupService groupService)
    {
        GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
    }

    private IGroupService GroupService { get; }

    // GET api/group
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<GroupModel>>> GetGroupList()
    {
        var groupList = await GroupService.GetGroupList();
        //if (groupList is null)
        //{
        //    return NotFound();
        //}
        return Ok(groupList);
    }


    // GET api/group/{id}
    [HttpGet("id/{id:Guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<GroupEntity>> GetGroupById([FromRoute] Guid id)
    {
        var group = await GroupService.GetGroupById(id);
        if (group is null)
        {
            return NotFound();
        }
        return Ok(group);
    }

    // GET api/group/{name}
    [HttpGet("name/{name}")]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<GroupModel>>> GetGroupByName([FromRoute] string name)
    {
        var group = await GroupService.GetGroupByName(name);
        if (group is null)
        {
            return NotFound();
        }
        return Ok(group);
    }


    // POST api/group
    [HttpPost]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<GroupEntity>> CreateGroup([FromBody] GroupModel request)
    {
        try
        {
            var result = await GroupService.CreateGroup(Mapper.Map<GroupEntity>(request));
            return Created(nameof(result), result);
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

    // PUT api/group
    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<GroupEntity>> UpdateGroup([FromRoute] Guid id, [FromBody] GroupModel request)
    {
        try
        {
            var result = await GroupService.UpdateGroup(id, Mapper.Map<GroupEntity>(request));
            return Ok(result);
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


    // Delete api/group/{id}
    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> DeleteGroupById([FromRoute] Guid id)
    {
        var group = await GroupService.GetGroupById(id);
        if (group is null)
        {
            return NotFound();
        }

        await GroupService.DeleteGroup(id);

        return NoContent();
    }

    // Delete api/group/{id}
    [HttpDelete("{id:Guid}/hard")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> HardDeleteGroupById([FromRoute] Guid id)
    {
        var group = await GroupService.GetGroupById(id);
        if (group is null)
        {
            return NotFound();
        }

        await GroupService.HardDeleteGroup(id);

        return NoContent();
    }
}
