using System;

namespace LeagueBot.Termination
{
    public class ExitHandler
    {
        public static void ExitSystem(ExitCode exitCode)
        {
            Environment.Exit((int) exitCode);
            //TODO logging (log4j)
        }
    }
}