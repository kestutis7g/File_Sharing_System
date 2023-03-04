using Forum.Core.Aggregates.GroupPost.Entities;
using Forum.Core.Interfaces;
using Forum.Shared.Interfaces;
using Forum.Shared.Models;
using Forum.Core.Aggregates.GroupPost.Specs;

namespace Forum.Core.Services;

public class GroupPostService : IGroupPostService
{
    public GroupPostService(IRepository<GroupPostEntity> groupPostRepo)
    {
        GroupPostRepo = groupPostRepo ?? throw new ArgumentNullException(nameof(groupPostRepo));
    }
    private IRepository<GroupPostEntity> GroupPostRepo { get; }


    public async Task<ICollection<GroupPostEntity>> GetGroupPostList()
    {
        return await GroupPostRepo.ListAsync();
    }

    public async Task<ICollection<GroupPostEntity>> GetGroupPostByGroupId(Guid id)
    {
        var spec = new GetGroupPostByGroupIdSpec(id);
        return await GroupPostRepo.ListAsync(spec);
    }

    public async Task<GroupPostEntity> CreateGroupPost(GroupPostEntity request)
    {
        return await GroupPostRepo.AddAsync(request);
    }

    /*public async Task<GroupPostEntity> UpdateGroupPost(Guid id, GroupPostEntity request)
    {
        var groupPost = await GetGroupPostById(id);
        if(groupPost is null)
        {
            throw new ArgumentNullException(nameof(groupPost));
        }
        groupPost.Update(request);
        await GroupPostRepo.SaveChanges();
        return groupPost;

    }*/

    public async Task DeleteGroupPost(Guid id)
    {
        var groupPost = await GetGroupPostByGroupId(id);
        if (groupPost is null)
        {
            throw new ArgumentNullException(nameof(groupPost));
        }
        foreach (var item in groupPost)
        {
            await GroupPostRepo.DeleteAsync(item);
        }
        

    }
}
