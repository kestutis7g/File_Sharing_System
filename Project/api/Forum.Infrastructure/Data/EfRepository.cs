using Ardalis.Specification.EntityFrameworkCore;
using Forum.Shared.Interfaces;

namespace Forum.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}
