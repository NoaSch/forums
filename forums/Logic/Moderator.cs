using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    public class Moderator : Member
    {
        private Member member;


        public Moderator(Member member) : base(member.Name, member.Password)
        {
        }

       
    }
}
