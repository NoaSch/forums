using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    public class SubForum:BusLogic
    {
        private string subject;
        Dictionary<string, Moderator> moderators;
        Dictionary<string, Discussion> discussions;
        Forum containingForum;
        /*private string key;
        private string value;*/

        public SubForum(Forum parentForum,string subId, string subSubject, bool init): base(false)
        {
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

        private void getDiscussions()
        {
            List<string> Discussions = db.getDiscussions(subject);
            foreach (string subjecy in Discussions)
            {
                discussions.Add(subject, new Discussion(subject));
                Console.WriteLine("done");
            }
        }

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








    }
}
