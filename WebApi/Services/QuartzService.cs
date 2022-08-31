using Quartz;
using WebApi.Jobs;

namespace WebApi.Services
{
  internal static class QuartzService
    {
        public static void AddServicesQuartz(this WebApplicationBuilder builder)
        {
            builder.Services.AddQuartz(q =>
            {
                q.SchedulerId = "Scheduler-Core";
                q.UseMicrosoftDependencyInjectionJobFactory(options =>
                {
                    options.AllowDefaultConstructor = true;
                });
                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 1;
                });

                var jobKey = new JobKey("DataLoadTriggerKey", "DataLoadTriggerGroup");
                q.AddJob<LoadDataJob>(jobKey, j => j
                    .WithDescription("Start loading data")
                );

                q.AddTrigger(t => t
                    .WithIdentity("DataLoadCronTriggerKey")
                    .ForJob(jobKey)
                    .WithCronSchedule(builder.Configuration["Settings:DownloadCronStartTime"])
                    .WithDescription("Start loading data")
                    .StartNow()
                );
            });

            builder.Services.AddQuartzHostedService(options =>
             {
                 options.WaitForJobsToComplete = true;
             });
        }
    }
}
