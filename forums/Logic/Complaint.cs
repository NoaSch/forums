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
            this.date = DateTime.Now;
        }


        public void AddSender(Member member)
        {
            this.complaintant = member;
        }

        public void AddRecipiant(Member member)
        {
            this.complaintSubject = member;
        }

        public void AddSubForum(SubForum sb)
        {
            this.subForum = sb;
        }

        public DateTime Date
        {
            get { return date; }

        }
        public string Content
        {
            get { return content; }

        }
        public SubForum SubForum
        {
            get { return subForum; }

        }
        public Member Complaintant
        {
            get { return complaintant; }

        }

        public Member Recipiant
        {
            get { return complaintSubject; }

        }


    }

}