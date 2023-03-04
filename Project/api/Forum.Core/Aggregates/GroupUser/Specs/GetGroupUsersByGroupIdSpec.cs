using Ardalis.Specification;
using Forum.Core.Aggregates.GroupUser.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.GroupUser.Specs;

public class GetGroupUsersByGroupIdSpec : Specification<GroupUserEntity>
{
    public GetGroupUsersByGroupIdSpec(Guid groupId)
    {
        Query
            .Where(c => c.GroupId == groupId);

    }
}
