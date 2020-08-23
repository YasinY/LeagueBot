using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using LeagueBot.Model;

namespace LeagueBot.Json
{
    public class RectanglePositionJsonConverter : JsonConverter<RectanglePosition>
    {
        
        public override RectanglePosition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RectanglePosition position = JsonSerializer.Deserialize<RectanglePosition>(ref reader);


            var positionX1 = position.X1;
            if (positionX1 != default && position.X2 == default)
            {
               position.X2 = Math.Abs(positionX1 - 41);
            }

            var positionY2 = position.Y2;
            if (positionY2 != default && position.Y1 == default) 
            {
                position.Y1 = Math.Abs(positionY2 - 14);
            }

            return position;
        }


        public override void Write(Utf8JsonWriter writer, RectanglePosition value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value);
        }
    }
}