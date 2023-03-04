
using Forum.Core.Aggregates.User.Entities;
using Forum.Shared.Interfaces;

namespace Forum.Core.Aggregates.Group.Entities
{
    public class GroupEntity : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Visibility { get; set; }
        public string? IconPicture { get; set; }
        public string? BackgroundPicture { get; set; }
        public string? Description { get; set; }
        public Guid UserId { get; set; }
        //public string UserId { get; set; }
        public UserEntity User { get; set; }
        //public UserEntity User { get; set; }
        public bool? Deleted { get; set; }
        public bool? Banned { get; set; }

        public void Update(GroupEntity request)
        {
            Name = request.Name ?? Name;
            Visibility = request.Visibility ?? Visibility;
            IconPicture = request.IconPicture ?? IconPicture;
            BackgroundPicture = request.BackgroundPicture ?? BackgroundPicture;
            Description = request.Description ?? Description;
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
}
