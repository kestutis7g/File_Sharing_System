using Forum.Core.Aggregates.Post.Entities;
using Forum.Shared.Interfaces;

namespace Forum.Core.Aggregates.User.Entities;

public class UserEntity : BaseEntity, IAggregateRoot
{
    public string Login { get; set; }
    public string? Password { get; set; }
    public byte[]? Salt { get; set; }
    public string Role { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public string? FileMime { get; set; }
    public string? Description { get; set; }
    public bool? Deleted { get; set; }
    public bool? Banned { get; set; }

    public void Update(UserEntity request)
    {
        Login = request.Login ?? Login;
        Name = request.Name ?? Name;
        Lastname = request.Lastname ?? Lastname;
        Email = request.Email ?? Email;
        Phone = request.Phone ?? Phone;
        Description = request.Description ?? Description;
        Deleted = request.Deleted ?? Deleted;
        Banned = request.Banned ?? Banned;

        ModifiedAt = DateTime.UtcNow;
    }

    public void UpdateProfilePicture(UserEntity request)
    {
        ProfilePicture = request.ProfilePicture ?? ProfilePicture;
        FileMime = request.FileMime ?? FileMime;

        ModifiedAt = DateTime.UtcNow;
    }

    public void MarkDeleted()
    {
        Deleted = true;
        ModifiedAt = DateTime.UtcNow;
    }
}

