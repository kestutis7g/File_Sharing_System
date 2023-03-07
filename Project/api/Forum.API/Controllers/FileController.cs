using Microsoft.AspNetCore.Mvc;
using Forum.Core.Interfaces;
using Forum.Core.Aggregates.File.Entities;
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
using Forum.API.Models.File;

namespace Forum.API.Controllers;

[ApiController]
[Route("api/file")]
public class FileController : BaseController
{
    public FileController(IFileService fileService, ILinkService linkService)
    {
        FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        LinkService = linkService ?? throw new ArgumentNullException(nameof(linkService));
    }

    private IFileService FileService { get; }
    private ILinkService LinkService { get; }

    // GET api/file
    [HttpGet]
    public async Task<ActionResult<ICollection<FileModel>>> GetFileList()
    {
       var fileList = await FileService.GetFileList();
       //if (fileList is null)
       //{
       //    return NotFound();
       //}
       return Ok(fileList);
    }

    // GET api/file
    [HttpGet("user/{id:Guid}")]
    public async Task<ActionResult<ICollection<FileModel>>> GetUserFiles([FromRoute] Guid id)
    {
        var fileList = await FileService.GetUserFiles(id);
        //if (fileList is null)
        //{
        //    return NotFound();
        //}
        return Ok(fileList);
    }

    // POST api/file/upload
    [HttpPost]
    //[Consumes("multipart/form-data")]
    public Task<FileEntity> Upload([FromForm] FileViewModel uploadFile)
    {
        byte[] b;
        using (BinaryReader br = new BinaryReader(uploadFile.File.OpenReadStream()))
        {
            b = br.ReadBytes((int)uploadFile.File.OpenReadStream().Length);
            // Convert the image in to bytes
        }
        Response.StatusCode = 200;

        FileModel request = new FileModel();
        request.Name = uploadFile.File.FileName;
        request.FileMime = uploadFile.File.ContentType;
        request.FileBinary = b;
        request.Size = Convert.ToInt32(uploadFile.File.Length);

        request.Location = uploadFile.Location;
        request.Visibility = uploadFile.Visibility;
        request.UserId = uploadFile.UserId;

        return FileService.CreateFile(Mapper.Map<FileEntity>(request));

    }

    // GET api/file/{id:Guid}
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Download(Guid id, string action = "preview")
    {
        var file = await FileService.GetFileById(id);

        if (file is null)
        {
            return NotFound();
        }
  

        if (action == "download")
        {
            return File(file.FileBinary, file.FileMime, file.Name, enableRangeProcessing: true);
        }
        else
        {
            return File(file.FileBinary, file.FileMime, enableRangeProcessing: true);
        }
    }

    // GET api/file/link/{id:Guid}
    [HttpGet("link/{id:Guid}")]
    public async Task<IActionResult> OpenLink(Guid id, string? password, string action = "preview")
    {
        var link = await LinkService.GetLinkById(id, password);
        if (link is null)
        {
            return NotFound();
        }



        var result = new HttpResponseMessage(HttpStatusCode.OK);
        // 1) Get file bytes
        var fileName = "";
        var fileBytes = new byte[0];

        var file = await FileService.GetFileById(link.FileId);

        fileBytes = file.FileBinary;

        if (fileBytes.Length == 0)
        {
            result.StatusCode = HttpStatusCode.NotFound;
        }
        else
        {
            // 2) Add bytes to a memory stream
            var fileMemStream =
                new MemoryStream(fileBytes);

            // 3) Add memory stream to response
            result.Content = new StreamContent(fileMemStream);

            // 4) build response headers
            var headers = result.Content.Headers;

            headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment");
            headers.ContentDisposition.FileName = fileName;

            headers.ContentType =
                //new MediaTypeHeaderValue("application/jpg");
                new MediaTypeHeaderValue("application/octet-stream");

            headers.ContentLength = fileMemStream.Length;
        }

        if (link.OneTime)
        {
            await LinkService.DeleteLink(link.Id);
        }

        if (action == "download")
        {
            return File(file.FileBinary, file.FileMime, file.Name, enableRangeProcessing: true);
        }
        else
        {
            return File(file.FileBinary, file.FileMime, enableRangeProcessing: true);
        }

    }

    // PUT api/file
    [HttpPut("{id:Guid}")]
    public async Task<ActionResult> UpdateFile([FromRoute] Guid id, [FromBody] FileModel request)
    {
       try
       {
           await FileService.UpdateFile(id, Mapper.Map<FileEntity>(request));

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


    // Delete api/file/{id}
    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> DeleteFileById([FromRoute] Guid id)
    {
       var file = await FileService.GetFileById(id);
       if (file is null)
       {
           return NotFound();
       }

       await FileService.DeleteFile(id);

       return NoContent();
    }

    // Delete api/file/{id}
    [HttpDelete("{id:Guid}/hard")]
    public async Task<ActionResult> HardDeleteCommentById([FromRoute] Guid id)
    {
       var file = await FileService.GetFileById(id);
       if (file is null)
       {
           return NotFound();
       }

       await FileService.HardDeleteFile(id);

       return NoContent();
    }
}
