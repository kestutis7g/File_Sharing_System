
using Forum.Core.Aggregates.Post.Entities;
using Forum.Core.Aggregates.User.Entities;
using Forum.Shared.Interfaces;
using JetBrains.Annotations;

namespace Forum.Core.Aggregates.Comment.Entities
{
    public class CommentEntity : BaseEntity, IAggregateRoot
    {
        public Guid? PostId { get; set; }
        public PostEntity Post { get; set; }
        public Guid? ParentId { get; set; }
        public CommentEntity? ParentComment { get; set; }
        public string Content { get; set; }
        public Guid? UserId { get; set; }
        //public string? UserId { get; set; }
        //public UserEntity? User { get; set; }
        public UserEntity? User { get; set; }
        public bool? Deleted { get; set; }

        public void Update(CommentEntity request)
        {
            PostId = request.PostId ?? PostId;
            ParentId = request.ParentId ?? ParentId;
            Content = request.Content ?? Content;
            UserId = request.UserId ?? UserId;
            Deleted = request.Deleted ?? Deleted;
            ModifiedAt = DateTime.UtcNow;
        }

        public void MarkDeleted()
        {
            Deleted = true;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}
