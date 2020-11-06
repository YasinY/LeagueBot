using System.Collections.Generic;
using System.Text.Json;
using LeagueBot.Helpers.Rectangle;
using LeagueBot.Json;

namespace LeagueBot.Mapper
{
    public class DimensionToJsonMapper : IMapper<List<RectanglePositionsModel>, string>
    {
        public List<RectanglePositionsModel> ToSource(string rectangle)
        {
            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new RectanglePositionJsonConverter());
            serializeOptions.WriteIndented = true;
            return JsonSerializer.Deserialize<List<RectanglePositionsModel>>(rectangle, serializeOptions);
        }

        public string ToDestination(List<RectanglePositionsModel> source)
        {
            return JsonSerializer.Serialize(source);
        }
    }
}