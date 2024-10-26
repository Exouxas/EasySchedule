
namespace EasySchedule.Test
{
    public class ScheduleTests
    {
        [Fact]
        public void CronExpressionParserTest()
        {
            // Arrange
            var daySchedule = new ScheduleAttribute("0 0 0 * * *");
            var hourSchedule = new ScheduleAttribute("0 0 * * * *");
            var minuteSchedule = new ScheduleAttribute("0 * * * * *");
            var secondSchedule = new ScheduleAttribute("* * * * * *");

            DateTime from = new(
                year: 2020, 
                month: 1, 
                day: 1, 
                hour: 0, 
                minute: 0, 
                second: 0,
                millisecond: 500,
                DateTimeKind.Utc);
            DateTime to = from.AddDays(2);

            // Act
            var dayOccurrences = daySchedule.CronExpression.GetOccurrences(from, to);
            var hourOccurrences = hourSchedule.CronExpression.GetOccurrences(from, to);
            var minuteOccurrences = minuteSchedule.CronExpression.GetOccurrences(from, to);
            var secondOccurrences = secondSchedule.CronExpression.GetOccurrences(from, to);

            // Assert
            Assert.Equal(2, dayOccurrences.Count());
            Assert.Equal(2 * 24, hourOccurrences.Count());
            Assert.Equal(2 * 24 * 60, minuteOccurrences.Count());
            Assert.Equal(2 * 24 * 60 * 60, secondOccurrences.Count());
        }
    }
}