
namespace Forum.Core.Common;

public class Pagination
{
    public Pagination()
    {

    }

    public Pagination(int skip, int pageSize)
    {
        Skip = skip;
        PageSize = pageSize;
    }

    public int Skip { get; set; }
    public int PageSize { get; set; }

    public static Pagination None() => new(0, 1000);
}

public interface IPaginatedCollection<T>
{
    ICollection<T> Data { get; }
    int Total { get; }
}

public class PaginatedList<T> : IPaginatedCollection<T>
{
    public PaginatedList()
    {

    }

    public PaginatedList(IEnumerable<T> items)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        foreach (T item in items)
        {
            Data.Add(item);
        }

        Total = Data.Count;
    }

    public PaginatedList(IEnumerable<T> items, int total)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        foreach (T item in items)
        {
            Data.Add(item);
        }

        Total = total;
    }

    public ICollection<T> Data { get; set; } = new List<T>();
    public int Total { get; set; }
}
