using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    public class Manager : Member
    {
       

        public Manager(Member member):base(member.Name, member.Password)
        {
            
        }

        public Manager(string name, string pass) : base(name, pass)
        {
        }
    }
}
