using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;

namespace EasySchedule
{
    public static class ScheduleExtensions
    {
        public static void UseEasySchedule(this IHost host)
        {
            IServiceProvider services = host.Services;
            ILogger logger = services.GetRequiredService<ILogger<ScheduleManager>>();

            _ = new ScheduleManager(logger);
        }
    }
}
