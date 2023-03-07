using Forum.Core.Aggregates.Comment.Entities;
using Forum.Core.Aggregates.User.Entities;
using Forum.Shared.Interfaces;
using JetBrains.Annotations;

namespace Forum.Core.Aggregates.File.Entities
{
    public class FileEntity : BaseEntity, IAggregateRoot
    {
        public string? Name { get; set; }
        public int? Size { get; set; }
        public string? FileMime { get; set; }
        public string? Location { get; set; }
        public string? Visibility { get; set; }
        public byte[]? FileBinary { get; set; }
        public Guid? UserId { get; set; }
        public UserEntity? User { get; set; }


        public void Update(FileEntity request)
        {
            Name = request.Name ?? Name;
            Location= request.Location ?? Location;
            Visibility = request.Visibility ?? Visibility;

            ModifiedAt = DateTime.UtcNow;
        }
    }
}
