using System.Runtime.Serialization;

namespace Forum.API.Controllers;
[Serializable]
internal class DbEntityValidationException : Exception
{
    public DbEntityValidationException()
    {
    }

    public DbEntityValidationException(string? message) : base(message)
    {
    }

    public DbEntityValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DbEntityValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public object EntityValidationErrors { get; }
}


