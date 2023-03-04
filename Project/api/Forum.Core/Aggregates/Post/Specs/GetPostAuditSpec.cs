using Ardalis.Specification;
using Forum.Core.Aggregates.Post.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.Post.Specs;

public class GetPostAuditSpec : Specification<PostEntity>
{
    public GetPostAuditSpec(Pagination pagination, GetPostAuditRequest request)
    {
        Query
            .OrderByDescending(x => x.ModifiedAt)
            .Skip(pagination.Skip)
            .Take(pagination.PageSize);
    }
}
