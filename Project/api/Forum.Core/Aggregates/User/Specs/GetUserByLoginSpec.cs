using Ardalis.Specification;
using Forum.Core.Aggregates.User.Entities;
using Forum.Core.Common;

namespace Forum.Core.Aggregates.User.Specs;

public class GetUserByLoginSpec : Specification<UserEntity>, ISingleResultSpecification
{
    public GetUserByLoginSpec(string login)
    {
        Query
            .Where(c => c.Login == login);
            
    }
}
