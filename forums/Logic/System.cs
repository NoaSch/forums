using forums.Data;
using System;
using System.Collections.Generic;

namespace forums.Logic
{
    public class System : BusLogic
    {

        Dictionary<string, Forum> forums;

        public System() : base(false)
        {
            //the forums by subjects
            forums = new Dictionary<string, Forum>();
            List<string> forumsName = db.getForums();
            foreach (string subject in forumsName)
            {
                forums.Add(subject, new Forum(subject));
            }
        }


        public Dictionary<string, Forum> Forums
        {
            get { return forums; }
            set { forums = value; }
        }

        public void find_moderator_to_suspend()
        {
            throw new NotImplementedException();
        }

        public void addForum(string subject)
        {
            throw new NotImplementedException();
        }

        public Forum getForumBySubject(string subject)
        {
            if(forums.ContainsKey(subject))
                return forums[subject];
            return null;
        }
    }
}

