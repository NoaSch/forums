using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    public class SubForum:BusLogic
    {
        private string subId;
        private string subject;
        Dictionary<string, Moderator> moderators;
        Dictionary<string, Discussion> discussions;
        Forum containingForum;
        private string newSubName;

        /*private string key;
private string value;*/

            //ini ttrue for initializing the system
        public SubForum(Forum parentForum,string subId, string subSubject, bool init): base(false)
        {
            this.subId = subId;
            containingForum = parentForum;
            subject = subSubject;
            moderators = new Dictionary<string, Moderator>();
            discussions = new Dictionary<string, Discussion>();

            if (init)
            {
                //get all moderators
                getModerators();


                //get allDiscussions
                getDiscussions();
            }
        }



        public SubForum(string id,string newSubName) : base(false)
        {
            this.subId = id;
            this.newSubName = newSubName;
        }

        //get all discussion from the db
        private void getDiscussions()
        {
            List<string> Discussions = db.getDiscussions(subject);
            foreach (string subjecy in Discussions)
            {
                discussions.Add(subject, new Discussion(subject));
                Console.WriteLine("done");
            }
        }

        //get all moderators from the db
        private void getModerators()
        {
            List<string> modList = db.getModerators(subject);
            foreach (string username in modList)
            {
                moderators.Add(username, new Moderator(containingForum.Members[username]));
                Console.WriteLine("done");
            }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }


        //add moderator to the subforun
        public bool addModerator(string name)
        {
            if (db.addModerator(subId, name))
            {
                Moderator m = new Moderator(containingForum.Members[name]);
                moderators.Add(name, m);
                return true;
            }
            return false;
        }







    }
}
