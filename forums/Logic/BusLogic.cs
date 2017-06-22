using forums.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forums.Logic
{

    //class for the business logic layer
    public class BusLogic
    {
        System system;
        protected myData db;

        public BusLogic(bool init)
        {
            db = new myData();
            if (init)
                system = new System();
        }


        public Dictionary<string, Tuple<string, string>> testFunc()
        {
            // return data.checkIfUserExist("noaschsFDF4@gmail.com");
            return db.getMembersOfForum("1");
        }


        public System ForumsSys
        {
            get { return system; }
        }
    }
}
