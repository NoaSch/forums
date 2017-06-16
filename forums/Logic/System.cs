using forums.Data;
using System.Collections.Generic;

namespace forums.Logic
{
    public class System:BusLogic
    {
        
        Dictionary<string, Forum> forums;
        
        public System():base(false)
        {
            //the forums by subjects
            forums = new Dictionary<string, Forum>();
            List<string> forumsName = db.getForums();
            foreach (string subject  in forumsName)
            {
                forums.Add(subject, new Forum(subject));
            }
        }


        public Dictionary<string, Forum> Forums { get => forums; set => forums = value; }
    }
}
