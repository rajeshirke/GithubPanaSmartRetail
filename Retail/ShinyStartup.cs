using System;
using Microsoft.Extensions.DependencyInjection;
using Retail.Hepler;
using Shiny;
using Shiny.Jobs;

namespace Retail
{
    public class ShinyStartup : Shiny.ShinyStartup
    {
        public static JobInfo RepeatedJob;
        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            services.UseNotifications();

            var job = new JobInfo(typeof(RepeatedTask))
            {
                Repeat = true,
                PeriodicTime = DateTime.Now.ToLocalTime().TimeOfDay,
                RequiredInternetAccess = InternetAccess.Any
            };
            RepeatedJob = job;
            services.RegisterJob(job);
        }
    }

    
}
