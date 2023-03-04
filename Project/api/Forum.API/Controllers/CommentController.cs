using Microsoft.AspNetCore.Mvc;
using Forum.Core.Interfaces;
using Forum.API.Models;
using Forum.Core.Aggregates.Comment.Entities;
using Forum.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Forum.API.Controllers;

[ApiController]
[Route("api/comment")]
public class CommentController : BaseController
{
    public CommentController(ICommentService commentService)
    {
        CommentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
    }

    private ICommentService CommentService { get; }


    // GET api/comment
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<CommentModel>>> GetCommentList()
    {
        var commentList = await CommentService.GetCommentList();
        //if (commentList is null)
        //{
        //    return NotFound();
        //}
        return Ok(commentList);
    }


    // GET api/comment/{id}
    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<CommentEntity>> GetCommentById([FromRoute] Guid id)
    {
        var comment = await CommentService.GetCommentById(id);
        if (comment is null)
        {
            return NotFound();
        }
        return Ok(comment);
    }
    


    // POST api/comment
    [HttpPost]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> CreateComment([FromBody] CommentModel request)
    {
        try
        {
            await CommentService.CreateComment(Mapper.Map<CommentEntity>(request));
            return Created(nameof(request), request);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(400, ex.InnerException.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // PUT api/comment
    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> UpdateComment([FromRoute] Guid id, [FromBody] CommentModel request)
    {
        try
        {
            await CommentService.UpdateComment(id, Mapper.Map<CommentEntity>(request));
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


    // Delete api/comment/{id}
    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> DeleteCommentById([FromRoute] Guid id)
    {
        var comment = await CommentService.GetCommentById(id);
        if (comment is null)
        {
            return NotFound();
        }

        await CommentService.DeleteComment(id);

        return NoContent();
    }

    // Delete api/comment/{id}
    [HttpDelete("{id:Guid}/hard")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> HardDeleteCommentById([FromRoute] Guid id)
    {
        var comment = await CommentService.GetCommentById(id);
        if (comment is null)
        {
            return NotFound();
        }

        await CommentService.HardDeleteComment(id);

        return NoContent();
    }
}
