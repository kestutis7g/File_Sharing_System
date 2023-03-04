using Forum.Core.Aggregates.User.Entities;
using Forum.Shared.Interfaces;
using System.Text.RegularExpressions;

namespace Forum.Core.Aggregates.Post.Entities;

public class PostEntity : BaseEntity, IAggregateRoot
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Picture { get; set; }
    public string Type { get; set; }
    public Guid UserId { get; set; }
    //public string UserId { get; set; }
    //public UserEntity User { get; set; }
    public UserEntity User { get; set; }
    public bool? Deleted { get; set; }
    public bool? Banned { get; set; }

    public void Update(PostEntity request)
    {
        Title = request.Title ?? Title;
        Content = request.Content ?? Content;
        Picture = request.Picture ?? Picture;
        Type = request.Type ?? Type;
        Deleted = request.Deleted ?? Deleted;
        Banned = request.Banned ?? Banned;

        ModifiedAt = DateTime.UtcNow;
    }

    public void MarkDeleted()
    {
        Deleted = true;
        ModifiedAt = DateTime.UtcNow;
    }
}

