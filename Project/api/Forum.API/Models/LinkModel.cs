using System.ComponentModel.DataAnnotations;

namespace Forum.API.Models;

public class LinkModel
{
    public Guid? FileId { get; set; }

    [Range(typeof(bool), "false", "true", ErrorMessage = "Must be valid boolean.")]
    public bool? OneTime { get; set; }
    public DateTimeOffset? ExpiryDate { get; set; }
    public string? Password { get; set; }
    public byte[]? Salt { get; set; }
}
