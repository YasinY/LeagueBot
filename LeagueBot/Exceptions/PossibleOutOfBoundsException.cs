using System;

namespace LeagueBot.Exceptions
{
    public class PossibleOutOfBoundsException : Exception
    {
        public PossibleOutOfBoundsException(int width, int height) : base(
            $"Rectangles Width or Height are possibly out of bounds! ({width.ToString()}x{height.ToString()})")
        {
            
        }
        
    }
}