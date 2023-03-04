using Forum.Core.Aggregates.GroupUser.Entities;
using Forum.Core.Interfaces;
using Forum.Shared.Interfaces;
using Forum.Shared.Models;
using Forum.Core.Aggregates.GroupUser.Specs;

namespace Forum.Core.Services;

public class GroupUserService : IGroupUserService
{
    public GroupUserService(IRepository<GroupUserEntity> groupUserRepo)
    {
        GroupUserRepo = groupUserRepo ?? throw new ArgumentNullException(nameof(groupUserRepo));
    }
    private IRepository<GroupUserEntity> GroupUserRepo { get; }


    public async Task<ICollection<GroupUserEntity>> GetGroupUserList()
    {
        return await GroupUserRepo.ListAsync();
    }

    public async Task<GroupUserEntity> GetGroupUserByIds(Guid groupId, Guid userId)
    {
        var spec = new GetGroupUserByIdsSpec(groupId, userId);
        return await GroupUserRepo.GetBySpecAsync(spec);
    }
    public async Task<ICollection<GroupUserEntity>> GetGroupUsersByGroupId(Guid id)
    {
        var spec = new GetGroupUsersByGroupIdSpec(id);
        return await GroupUserRepo.ListAsync(spec);
    }
    public async Task<ICollection<GroupUserEntity>> GetGroupsByUserId(Guid id)
    {
        var spec = new GetGroupsByUserIdSpec(id);
        return await GroupUserRepo.ListAsync(spec);
    }

    public async Task<GroupUserEntity> CreateGroupUser(GroupUserEntity request)
    {
        return await GroupUserRepo.AddAsync(request);
    }

    /*public async Task<GroupUserEntity> UpdateGroupUser(Guid id, GroupUserEntity request)
    {
        var groupUser = await GetGroupUserById(id);
        if(groupUser is null)
        {
            throw new ArgumentNullException(nameof(groupUser));
        }
        groupUser.Update(request);
        await GroupUserRepo.SaveChanges();
        return groupUser;

    }*/

    public async Task DeleteGroupUser(GroupUserEntity groupUser)
    {
        if (groupUser is null)
        {
            throw new ArgumentNullException(nameof(groupUser));
        }
        
        await GroupUserRepo.DeleteAsync(groupUser);
    }
}
