using CourseData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable
{
    public class UserService
    {
        WorkshopEntity courseDB;

        public UserService(WorkshopEntity courseDB)
        {
            this.courseDB = courseDB; 
        }

        public bool IfExist(string email)
        {
            var userCheck = courseDB.Users.Any(u => u.Email.Contains(email));            

            if (userCheck)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void AddUser(UserObject user)
        {
            var userToAdd = new Users()
            {
                Name = user.Name,
                Sername = user.Sername,
                Email = user.Email
            };

            courseDB.Users.Add(userToAdd);
            courseDB.SaveChanges();
        }

        public int ShowId(string email)
        {
            var userCheck = courseDB.Users.Where(u => u.Email==email);
            foreach (var user in userCheck)
            {
                return user.Id;
            }
            return 0;
        }

        public void AddCourse(int UserId)
        {
            Console.WriteLine("Podaj ID wybranych kursow:");
            var courseList = new List<int>();

            while (true)
            {
                try
                {
                    var selector = int.Parse(Console.ReadLine());
                    if (!CheckDateCollision(UserId, selector))
                    {
                        courseList.Add(selector);
                    }
                    else
                    {
                        Console.WriteLine("Kolizja terminow");
                    }
                    
                }
                catch
                {
                    break;
                }
            }

            AddAction(courseList, UserId);
        }

        public void AddAction(List<int> courseList, int UserId)
        {
            var selectCourses = courseDB.Courses.Where(c => courseList.Contains(c.Id));

            var userCourse = new Order()
            {
                UserId = UserId,
                Courses = selectCourses.ToList(),
                CreateDate = DateTime.Now,
            };

            courseDB.Order.Add(userCourse);
            courseDB.SaveChanges();

            foreach(var course in courseList)
            {
                ActPlacesInCourses(course);
            }
        }

        public void ActPlacesInCourses(int CourseId)
        {
                var updatedCourse = courseDB.Courses.Find(CourseId);
                updatedCourse.Places = updatedCourse.Places-1;
                courseDB.Courses.Attach(updatedCourse);
                courseDB.Entry(updatedCourse).State = EntityState.Modified;
                courseDB.SaveChanges();       
        }

        public List<int> ShowUserCourses(int UserId)
        {
            Console.Clear();
            var listOfCourses = new List<int>();
            var selectCourses = courseDB.Order.Include("Courses").Where(o => o.UserId == UserId).SelectMany(c=>c.Courses).Distinct();

            foreach (var courses in selectCourses)
            {
                Console.WriteLine(courses.Id);
                listOfCourses.Add(courses.Id);
            }
            Console.ReadKey();
            return listOfCourses;            
        }

        public List<int> UserCourses(int UserId)
        {
            var listOfCourses = new List<int>();
            var selectCourses = courseDB.Order.Include("Courses").Where(o => o.UserId == UserId).SelectMany(c => c.Courses).Distinct();

            foreach (var courses in selectCourses)
            {
                listOfCourses.Add(courses.Id);
            }
            return listOfCourses;
        }

        public void ShowUsers()
        {
            Console.Clear();
            var users = courseDB.Users.Where(u => u.Name != null);
            foreach(var user in users)
            {
                Console.WriteLine($"{user.Id} / {user.Name} / {user.Sername} / {user.Email}");
            }
        }



        public bool CheckDateCollision (int UserId, int NewCourseID)
        {
            var listOfCourses = UserCourses(UserId);
            var actualCourses = courseDB.Courses.Where(c => listOfCourses.Contains(c.Id));
            var newCourse = courseDB.Courses.FirstOrDefault(c => c.Id==NewCourseID);

            foreach(var course in actualCourses)
            {
                if (course.Name == null) return false;
                if ((newCourse.StartTime > course.StartTime && newCourse.StartTime < course.EndTime) || (newCourse.EndTime > course.StartTime && newCourse.EndTime < course.EndTime))
                {
                    return true;
                }
            }

            return false;
        }


    }
}
