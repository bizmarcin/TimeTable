using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable
{
    public class UserObject
    {
        public string Name { get; set; }
        public string Sername { get; set; }
        public string Email { get; set; }

        public UserObject(string Name, string Sername, string Email)
        {
            this.Name = Name;
            this.Sername = Sername;
            this.Email = Email;
        }
            

    }
}

