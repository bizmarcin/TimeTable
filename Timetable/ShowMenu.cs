using CourseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable
{
    public class ShowMenu
    {
        private WorkshopEntity courseDB;

        public ShowMenu()
        {
            string select;
            WorkshopEntity courseDB;
        }

        public void MenuDrowing()
        {
            Console.WriteLine("1.   Pokaz liste kursow");
            Console.WriteLine("2.   Pokaz kursy dla wybranych uzytkownikow");
            Console.WriteLine("3.   Podlicz faktury");
            Console.WriteLine("4.   Wyjscie");
        }

        public void Swich (string select, WorkshopEntity courseDB)
        {
            switch (select)
            {
                case "1":
                    {
                        Console.Clear();
                        var allCourses = courseDB.Courses.Where(c => c.Name != null);
                        Console.WriteLine($"Id kursu   Nazwa kursu            Poczatek kursu       Koniec kursu           Wolne miejsca   Cena");
                        foreach (var course in allCourses)
                        {
                            Console.WriteLine($"{course.Id}         {course.Name}  {course.StartTime}  {course.EndTime}    {course.Places}               {course.Price}");
                        }

                        Console.WriteLine("Chcesz zapisac sie na kurs? [Y/N]?");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            Console.WriteLine("Podaj imie:");
                            var uName = Console.ReadLine();
                            Console.WriteLine("Podaj nazwisko:");
                            var uSername = Console.ReadLine();
                            Console.WriteLine("Podaj emial:");
                            var uEmail = Console.ReadLine();

                            var user = new UserObject(uName,uSername,uEmail);
                            var userService = new UserService(courseDB);
                            if (!userService.IfExist(user.Email))
                            {
                                userService.AddUser(user);
                            }

                            
                            userService.AddCourse(userService.ShowId(user.Email));

                        }
                        break;
                    }
                case "2":
                    {   
                        var userService = new UserService(courseDB);

                        userService.ShowUsers();
                        Console.WriteLine("Wybieram uzytkownika o ID");
                        var selectedUser = int.Parse(Console.ReadLine());
                        userService.ShowUserCourses(selectedUser);
                        Console.ReadKey();
                        break;
                    }
            }
        }


      
    }    
}
