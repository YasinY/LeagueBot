using System;

namespace LeagueBot.Exceptions
{
    public class InvalidRectangleException : Exception
    {
        public InvalidRectangleException() : base("Invalid Rectangle dimension!")
        {
            
        }
    }
}