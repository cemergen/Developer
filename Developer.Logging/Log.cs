using log4net;

namespace Developer.Logging
{
    public class Log
    {
        public static readonly ILog Logg =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
