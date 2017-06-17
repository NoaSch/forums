using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    public class Forum: BusLogic
    {
        Dictionary<string, SubForum> subForums;
        Dictionary<string, Manager> managers;
        Dictionary<string, Member> members;
        private string subject;

       
      

        public Forum(string subject): base(false)
        {
           
            this.subject = subject;
            subForums = new Dictionary<string, SubForum>();
            members = new Dictionary<string, Member>();
            managers = new Dictionary<string, Manager>();

            //add members
            getExistingMembers();


            //add menagers
            getForumMansgers();


            //add subForums
            getExistingSubForums();
        }

        private void getForumMansgers()
        {
            List<string> names = db.getMenagers(subject);
            foreach (string name in names)
            {
                managers.Add(name,new Manager(members[name]));
            }
        }

        private void getExistingSubForums()
        {
           
            Dictionary<string, string> subFList = db.getSubForumsOfForum(subject);
            foreach (KeyValuePair<string,string> pair in subFList)
            {
                subForums.Add(pair.Value,new SubForum(this,pair.Key,pair.Value,true));
                Console.WriteLine("done");
            }

            ///add moderators to subforums 
            ///getSubForumsOfForum
        }

        internal bool addSubForum(string newSubName)
        {
            string newId = (subForums.Count+1).ToString();

            if (db.addSubForum(newId,subject, newSubName))
            {
                subForums.Add(newSubName, new SubForum(this, newId, newSubName,false));
                return true;
            }
            return false;
        }

        internal bool isFreeSubforumSubject(string text)
        {
            if (subForums.ContainsKey(text))
                return false;
            return true;
        }

        internal bool addMember(string username, string password)
        {
            if (db.addMember(subject, username, password))
            {
                members.Add(username, new Member(username, password));
                return true;
            }
            return false;

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




        /*
        private void LoadMembers()
        {
            string path=""; 
            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    Member m = new Member(); 
                    listA.Add(values[0]);
                    listB.Add(values[1]);
                    listC.Add(values[2]);

                }

     */

    }



}

