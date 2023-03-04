namespace Forum;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ForumExceptionAttribute : Attribute
{
    public int StatusCode { get; }
    public string? ErrorCode { get; }
    public string? Message { get; set; }

    public ForumExceptionAttribute(int statusCode, string errorCode)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }

    public ForumExceptionAttribute(int statusCode, string errorCode, string message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
        Message = message;
    }

    public ForumExceptionAttribute(int statusCode)
    {
        StatusCode = statusCode;
    }

    public ForumExceptionAttribute()
    {
    }
}
