using System;
using System.Text.Json;

namespace LeagueBot.Mapper
{
    public class DimensionToJsonMapper : IJsonMapper<Model.Dimension>
    {
     
        public Model.Dimension ToSource(string json)
        {
            return JsonSerializer.Deserialize<Model.Dimension>(json);
        }

        public string ToDestination(Model.Dimension source)
        {
            return JsonSerializer.Serialize(source);
        }
    }
}