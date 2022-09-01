using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooster
{
    public class Program
    {

        public class Tag
        {
            public int Id { get; set; }
            public string User { get; set; }
            public DayOfWeek Day { get; set; }
            public DateTime Date { get; set; }
        }

        static void Main(string[] args)
        {

          
            var Users = new List<string>() { "User A","User B","User C","User D",
            "Mich","Victor Usoro","Paul Odeh","Adeshina Adebayo","Abiodun Adegbuyi","Andrew Ehikioya"};
            var Months = new List<string>();
            var meetingDays = new List<DayOfWeek> { (DayOfWeek)1, (DayOfWeek)3, (DayOfWeek)5 };
            var now = DateTime.Now;
            var response = new List<Tag>();
            var daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            Console.WriteLine("{0} Days",daysInMonth);
            Console.WriteLine(now.Date.Day);
            var random = new Random();
            for (int i = 1; i < daysInMonth + 1; i++)
            {
                var t = new Tag();
                var day = new DateTime(now.Year, now.Month, i);
                var index = random.Next(Users.Count);
                var dayOfWeek = day.DayOfWeek;
                //When it matches any day in meeting days
                if (meetingDays.Contains(dayOfWeek))
                {
                    t.Date = day;
                    t.User = Users[index];
                    t.Day = dayOfWeek;
                    t.Id = i;
                    response.Add(t);
                   
                }
            }
            foreach (var item in response)
            {
                Console.WriteLine("Day: {0} Date : {1}, Cordinator : {2}",item.Day, item.Date, item.User);
               
            }


        }
    }
}
