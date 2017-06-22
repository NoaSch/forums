using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    public class Forum : BusLogic
    {
        Dictionary<string, SubForum> subForums;
        Dictionary<string, Manager> managers;
        Dictionary<string, Member> members;
        private string subject;
        List<Complaint> complaints;
        HashSet<FriendGroup> allFriendsGroups;


        public Forum(string subject) : base(false)
        {

            this.subject = subject;
            subForums = new Dictionary<string, SubForum>();
            members = new Dictionary<string, Member>();
            managers = new Dictionary<string, Manager>();
            complaints = new List<Complaint>();
            allFriendsGroups = new HashSet<FriendGroup>();

            //add members
            getExistingMembers();
            //add menagers
            getForumMansgers();
            //add subForums
            getExistingSubForums();
            //add friends groups
            getForumGroups();
        }

        private void getForumMansgers()
        {
            List<string> names = db.getMenagers(subject);
            foreach (string name in names)
            {
                managers.Add(name, new Manager(members[name]));
            }
        }

        private void getExistingSubForums()
        {
            Dictionary<string, string> subFList = db.getSubForumsOfForum(subject);
            foreach (KeyValuePair<string, string> pair in subFList)
            {
                subForums.Add(pair.Value, new SubForum(this, pair.Key, pair.Value, true));
                Console.WriteLine("done");
            }

            ///add moderators to subforums 
            ///getSubForumsOfForum
        }

        internal string getNextSubID()
        {
            return (subForums.Count + 1).ToString();
        }

        private void getForumGroups()
        {
            Dictionary<string, List<string>> toConvert = db.getFriendsGroupsOfForum(subject);
            foreach (KeyValuePair<string, List<string>> item in toConvert)
            {
                List<Member> newMembersList = new List<Member>();
                foreach (string usernm in item.Value)
                {
                    Member m;
                    this.members.TryGetValue(usernm, out m);
                    newMembersList.Add(m);
                }
                FriendGroup fg = new FriendGroup(item.Key, newMembersList);
                allFriendsGroups.Add(fg);
            }
        }




        internal bool addSubForum(string newId, string newSubName, List<string> newModeratorsID)
        {
            // string newId = (subForums.Count+1).ToString();

            if (db.addSubForum(newId, subject, newSubName))
            {
                SubForum newSB = new SubForum(this, newId, newSubName, false);
                foreach (string mID in newModeratorsID)
                {
                    Member mem = getMember(mID);
                    Moderator mod = new Moderator(mem);
                    newSB.addModerator(mod);

                }
                addToSubForum(newSB);
                //subForums.Add(newSubName,;
                return true;
            }
            return false;
        }

        internal bool createNewFriendsGroup(string name, string user)
        {
            //check for uniqe group name
            foreach (FriendGroup item in allFriendsGroups)
            {
                if (item.name == name)
                {
                    return false;
                }
            }
            //creating friend group object and adding it to db & to dictionary
            List<Member> member = new List<Member>();
            Member m = this.getMember(user);
            member.Add(m);
            FriendGroup fg = new FriendGroup(name, member);
            if (db.addFriendsGroup(fg, this.subject)) //adding to FriendGroup table
            {
                if (db.addFriendsGroupMembers(fg, this.subject)) //adding to FriendsGroupMembers table
                {
                    allFriendsGroups.Add(fg);
                    return true;
                }
            }
            return false;
        }

        private void addToSubForum(SubForum newSB)
        {
            subForums.Add(newSB.Subject, newSB);
        }

        public Member getMember(string mID)
        {
            if (members.ContainsKey(mID))
                return members[mID];
            return null;
        }

        internal bool isFreeSubforumSubject(string text)
        {
            if (subForums.ContainsKey(text))
                return false;
            return true;
        }

        internal bool addUser(string username, string password)
        {
            bool isValid = isUserNameFree(username);
            if (isValid)
            {
                if (db.addMember(subject, username, password))
                {
                    Member m = new Member(username, password);
                    addMember(m);
                    //members.Add(username,m);
                    return true;
                }
            }
            return false;

        }

        private void addMember(Member m)
        {
            members.Add(m.Name, m);
        }

        private void getExistingMembers()
        {
            Dictionary<string, Tuple<string, string>> membersList = db.getMembersOfForum(subject);
            foreach (string key in membersList.Keys)
            {
                members.Add(key, new Member(key, membersList[key].Item1));
                Console.WriteLine("done");
            }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }


        public string ForumSubject
        {
            get { return subject; }
            set { subject = value; }
        }

        public Dictionary<string, Member> Members
        {
            get { return members; }

        }

        public Dictionary<string, Manager> Managers
        {
            get { return managers; }

        }

        public Dictionary<string, SubForum> SubForums
        {
            get { return subForums; }

        }

        public bool isUserNameFree(string username)
        {
            if (members.ContainsKey(username))
                return false;
            return true;
        }

        public bool addComplaint(string senderUserName, string complaintAboutUserNmae, string content, string subforumSubject)
        {
            Member recipiant = getMember(complaintAboutUserNmae);
            Member sender = getMember(senderUserName);
            Complaint c = new Complaint(content);
            c.AddSender(sender);
            c.AddRecipiant(recipiant);

            if (subforumSubject != "")
            {
                SubForum sb = getSubForum(subforumSubject);
                c.AddSubForum(sb);
            }

            complaints.Add(c);

            if (db.addComplaint(c))
            {
                return true;
            }
            return false;
        }

        public SubForum getSubForum(string subforumSubject)
        {
            if (subForums.ContainsKey(subforumSubject))
                return subForums[subforumSubject];
            return null;
        }
      

    }



}

