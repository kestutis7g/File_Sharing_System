using Forum.Core.Aggregates.Comment.Entities;
using Forum.Core.Aggregates.File.Entities;
using Forum.Shared.Interfaces;
using JetBrains.Annotations;

namespace Forum.Core.Aggregates.Link.Entities
{
    public class LinkEntity : BaseEntity, IAggregateRoot
    {
        public Guid FileId { get; set; }
        public bool OneTime { get; set; }
        public DateTimeOffset? ExpiryDate { get; set; }
        public string? Password { get; set; }
        public byte[]? Salt { get; set; }

        public void Update(LinkEntity request)
        {
            OneTime = request.OneTime;
            ExpiryDate = request.ExpiryDate ?? ExpiryDate;

            ModifiedAt = DateTime.UtcNow;
        }
    }
}
