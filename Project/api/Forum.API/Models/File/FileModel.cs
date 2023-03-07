using System.ComponentModel.DataAnnotations;

namespace Forum.API.Models.File;

public class FileModel
{
    public string? Name { get; set; }
    public int? Size { get; set; }
    public string? FileMime { get; set; }
    public byte[]? FileBinary { get; set; }
    public string? Location { get; set; }
    public string? Visibility { get; set; }
    public Guid? UserId { get; set; }

}
