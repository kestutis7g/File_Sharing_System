using System.ComponentModel.DataAnnotations;

namespace Forum.API.Models;

public class FileModel
{
    public string? Name { get; set; }
    public string? FileMime { get; set; }

    public byte[]? FileBinary { get; set; }

    public int ? Size { get; set; }
        
}
