using Forum.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Infrastructure;

public static class InfrastructureSetup
{
    public static void AddAppDbContext(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<DatabaseContext>(options => 
            options.UseSqlServer(connectionString));
}
