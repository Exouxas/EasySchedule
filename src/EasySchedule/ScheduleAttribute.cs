using Cronos;
using System;

namespace EasySchedule
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ScheduleAttribute : Attribute
    {
        public CronExpression CronExpression { get; }
        public ScheduleAttribute(string cronExpression)
        {
            CronExpression = CronExpression.Parse(cronExpression, CronFormat.IncludeSeconds);
        }
    }
}
