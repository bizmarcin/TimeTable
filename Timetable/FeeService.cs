using CourseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable
{
    public class FeeService
    {
        WorkshopEntity courseDB;

        public FeeService()
        {
            this.courseDB = courseDB;
        }
        
        public void StartFeeProcess(int OrderId)
        {
            Console.Clear();
            Console.WriteLine("Podaj numer zamowienia:");
            var orderId = int.Parse(Console.ReadLine());
            var orderPriceStruct = courseDB.Courses.Where(c => c.Price != null);
            decimal totalPrice = 0;
            foreach(var course in orderPriceStruct)
            {
                totalPrice = totalPrice + course.Price;
            }

            totalPrice = totalPrice - CountCoursesInOrder(OrderId)*totalPrice;
        }

        public decimal CompareDates(int OrderId, decimal totalPrice)
        {
            var coursesList = new List<int>();
            var coursesOrders = courseDB.Order.Where(o => o.Id == OrderId);
            foreach (var course in coursesOrders)
            {
                //if(DateTime.Now- course.CreateDate>>
                //totalPrice=totalPrice
            }


            return totalPrice;
        }

        public decimal CountCoursesInOrder(int OrderId)
        {
            var coursesOrders = courseDB.Order.Where(o => o.Id == OrderId).Count();
            decimal discout = 0;
            if (coursesOrders >= 1 || coursesOrders < 2) discout = 0.05m;
            if (coursesOrders >= 2 || coursesOrders < 3) discout = 0.10m;
            if (coursesOrders >= 3) discout = 0.15m;
            return discout;
        }
    }
}
