using System;
using System.Text.Json.Serialization;
using LeagueBot.Json;
using LeagueBot.Loader;

namespace LeagueBot.Model
{ //TODO unfortunately c# doesnt have extension variable support *yet*
    public class RectanglePosition
    {
        [JsonPropertyName("identifier")] public string Identifier { get; set; }

        [JsonPropertyName("x1")] public long X1 { get; set; }

        [JsonPropertyName("y2")] public long Y2 { get; set; }

        [JsonPropertyName("x2")] public long X2 { get; set; }

        [JsonPropertyName("y1")] public long Y1 { get; set; }

        public RectanglePosition()
        {
            
        }

        public RectanglePosition(int x1, int y2, int x2, int y1)
        {
            X1 = x1;
            Y2 = y2;
            X2 = x2;
            Y1 = y1;
        }
        
        public long CalculateX2()
        {
            return Math.Abs(X1 + 41);

        }
        public long CalculateY1()
        {
            return Math.Abs(Y2 + 14);
        }
        
    }
}