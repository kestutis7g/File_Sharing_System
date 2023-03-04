using Microsoft.Extensions.Options;
using Forum.Core.Aggregates.Comment.Entities;
using Forum.Core.Interfaces;
using Forum.Shared.Interfaces;
using Forum.Shared.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Forum.Core.Aggregates.Comment.Specs;
using Microsoft.AspNetCore.Diagnostics;

namespace Forum.Core.Services;

public class CommentService : ICommentService
{
    public CommentService(IRepository<CommentEntity> commentRepo)
    {
        CommentRepo = commentRepo ?? throw new ArgumentNullException(nameof(commentRepo));
    }
    private IRepository<CommentEntity> CommentRepo { get; }


    public async Task<ICollection<CommentEntity>> GetCommentList()
    {
        return await CommentRepo.ListAsync();
    }

    public async Task<CommentEntity?> GetCommentById(Guid id)
    {
        return await CommentRepo.GetByIdAsync(id);
    }
    public async Task<ICollection<CommentEntity>> GetCommentListByPostId(Guid id)
    {
        var spec = new GetCommentByPostIdSpec(id);
        return await CommentRepo.ListAsync(spec);
    }

    public async Task<CommentEntity> CreateComment(CommentEntity request)
    {
        return await CommentRepo.AddAsync(request); 
    }

    public async Task<CommentEntity> UpdateComment(Guid id, CommentEntity request)
    {
        var comment = await GetCommentById(id);
        if(comment is null)
        {
            throw new ArgumentNullException(nameof(comment));
        }
        comment.Update(request);
        await CommentRepo.SaveChangesAsync();
        return comment;

    }

    public async Task DeleteComment(Guid id)
    {
        var comment = await GetCommentById(id);
        if (comment is null)
        {
            throw new ArgumentNullException(nameof(comment));
        }
        comment.MarkDeleted();
        await CommentRepo.SaveChangesAsync();

    }

    public async Task HardDeleteComment(Guid id)
    {
        var comment = await GetCommentById(id);
        if (comment is null)
        {
            throw new ArgumentNullException(nameof(comment));
        }
        await CommentRepo.DeleteAsync(comment);

    }
}
