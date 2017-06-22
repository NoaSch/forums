using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{

   public  enum Status
    {
        active,
        inactive
    };

    public class Member : guest
    {
        string name;
        string password;
        Status status;


        public Member(string name, string pass)
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

        public void ChangeStatus(Status status_s, DateTime Date)
        {
            throw new NotImplementedException();
        }
    }
}
