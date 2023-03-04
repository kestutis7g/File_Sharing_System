using Ardalis.Specification;
using Forum.Core.Aggregates.Group.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.Group.Specs;

public class GetGroupByNameSpec : Specification<GroupEntity>, ISingleResultSpecification
{
    public GetGroupByNameSpec(string name)
    {
        Query
            .Where(c => c.Name == name);
            
    }
}
