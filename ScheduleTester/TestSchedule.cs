using EasySchedule;

namespace ScheduleTester
{
    public class TestSchedule
    {
        private Random r = new();
        private int i;
        public TestSchedule() 
        { 
            i = r.Next(0, 100);
        }


        [Schedule("*/5 * * * * *")]
        public void DoTest()
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.ffff}: It did a thing, and the value is {i}");
        }

        [Schedule("*/6 * * * * *")]
        public void DoOtherTest()
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.ffff}: It did another thing, and this time the value is {i}");
        }
    }
}
