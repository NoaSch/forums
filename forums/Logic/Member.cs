using forums.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{

    public enum Status
    {
        active,
        inactive
    };

    public class Member : guest
    {
        string name;
        string password;
        HashSet<FriendGroup> friendGroups;


        public Member(string name,string pass)
        {
            this.name = name;
            this.password = pass;
            this.status = Status.active;
            friendGroups = new HashSet<FriendGroup>();
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
        internal void addFriendGroup(FriendGroup fg)
        {
            friendGroups.Add(fg);
        }
        }
    }
}
