using System;

namespace forums.Logic
{
    public abstract class Logger
    {
        private static Logger _instance;

        private static Logger instance()
        {
            throw new NotImplementedException();
        }

        public static void WriteLog(string String_content)
        {
            throw new NotImplementedException();
        }
    }
}