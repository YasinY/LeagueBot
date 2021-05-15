using System;

namespace LeagueBot.Exceptions
{
    public class   JsonNotFoundException : Exception
    {

        public JsonNotFoundException(string fileName) : base("json " + fileName + " does not exist!")
        {
            
        }
        
    }
}