using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Forum.Infrastructure;

public static class QuartzDIExtensions
{
    public static void AddScopedQuartz(this IServiceCollection services)
    {
        services.AddQuartz(x =>
        {
            x.UseMicrosoftDependencyInjectionJobFactory();
        });
        services.AddQuartzServer(options =>
        {
            options.AwaitApplicationStarted = true;
            options.WaitForJobsToComplete = true;
        });
    }

    private static void AddCronJob<TJob>(this IServiceCollectionQuartzConfigurator quartz, string cronExpression, string jobKey) where TJob : IJob
    {
        quartz.AddJob<TJob>(j => j.WithIdentity(jobKey));
        quartz.AddTrigger(t => t.ForJob(jobKey).WithCronSchedule(cronExpression));
    }
}
