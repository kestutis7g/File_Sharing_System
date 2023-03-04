using Ardalis.Specification;

namespace Forum.Shared.Interfaces;

public interface IReadRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
