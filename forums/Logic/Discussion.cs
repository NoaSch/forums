using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    public class Discussion : BusLogic
    {

        public string subForumSubject;
        private string subject;
        private Dictionary<int, Message> messages;

        public Discussion(string subject, string subForum) : base(false)
        {
            this.subForumSubject = subForum;
            this.subject = subject;
            messages = new Dictionary<int, Message>();
            //messages.Add("first", new Message(subject, "0", "bla"));
            getAllMessages();
        }
        private void getAllMessages()
        {
            List<Message> msgs = db.getMessages(subject);
            foreach (Message m in msgs)
            {
                messages.Add(m.msgID, m);
            }
        }

        //internal Dictionary<string, Message> getMessages()
        //{
        //    return messages;
        //}
    }
}
