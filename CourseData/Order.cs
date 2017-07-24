﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseData
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set;}

        public virtual Users User { get; set; }
        public virtual ICollection<Courses> Courses { get; set; }

    }
}
