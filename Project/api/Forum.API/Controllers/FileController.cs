using Microsoft.AspNetCore.Mvc;
using Forum.Core.Interfaces;
using Forum.API.Models;
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

namespace Forum.API.Controllers;

[ApiController]
[Route("api/file")]
public class FileController : BaseController
{
    public FileController(IFileService fileService)
    {
        FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
    }

    private IFileService FileService { get; }


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


    // GET api/file/{id}
    [HttpGet("id/{id:Guid}")]
    public async Task<ActionResult<FileEntity>> GetFileById([FromRoute] Guid id)
    {
       var file = await FileService.GetFileById(id);
        if (file is null)
        {
            return NotFound();
        }
        return Ok(file);
    }


    // POST api/file
    [HttpPost]
    public async Task<ActionResult> CreateFile([FromBody] FileModel request)
    {
       try
       {
           await FileService.CreateFile(Mapper.Map<FileEntity>(request));
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

    // POST api/file/uploadfile
    [HttpPost("uploadfile")]
    [Consumes("multipart/form-data")]
    public Task<FileEntity> Save(IFormFile uploadFile)
    {
        
        //if (uploadFile.ContentType.ToLower().StartsWith("image/"))
        // Check whether the selected file is image
        //{
            byte[] b;
            using (BinaryReader br = new BinaryReader(uploadFile.OpenReadStream()))
            {
                b = br.ReadBytes((int)uploadFile.OpenReadStream().Length);
                // Convert the image in to bytes
            }
            Response.StatusCode = 200;

            FileModel request = new FileModel();
            request.Name = uploadFile.FileName;
            request.FileMime = uploadFile.ContentType;
            request.FileBinary = b;
            request.Size = Convert.ToInt32(uploadFile.Length);

        return FileService.CreateFile(Mapper.Map<FileEntity>(request));

        //}
        
    }

    // GET api/file/uploadfile
    [HttpGet("downloadfile/{id:Guid}")]
    public async Task<IActionResult> Download(Guid id)
    {
        var result = new HttpResponseMessage(HttpStatusCode.OK);
        // 1) Get file bytes
        var fileName = "";
        var fileBytes = new byte[0];

        var file = await FileService.GetFileById(id);

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

        return File(file.FileBinary, file.FileMime, file.Name, enableRangeProcessing: true);
        //return File(file.FileBinary, file.FileMime);   preview
    }


    // GET api/file/uploadfile
    [HttpGet("previewfile/{id:Guid}")]
    public async Task<IActionResult> PreviewFile(Guid id)
    {
        var result = new HttpResponseMessage(HttpStatusCode.OK);
        // 1) Get file bytes
        var fileName = "";
        var fileBytes = new byte[0];

        var file = await FileService.GetFileById(id);

        if(file is null)
        {
            return NotFound();
        }

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


        return File(file.FileBinary, file.FileMime, enableRangeProcessing: true);
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
