using Microsoft.AspNetCore.Mvc;
using Forum.Core.Interfaces;
using Forum.Core.Aggregates.Link.Entities;
using Forum.Core.Services;
using Microsoft.EntityFrameworkCore;
using Forum.API.Auth;
using Microsoft.AspNetCore.Authorization;
using System.Security.Permissions;
using NLog.Fluent;
using Forum.API.Auth.Model;
using System.Net;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;
using Forum.API.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Forum.API.Controllers;

[ApiController]
[Route("api/link")]
public class LinkController : BaseController
{
    public LinkController(ILinkService linkService, IFileService fileService)
    {
        LinkService = linkService ?? throw new ArgumentNullException(nameof(linkService));
    }

    private ILinkService LinkService { get; }

    // GET api/link
    [HttpGet]
    public async Task<ActionResult<ICollection<LinkModel>>> GetLinkList()
    {
       var linkList = await LinkService.GetLinkList();

       return Ok(linkList);
    }


    // GET api/link/{id}
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<LinkEntity>> GetLinkById([FromRoute] Guid id, string? password)
    {
        var link = await LinkService.GetLinkById(id, password);
        if (link is null)
        {
            return NotFound();
        }
        return Ok(link);
    }

    // GET api/link/{id}
    [HttpGet("{id:Guid}/passwordRequired")]
    public async Task<ActionResult<LinkEntity>> IsPasswordRequired([FromRoute] Guid id)
    {
        return Ok(await LinkService.IsPasswordRequired(id));
    }


    // POST api/link
    [HttpPost]
    public async Task<ActionResult> CreateLink([FromBody] LinkModel request)
    {
        LinkEntity entity = Mapper.Map<LinkEntity>(request);

        if (request.Password != null)
        {
            entity = await LinkService.HashEntityPassword(Mapper.Map<LinkEntity>(request));
        }

        try
        {
            await LinkService.CreateLink(entity);
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

    // Delete api/link/{id}
    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> DeleteLinkById([FromRoute] Guid id)
    {
       var link = await LinkService.GetLinkById(id);
       if (link is null)
       {
           return NotFound();
       }

       await LinkService.DeleteLink(id);

       return NoContent();
    }

}
