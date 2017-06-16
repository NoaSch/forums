using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{

    enum Status
    {
        active,
        inactive
    };

    public class Member
    {
        string name;
        string password;
        Status status; 


        public Member(string name,string pass)
        {
            this.name = name;
            this.password = pass;
            this.status = Status.active;
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Password
        {
            get { return password; }
           
        }
    }
}
