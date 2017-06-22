using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums
{
    public interface Observer
    {

         void Update(string aList_ofMembers_recipiants, string aString_content);
    }
}
