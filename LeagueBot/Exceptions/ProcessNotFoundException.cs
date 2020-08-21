using System;

namespace LeagueBot.Exceptions
{
    public class ProcessNotFoundException : Exception
    {

        public ProcessNotFoundException(string processName) : base($"Process \"{processName}\" not found")
        {
            
        }
    }
}