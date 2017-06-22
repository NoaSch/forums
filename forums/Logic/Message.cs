using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    class Message : BusLogic
    {
        private string parentDiscussion;
        public int msgID;
        public string discussionSubject;
        public string title;
        public string content;
        public DateTime dateCreated;
        public string publisher;
        public string parentMsg;

        //ID, discussionSubject, title, content, dateCreated, publisher

        //message created by user
        public Message(string parentD, string parentM, int msg) : base(false)
        {
            this.parentDiscussion = parentD;
            this.parentMsg = parentM;
            this.msgID = msg;
            /*
            parentDiscussion;
            msgID;
            discussionSubject;
            title;
            content;
            dateCreated;
            publisher;
            parentMsg;
            */
        }

        //message from the db
        public Message(int id, string discussionSubject, string title, string content, DateTime date, string publisher, string commentTo) : base(false)
        {
            this.parentMsg = commentTo;
            this.msgID = id;
            this.discussionSubject = discussionSubject;
            this.title = title;
            this.content = content;
            this.dateCreated = date;
            this.publisher = publisher;
        }

    }
}
