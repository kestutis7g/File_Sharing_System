namespace Forum.Core;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTimeOffset ModifiedAt { get; set; } = DateTime.UtcNow;
}
 