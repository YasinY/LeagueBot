using System.Drawing;
using LeagueBot.Exceptions;
using LeagueBot.Model;

namespace LeagueBot.Mapper
{
    public class RectanglePositionToRectangleMapper : IMapper<RectanglePosition, Rectangle>
    {
        public RectanglePosition ToSource(Rectangle rectangle)
        {
            return new RectanglePosition(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
        }

        public Rectangle ToDestination(RectanglePosition source)
        {
            var rectangle =
                Rectangle.FromLTRB((int) source.X1, (int) source.Y2, (int) source.X2, (int) source.Y1);

            return rectangle;
        }
    }
}