using Microsoft.Extensions.Options;
using Forum.Core.Aggregates.Group.Entities;
using Forum.Core.Interfaces;
using Forum.Shared.Interfaces;
using Forum.Shared.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Forum.Core.Aggregates.Group.Specs;

namespace Forum.Core.Services;

public class GroupService : IGroupService
{
    public GroupService(IRepository<GroupEntity> groupRepo)
    {
        GroupRepo = groupRepo ?? throw new ArgumentNullException(nameof(groupRepo));
    }
    private IRepository<GroupEntity> GroupRepo { get; }


    public async Task<ICollection<GroupEntity>> GetGroupList()
    {
        return await GroupRepo.ListAsync();
    }

    public async Task<GroupEntity?> GetGroupById(Guid id)
    {
        return await GroupRepo.GetByIdAsync(id);
    }
    public async Task<ICollection<GroupEntity>> GetGroupByName(string name)
    {

        var spec = new GetGroupByNameSpec(name);
        return await GroupRepo.ListAsync(spec);
    }

    public async Task<GroupEntity> CreateGroup(GroupEntity request)
    {
        return await GroupRepo.AddAsync(request);
    }

    public async Task<GroupEntity> UpdateGroup(Guid id, GroupEntity request)
    {
        var group = await GetGroupById(id);
        if(group is null)
        {
            throw new ArgumentNullException(nameof(group));
        }
        group.Update(request);
        await GroupRepo.SaveChangesAsync();
        return group;

    }

    public async Task DeleteGroup(Guid id)
    {
        var group = await GetGroupById(id);
        if (group is null)
        {
            throw new ArgumentNullException(nameof(group));
        }
        group.MarkDeleted();
        await GroupRepo.SaveChangesAsync();

    }

    public async Task HardDeleteGroup(Guid id)
    {
        var group = await GetGroupById(id);
        if (group is null)
        {
            throw new ArgumentNullException(nameof(group));
        }
        await GroupRepo.DeleteAsync(group);

    }
}
