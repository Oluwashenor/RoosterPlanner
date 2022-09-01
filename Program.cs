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
            public int Prev { get; set; }
            public int Current { get; set; }
            public string User { get; set; }
            public DayOfWeek Day { get; set; }
            public DateTime Date { get; set; }
        }

        static int IndexGenerator(int length, int prev,int listLength)
        {
            var random = new Random();
            var index = random.Next(length);
            if (listLength < 1)
            {
                return index;
            }
            if(index == prev)
            {
                return IndexGenerator(length, prev, listLength);
                
            }
            return index;
        }


        static void Main(string[] args)
        {
            var Users = new List<string>() { "Immaculate Egwu","Kalu Orji","Elsie Okorochukwu","Abayomi Omosehin",
            "Micheal Madume","Victor Usoro","Paul Odeh","Adesina Adebayo","Abiodun Adegbuyi","Andrew Ehikioya"};
            var Months = new List<string>();
            var meetingDays = new List<DayOfWeek> { (DayOfWeek)1, (DayOfWeek)3, (DayOfWeek)5 };
            var now = DateTime.Now;
            var response = new List<Tag>();
            var daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            int prevIndex = 0;
            for (int i = 1; i < daysInMonth + 1; i++)
            {
                var t = new Tag();
                var day = new DateTime(now.Year, now.Month, i);
                var index = IndexGenerator(Users.Count, prevIndex, response.Count);
                var dayOfWeek = day.DayOfWeek;
                //When it matches any day in meeting days
                if (meetingDays.Contains(dayOfWeek))
                {
                    t.Date = day;
                    t.User = Users[index];
                    t.Day = dayOfWeek;
                    t.Id = i;
                    t.Current = index;
                    t.Prev = prevIndex;
                    response.Add(t);
                    prevIndex = index;
                }
            }
            foreach (var item in response)
            {
                Console.WriteLine("Day: {0} Date : {1}, Cordinator : {2}",item.Day, item.Date, item.User);
            }


        }
    }
}
