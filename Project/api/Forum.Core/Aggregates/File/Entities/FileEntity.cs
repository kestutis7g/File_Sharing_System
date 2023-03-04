using Forum.Core.Aggregates.Comment.Entities;
using Forum.Shared.Interfaces;
using JetBrains.Annotations;

namespace Forum.Core.Aggregates.File.Entities
{
    public class FileEntity : BaseEntity, IAggregateRoot
    {
        public string? Name { get; set; }
        public string? FileMime { get; set; }

        public byte[]? FileBinary { get; set; }

        public int? Size { get; set; }

        public void Update(FileEntity request)
        {
            Name = request.Name ?? Name;
            FileBinary = request.FileBinary ?? FileBinary;
            Size = request.Size ?? Size;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}
