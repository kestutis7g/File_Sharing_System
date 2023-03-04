using Forum.Core.Aggregates.Post.Entities;
using System.Collections;

namespace Forum.Core.Interfaces;

public interface IPostService
{
    Task<ICollection<PostEntity>> GetPostList();
    Task<PostEntity?> GetPostById(Guid id);
    Task<PostEntity> CreatePost(PostEntity request);
    Task<PostEntity> UpdatePost(Guid id, PostEntity request);
    Task DeletePost(Guid id);
    Task HardDeletePost(Guid id);
}
