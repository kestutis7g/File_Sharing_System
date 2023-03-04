using Forum.Core.Aggregates.Group.Entities;
using Forum.Core.Aggregates.Post.Entities;
using Forum.Shared.Interfaces;
using System.Xml.Linq;

namespace Forum.Core.Aggregates.GroupPost.Entities;

public class GroupPostEntity : IAggregateRoot
{
    public Guid GroupId { get; set; }
    public GroupEntity Group { get; set; }
    public Guid PostId { get; set; }
    public PostEntity Post { get; set; }


}

