using Ardalis.Specification;
using Forum.Core.Aggregates.GroupUser.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.GroupUser.Specs;

public class GetGroupsByUserIdSpec : Specification<GroupUserEntity>
{
    public GetGroupsByUserIdSpec(Guid userId)
    {
        Query
            .Where(c => c.UserId == userId);

    }
}
