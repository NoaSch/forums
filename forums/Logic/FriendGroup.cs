using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    class FriendGroup : BusLogic
    {
        string name;
        List<string> members;

        public FriendGroup(string name, List<string> members) : base(false)
        {
            this.name = name;
            this.members = members;
        }



    }
}
