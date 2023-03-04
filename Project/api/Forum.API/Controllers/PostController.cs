using Microsoft.AspNetCore.Mvc;
using Forum.Core.Interfaces;
using Forum.API.Models;
using Forum.Core.Aggregates.Post.Entities;
using Forum.Core.Services;
using Forum.Core.Aggregates.User.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MySqlX.XDevAPI.Common;

namespace Forum.API.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : BaseController
{
    public PostController(IPostService postService, ICommentService commentService)
    {
        PostService = postService ?? throw new ArgumentNullException(nameof(postService));
        CommentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
    }

    private IPostService PostService { get; }
    public ICommentService CommentService { get; }

    // GET api/post
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<PostModel>>> GetPostList()
    {
        var postList = await PostService.GetPostList();
        //if (postList is null)
        //{
        //    return NotFound();
        //}
        return Ok(postList);
    }

    // GET api/post/{id}/comments
    [HttpGet("{id:Guid}/comments")]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<CommentModel>>> GetCommentListByPostId([FromRoute] Guid id)
    {
        var commentFromRepo = await CommentService.GetCommentListByPostId(id);
        //if (commentFromRepo is null)
        //{
        //    return NotFound();
        //}
        return Ok(commentFromRepo);
    }



    // GET api/post/{id}
    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<PostEntity>> GetPostById([FromRoute] Guid id)
    {
        var post = await PostService.GetPostById(id);
        if (post is null)
        {
            return NotFound();
        }
        return Ok(post);
    }


    // POST api/post
    [HttpPost]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> CreatePost([FromBody] PostModel request)
    {
        try
        {
            var result = await PostService.CreatePost(Mapper.Map<PostEntity>(request));
            var model = Mapper.Map<PostModel>(result);
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

    // PUT api/post
    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> UpdatePost([FromRoute] Guid id, [FromBody] PostModel request)
    {
        try
        {
            await PostService.UpdatePost(id, Mapper.Map<PostEntity>(request));
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


    // Delete api/post/{id}
    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> DeletePostById([FromRoute] Guid id)
    {
        var post = await PostService.GetPostById(id);
        if (post is null)
        {
            return NotFound();
        }

        await PostService.DeletePost(id);

        return NoContent();
    }

    // Delete api/post/{id}
    [HttpDelete("{id:Guid}/hard")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult> HardDeletePostById([FromRoute] Guid id)
    {
        var post = await PostService.GetPostById(id);
        if (post is null)
        {
            return NotFound();
        }
        Console.WriteLine(post.Id);

        await PostService.HardDeletePost(id);

        return NoContent();
    }

}
