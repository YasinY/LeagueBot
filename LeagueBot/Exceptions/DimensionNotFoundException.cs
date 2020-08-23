using System;
using System.Drawing;

namespace LeagueBot.Exceptions
{
    public class DimensionNotFoundException : Exception
    {
        DimensionNotFoundException(string size) : base($"Dimensions with size {size} not found.")
        {
            
        }
        
    }
}