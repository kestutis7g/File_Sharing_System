using Forum.Core.Aggregates.GroupPost.Entities;

namespace Forum.Core.Interfaces;

public interface IGroupPostService
{
    Task<ICollection<GroupPostEntity>> GetGroupPostList();
    Task<ICollection<GroupPostEntity>> GetGroupPostByGroupId(Guid id);
    Task<GroupPostEntity> CreateGroupPost(GroupPostEntity request);
    //Task<GroupPostEntity> UpdateGroupPost(Guid id, GroupPostEntity request);
    Task DeleteGroupPost(Guid id);
}
