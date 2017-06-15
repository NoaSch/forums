using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    class SubForum
    {
        private string subject;
        private string  subForumID;

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        Dictionary<string, Discussion> Discussions;

        public string SubForumId
        {
            get { return subForumID; }
            set { subForumID = value; }
        }







    }
}
