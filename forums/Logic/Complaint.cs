using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    class Complaint
    {
        DateTime date;
        string content;
        SubForum subForum;
        Member complaintSubject;
        Member complaintant;


        public Complaint(string content)
        {
            this.content = content;
        }


        public void AddSender(Member member)
        {
            this.complaintant = member;
        }

        public void AddRecipiant(Member member)
        {
            this.complaintSubject = member;
        }
    }

}