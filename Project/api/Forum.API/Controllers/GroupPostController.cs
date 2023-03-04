using Microsoft.AspNetCore.Mvc;
using Forum.Core.Interfaces;
using Forum.API.Models;
using Forum.Core.Aggregates.GroupPost.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Forum.API.Controllers;

[ApiController]
[Route("api/groupPost")]
public class GroupPostController : BaseController
{
    public GroupPostController(IGroupPostService groupPostService)
    {
        GroupPostService = groupPostService ?? throw new ArgumentNullException(nameof(groupPostService));
    }

    private IGroupPostService GroupPostService { get; }

    // GET api/groupPost
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<GroupPostModel>>> GetGroupPostList()
    {
        var groupPostList = await GroupPostService.GetGroupPostList();
        if (groupPostList is null)
        {
            return NotFound();
        }
        return Ok(groupPostList);
    }


    // GET api/groupPost/{id}
    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<GroupPostModel>>> GetGroupPostById([FromRoute] Guid id)
    {
        var groupPost = await GroupPostService.GetGroupPostByGroupId(id);
        if (groupPost is null)
        {
            return NotFound();
        }
        return Ok(groupPost);

    }


    // POST api/groupPost
    [HttpPost]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> CreateGroupPost([FromBody] GroupPostModel request)
    {
        try
        {
            await GroupPostService.CreateGroupPost(Mapper.Map<GroupPostEntity>(request));
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

    /*// PUT api/groupPost
    [HttpPut("{id:Guid}")]
    public async Task<ActionResult> UpdateGroupPost([FromRoute] Guid id, [FromBody] GroupPostModel request)
    {
        await GroupPostService.UpdateGroupPost(id, Mapper.Map<GroupPostEntity>(request));

        return NoContent();
    }*/


    // Delete api/groupPost/{id}
    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteGroupPostById([FromRoute] Guid id)
    {
        await GroupPostService.DeleteGroupPost(id);

        return NoContent();
    }

}
