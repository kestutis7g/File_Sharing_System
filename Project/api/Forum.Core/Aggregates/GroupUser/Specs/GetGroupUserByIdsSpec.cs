using Ardalis.Specification;
using Forum.Core.Aggregates.GroupUser.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.GroupUser.Specs;

public class GetGroupUserByIdsSpec : Specification<GroupUserEntity>
{
    public GetGroupUserByIdsSpec(Guid groupId, Guid userId)
    {
        Query
            .Where(c => c.GroupId == groupId && c.UserId == userId);

    }
}
