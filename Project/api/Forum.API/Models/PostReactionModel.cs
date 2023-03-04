using System.ComponentModel.DataAnnotations;

namespace Forum.API.Models;

public class PostReactionModel
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public Guid ReactionId { get; set; }

}
