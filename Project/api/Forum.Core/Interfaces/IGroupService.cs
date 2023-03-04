using Forum.Core.Aggregates.Group.Entities;

namespace Forum.Core.Interfaces;

public interface IGroupService
{
    Task<ICollection<GroupEntity>> GetGroupList();
    Task<GroupEntity?> GetGroupById(Guid id);
    Task<ICollection<GroupEntity>> GetGroupByName(string name);
    Task<GroupEntity> CreateGroup(GroupEntity request);
    Task<GroupEntity> UpdateGroup(Guid id, GroupEntity request);
    Task DeleteGroup(Guid id);
    Task HardDeleteGroup(Guid id);

}
