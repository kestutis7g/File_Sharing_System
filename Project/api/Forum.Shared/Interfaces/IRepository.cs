namespace Forum.Shared.Interfaces;

public interface IRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
{
}
