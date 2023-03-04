using Forum.Core.Aggregates.Group.Entities;
using Forum.Core.Aggregates.User.Entities;
using Forum.Shared.Interfaces;
using System.Xml.Linq;

namespace Forum.Core.Aggregates.GroupUser.Entities;

public class GroupUserEntity : IAggregateRoot
{
    public Guid GroupId { get; set; }
    public GroupEntity Group { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public bool IsAdmin { get; set; }


}

