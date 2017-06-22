using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    class Message 
    {
        
        
            private String _title;
            private String _content;
            private DateTime _date;
            public Message _prev_message;
            public HashSet<Message> _comment = new HashSet<Message>();
            public Discussion _containing_discussion;
            public Member _sender;
            public Member _recipiant;

            public Message createMessage(Object aString_subject, Object aString_content, Object aString_username)
            {
            throw new NotImplementedException();
        }

            public void addAsComment(Object aMessage_m)
            {
            throw new NotImplementedException();
        }

            public Member getPublisher()
            {
            throw new NotImplementedException();
        }

            public Discussion getDiscussion()
            {
            throw new NotImplementedException();
        }

            public HashSet<FriendGroup> getAllFriendsGroup()
            {
            throw new NotImplementedException();
        }
        

    }
}
