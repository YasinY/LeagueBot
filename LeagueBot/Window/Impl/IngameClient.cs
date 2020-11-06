using System.Drawing.Imaging;

using LeagueBot.Helpers.Rectangle;

namespace LeagueBot.Window.Impl
{
    public class IngameClient : BaseWindow
    {
        public IngameClient() : base("League of Legends")
        {
            RectanglePositions = new RectangleFactory(RectanglePositionType.RECTANGLE_POSITIONS_INGAME).Produce(RectangleWidth, RectangleHeight);
        }

        public void SaveBitMap()
        {
            var adBitmap = GetBitmap("ad", ActiveScreenBitmap);
            var apBitmap = GetBitmap("ap", ActiveScreenBitmap);
            adBitmap.Save(@"C:\Users\Yasin\ad.png", ImageFormat.Png);
            apBitmap.Save(@"C:\Users\Yasin\ap.png", ImageFormat.Png);
        }
        
        
    }
}