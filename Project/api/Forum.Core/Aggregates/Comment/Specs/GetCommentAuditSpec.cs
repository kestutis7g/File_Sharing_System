using Ardalis.Specification;
using Forum.Core.Aggregates.Comment.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.Comment.Specs;

public class GetCommentAuditSpec : Specification<CommentEntity>
{
    public GetCommentAuditSpec(Pagination pagination, GetCommentAuditRequest request)
    {
        Query
            .OrderByDescending(x => x.ModifiedAt)
            .Skip(pagination.Skip)
            .Take(pagination.PageSize);
    }


}
