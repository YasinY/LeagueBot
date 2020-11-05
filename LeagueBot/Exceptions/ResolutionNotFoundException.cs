using System;

namespace LeagueBot.Exceptions
{
    public class ResolutionNotFoundException : Exception
    {
        public ResolutionNotFoundException(string widthAndHeight) : base("Resolution not found. Check the json if dimension " + widthAndHeight + " exists.")
        {
            
        }
    }
}