using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    class FriendGroup : BusLogic
    {
        //public string forum;
        public string name;
        public List<Member> members;

        public FriendGroup(string name, List<Member> members) : base(false)
        {
            //this.forum = forum;
            this.name = name;
            this.members = members;
        }



    }
}
