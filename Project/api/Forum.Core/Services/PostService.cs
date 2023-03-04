using Microsoft.Extensions.Options;
using Forum.Core.Aggregates.Post.Entities;
using Forum.Core.Interfaces;
using Forum.Shared.Interfaces;
using Forum.Shared.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Forum.Core.Services;

public class PostService : IPostService
{
    public PostService(IRepository<PostEntity> postRepo)
    {
        PostRepo = postRepo ?? throw new ArgumentNullException(nameof(postRepo));
    }
    private IRepository<PostEntity> PostRepo { get; }


    public async Task<ICollection<PostEntity>> GetPostList()
    {
        return await PostRepo.ListAsync();
    }

    public async Task<PostEntity?> GetPostById(Guid id)
    {
        return await PostRepo.GetByIdAsync(id);
    }

    public async Task<PostEntity> CreatePost(PostEntity request)
    {
        return await PostRepo.AddAsync(request);
    }

    public async Task<PostEntity> UpdatePost(Guid id, PostEntity request)
    {
        var post = await GetPostById(id);
        if(post is null)
        {
            throw new ArgumentNullException(nameof(post));
        }
        post.Update(request);
        await PostRepo.SaveChangesAsync();
        return post;

    }

    public async Task DeletePost(Guid id)
    {
        var post = await GetPostById(id);
        if (post is null)
        {
            throw new ArgumentNullException(nameof(post));
        }
        post.MarkDeleted();
        await PostRepo.SaveChangesAsync();

    }

    public async Task HardDeletePost(Guid id)
    {
        var post = await GetPostById(id);
        if (post is null)
        {
            throw new ArgumentNullException(nameof(post));
        }
        await PostRepo.DeleteAsync(post);

    }
}
