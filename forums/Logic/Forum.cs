using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
    public class Forum
    {


        private string subject;
        private string forumId; 


        public Forum()
        {
            //LoadMembers(); 



        }
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }


        public string ForumId
        {
            get { return forumId; }
            set { forumId = value; }
        }

        Dictionary<string, SubForum> SubForums;


        /*
        private void LoadMembers()
        {
            string path=""; 
            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    Member m = new Member(); 
                    listA.Add(values[0]);
                    listB.Add(values[1]);
                    listC.Add(values[2]);

                }

     */

    }



}

