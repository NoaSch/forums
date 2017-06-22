using System;
using System.Collections.Generic;

namespace forums.Logic
{
    public class sender_Obsrver:Observer
    {

    public Discussion _observable;

        public void Update(string aList_ofMembers_recipiants, string aString_content)
        {
            throw new NotImplementedException();
        }

        public void Update(List<Member> recipiants, string content)
    {
            throw new NotImplementedException();
        }
}
}