using Forum.Core.Aggregates.Comment.Entities;

namespace Forum.Core.Interfaces;


public interface ICommentService
{
    Task<ICollection<CommentEntity>> GetCommentList();
    Task<CommentEntity?> GetCommentById(Guid id);
    Task<ICollection<CommentEntity>> GetCommentListByPostId(Guid id);
    Task<CommentEntity> CreateComment(CommentEntity request);
    Task<CommentEntity> UpdateComment(Guid id, CommentEntity request);
    Task DeleteComment(Guid id);
    Task HardDeleteComment(Guid id);

}
