using Ardalis.Specification;
using Forum.Core.Aggregates.User.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.User.Specs;

public class GetUserAuditSpec : Specification<UserEntity>
{
    public GetUserAuditSpec(Pagination pagination, GetUserAuditRequest request)
    {
        Query
            .OrderByDescending(x => x.ModifiedAt)
            .Skip(pagination.Skip)
            .Take(pagination.PageSize);
    }
}
