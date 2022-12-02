using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

        static int IndexGenerator(int length, int prev, int listLength, Random random)
        {
            var index = random.Next(length);
            if (listLength < 1) return index;
            if (index == prev) return IndexGenerator(length, prev, listLength, random);
            return index;
        }


        static List<Tag> Sorter(List<Tag> list, List<string> allUsers)
        {
            var mostUsed = list.GroupBy(x => x.User).Select(g => new
            {
                Name = g.Key,
                Frequency = g.Count()
            }).OrderByDescending(x => x.Frequency).ToList();
            if(mostUsed.Count() != allUsers.Count())
            {
                var usedUsers = list.Select(v => v.User).ToList();
                var nonUsedUser = allUsers.Except(usedUsers).ToList();
                var counter = nonUsedUser.Count();
                for (int i = 0; i < counter; i++)
                {
                    var nameToRemove = mostUsed[i].Name;
                    var user = list.Where(x => x.User == nameToRemove).LastOrDefault();
                    user.User = nonUsedUser[i];
                    
                }
                mostUsed = list.GroupBy(x => x.User).Select(g => new
                {
                    Name = g.Key,
                    Frequency = g.Count()
                }).OrderByDescending(x => x.Frequency).ToList();
                return list;
            } 
            return list;
        }


        static void Main(string[] args)
        {
            var random = new Random();
            // Line below contains the list of the users that would be used in the Rooster
            var users = new List<string>() { "Immaculate Egwu","Kalu Orji","Elsie Okorochukwu","Abayomi Omosehin",
            "Micheal Madume","Victor Usoro","Paul Odeh","Adesina Adebayo","Abiodun Adegbuyi","Andrew Ehikioya"};
            var Months = new List<string>();
            //Below is where the days of the weeks wehre meetings are held are being selected
            var meetingDays = new List<DayOfWeek> { (DayOfWeek)1, (DayOfWeek)3, (DayOfWeek)5 };
            var now = DateTime.Now;
            var response = new List<Tag>();
            var daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            int prevIndex = 0;
            for (int i = 1; i < daysInMonth + 1; i++)
            {
                var t = new Tag();
                var day = new DateTime(now.Year, now.Month, i);
                var index = IndexGenerator(users.Count, prevIndex, response.Count, random);
                var dayOfWeek = day.DayOfWeek;
                //When it matches any day in meeting days
                if (meetingDays.Contains(dayOfWeek))
                {
                    t.Date = day;
                    t.User = users[index];
                    t.Day = dayOfWeek;
                    t.Id = i;
                    t.Current = index;
                    t.Prev = prevIndex;
                    response.Add(t);
                    prevIndex = index;
                }
            }
            //foreach (var item in response)
            //{
            //    Console.WriteLine("Day: {0} Date : {1}, Cordinator : {2}", item.Day, item.Date, item.User);
            //}
            var usedUsers = response.Select(v => v.User).ToList();
            var nonUsedUser = users.Except(usedUsers);
            Console.WriteLine(nonUsedUser.Count());
            foreach (var user in nonUsedUser) Console.WriteLine(user);
            var res = Sorter(response, users);
            foreach (var item in res)
            {
                Console.WriteLine("Day: {0} Date : {1}, Cordinator : {2}", item.Day, item.Date, item.User);
            }
            usedUsers = res.Select(v => v.User).ToList();
            nonUsedUser = users.Except(usedUsers);
            Console.WriteLine(nonUsedUser.Count());

            var path = @"c:\Users\Shenor\Desktop\CSV\csv.csv";
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(res);
            }

            //The Except() method requires two collections. It returns a new collection with elements from the first
            //collection which do not exist in the second collection(parameter collection).


            //Check first, if a user has already appeared two times, it should not even add the person again in the first list 

        }
    }
}
