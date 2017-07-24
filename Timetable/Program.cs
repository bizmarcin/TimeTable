using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseData;

namespace Timetable
{
    class Program
    {
        static void Main(string[] args)
        {
            var course1 = new Courses()
            {
                Name = "C# dla poczatkujacych",
                Price = 2500,
                StartTime = DateTime.Parse("01.07.2017 15:00:00"),
                EndTime = DateTime.Parse("01.07.2017 17:00:00"),
                Places = 30
            };

            var course2 = new Courses()
            {
                Name = "C# dla zaawansowanych",
                Price = 4500,
                StartTime = DateTime.Parse("01.07.2017 17:00:00"),
                EndTime = DateTime.Parse("01.07.2017 19:00:00"),
                Places = 15
            };

            var course3 = new Courses()
            {
                Name = "JS dla poczatkujacych",
                Price = 2800,
                StartTime = DateTime.Parse("01.07.2017 16:00:00"),
                EndTime = DateTime.Parse("01.07.2017 18:00:00"),
                Places = 20
            };

            using (var courseDB = new WorkshopEntity())
            {

                //var courseToDelete = courseDB.Courses.Where(c => c.Name != null);
                //foreach (var course in courseToDelete)
                //{
                //    courseDB.Courses.Remove(course);
                //}

                //courseDB.Courses.Add(course1);
                //courseDB.Courses.Add(course2);
                //courseDB.Courses.Add(course3);

                //courseDB.SaveChanges();
                while (true)
                {
                    Console.Clear();
                    var menu= new ShowMenu();
                    menu.MenuDrowing();
                    var select = Console.ReadLine();
                    if (select == "4") break;

                    menu.Swich(select,courseDB);
                }
                
            }



        }
    }
}
