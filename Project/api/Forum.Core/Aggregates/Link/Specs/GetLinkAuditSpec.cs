using Ardalis.Specification;
using Forum.Core.Aggregates.Link.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.Link.Specs;

public class GetLinkAuditSpec : Specification<LinkEntity>
{
    public GetLinkAuditSpec(Pagination pagination, GetLinkAuditRequest request)
    {
        Query
            .OrderByDescending(x => x.ModifiedAt)
            .Skip(pagination.Skip)
            .Take(pagination.PageSize);
    }


}
