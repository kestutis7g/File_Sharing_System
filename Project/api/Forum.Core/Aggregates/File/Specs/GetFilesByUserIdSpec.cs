using Ardalis.Specification;
using Forum.Core.Aggregates.File.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.File.Specs;

public class GetFilesByUserIdSpec : Specification<FileEntity>, ISingleResultSpecification
{
    public GetFilesByUserIdSpec(Guid id)
    {
        Query
            .Where(c => c.UserId == id);
       
            
    }
}
