using Ardalis.Specification;
using Forum.Core.Aggregates.GroupPost.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.GroupPost.Specs;

public class GetGroupPostByGroupIdSpec : Specification<GroupPostEntity>
{
    public GetGroupPostByGroupIdSpec(Guid groupId)
    {
        Query
            .Where(c => c.GroupId == groupId);

    }
}
