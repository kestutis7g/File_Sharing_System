using Forum.Core.Aggregates.GroupUser.Entities;

namespace Forum.Core.Interfaces;

public interface IGroupUserService
{
    Task<ICollection<GroupUserEntity>> GetGroupUserList();
    Task<GroupUserEntity> GetGroupUserByIds(Guid groupId, Guid userId);
    Task<ICollection<GroupUserEntity>> GetGroupUsersByGroupId(Guid id);
    Task<ICollection<GroupUserEntity>> GetGroupsByUserId(Guid id);
    Task<GroupUserEntity> CreateGroupUser(GroupUserEntity request);
    //Task<GroupUserEntity> UpdateGroupUser(Guid id, GroupUserEntity request);
    Task DeleteGroupUser(GroupUserEntity groupUser);
}
