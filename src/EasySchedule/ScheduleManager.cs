using Cronos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EasySchedule
{
    public class ScheduleManager
    {
        private List<Task> _backgroundTasks = new List<Task>();


        private readonly ILogger _logger;

        public ScheduleManager(ILogger logger)
        {
            _logger = logger;

            _logger.LogInformation("Starting ScheduleManager");

            // Use reflection to find all types that has at least one method with the ScheduleAttribute
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.GetMethods().Any(m => m.GetCustomAttribute(typeof(ScheduleAttribute)) != null));

            foreach (var type in types)
            {
                // Find all methods containing the attribute
                var methods = type.GetMethods()
                    .Where(m => m.GetCustomAttribute(typeof(ScheduleAttribute)) != null);

                // Create instance of the class
                var instance = Activator.CreateInstance(type);

                foreach (var method in methods)
                {
                    // Schedule the method
                    var scheduleAttribute = method.GetCustomAttribute(typeof(ScheduleAttribute));
                    ScheduleMethod((ScheduleAttribute)scheduleAttribute, method, instance);
                }
            }
        }

        private void ScheduleMethod(ScheduleAttribute scheduleAttribute, MethodInfo method, object instance)
        {
            _logger.LogInformation($"Registering method {method.Name}() with cron expression {scheduleAttribute.CronExpression}");

            CronExpression cronExpression = scheduleAttribute.CronExpression;
            if(cronExpression == null)
            {
                _logger.LogError($"Cron expression is null for method {method.Name}()");
                return;
            }


            var task = new Task(async () =>
            {
                while (true)
                {
                    // Calculate the delay until the next occurrence
                    var next = cronExpression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
                    var delay = next - DateTimeOffset.Now;

                    // Wait until the next occurrence
                    await Task.Delay(delay.Value);
                    while (DateTimeOffset.Now < next);

                    // Invoke the method
                    method.Invoke(instance, null);
                }
            });
            task.Start();
            _backgroundTasks.Add(task);
        }
    }
}
