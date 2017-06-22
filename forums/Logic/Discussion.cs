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
        public sender_Obsrver _observer;

        public Discussion(string subject, string subForum) : base(false)
        {
            this.subForumSubject = subForum;
            this.subject = subject;
            messages = new Dictionary<int, Message>();

        }
      


        public void addCreator(Object aMember_creator)
        {
            throw new NotImplementedException();
        }

        public void addMessage(Object aMessage_m)
        {
            throw new NotImplementedException();
        }



        public Member getCreatedBy()
        {
            throw new NotImplementedException();
        }


    }
}
