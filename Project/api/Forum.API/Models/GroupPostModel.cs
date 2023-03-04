using System.ComponentModel.DataAnnotations;

namespace Forum.API.Models;

public class GroupPostModel
{
    public Guid GroupId { get; set; }
    public Guid PostId { get; set; }
        
}
