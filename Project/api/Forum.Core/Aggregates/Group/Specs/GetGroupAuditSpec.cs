using Ardalis.Specification;
using Forum.Core.Aggregates.Group.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.Group.Specs;

public class GetGroupAuditSpec : Specification<GroupEntity>
{
    public GetGroupAuditSpec(Pagination pagination, GetGroupAuditRequest request)
    {
        Query
            .OrderByDescending(x => x.ModifiedAt)
            .Skip(pagination.Skip)
            .Take(pagination.PageSize);
    }
}
