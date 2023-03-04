using System.ComponentModel.DataAnnotations;

namespace Forum.API.Models;

public class CommentModel
{
    public Guid? PostId { get; set; }
    public Guid? ParentId { get; set; }


    [StringLength(1024, ErrorMessage = "Length can't be longer than 1024.")]
    public string? Content { get; set; }

    public Guid? UserId { get; set; }


    [Range(typeof(DateTimeOffset), "1950-01-01T00:00:00+00:00", "2200-01-01T00:00:00+00:00", ErrorMessage = "Must be valid date between 1950 and 2200.")]
    public DateTimeOffset? CreatedAt { get; set; }


    [Range(typeof(DateTimeOffset), "1950-01-01T00:00:00+00:00", "2200-01-01T00:00:00+00:00", ErrorMessage = "Must be valid date between 1950 and 2200.")]
    public DateTimeOffset? ModifiedAt { get; set; }


    [Range(typeof(bool), "false", "true", ErrorMessage = "Must be valid boolean.")]
    public bool? Deleted { get; set; }
        
}
