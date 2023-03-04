using Ardalis.Specification;
using Forum.Core.Aggregates.File.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.File.Specs;

public class GetFileAuditSpec : Specification<FileEntity>
{
    public GetFileAuditSpec(Pagination pagination, GetFileAuditRequest request)
    {
        Query
            .OrderByDescending(x => x.ModifiedAt)
            .Skip(pagination.Skip)
            .Take(pagination.PageSize);
    }


}
