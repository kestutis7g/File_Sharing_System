using System.ComponentModel.DataAnnotations;

namespace Forum.API.Models.File;

public class FileViewModel
{
    public string? Location { get; set; }
    public string? Visibility { get; set; }

    public IFormFile? File { get; set; }



}
