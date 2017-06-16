using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{
  public  class Discussion
    {

        Dictionary<string, Message> messages;
        private string subject;

        public Discussion(string subject)
        {
            this.subject = subject;
        }
    }
}
