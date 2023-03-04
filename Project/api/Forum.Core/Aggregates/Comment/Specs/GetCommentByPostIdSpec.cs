using Ardalis.Specification;
using Forum.Core.Aggregates.Comment.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.Comment.Specs;

public class GetCommentByPostIdSpec : Specification<CommentEntity>
{
    public GetCommentByPostIdSpec(Guid postId)
    {
        Query
            .Where(c => c.PostId == postId)
            .Include(x => x.Post) //cia includina is to pacio
                .ThenInclude(x => x.User) // ce is kitos lenteles i     /jei nori du includint reikia per naujo includint is naujo ir tada then include
            .OrderByDescending(x => x.CreatedAt);

    }
}
