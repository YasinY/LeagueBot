using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using LeagueBot.Dimension;
using LeagueBot.Json;

namespace LeagueBot.Mapper
{
    public class DimensionToJsonMapper : IMapper<List<DimensionModel>, string>
    {
        public List<DimensionModel> ToSource(string rectangle)
        {
            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new RectanglePositionJsonConverter());
            serializeOptions.WriteIndented = true;
            return JsonSerializer.Deserialize<List<DimensionModel>>(rectangle, serializeOptions);
        }

        public string ToDestination(List<DimensionModel> source)
        {
            return JsonSerializer.Serialize(source);
        }
    }
}