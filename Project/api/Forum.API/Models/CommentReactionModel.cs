using System.ComponentModel.DataAnnotations;

namespace Forum.API.Models;

    public class CommentReactionModel
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public Guid ReactionId { get; set; }

    }
