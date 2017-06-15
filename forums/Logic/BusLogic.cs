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

            myData data;

            public BusLogic()
            {
                data = new myData();
            }


        public bool testFunc()
        {
            return data.checkIfUserExist("noaschsFDF4@gmail.com");
                
        }



}
}
