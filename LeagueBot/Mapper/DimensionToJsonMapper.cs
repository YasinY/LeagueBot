using System;
using System.Text.Json;

namespace LeagueBot.Mapper
{
    public class DimensionToJsonMapper : IJsonMapper<Model.Dimension>
    {
     
        public Model.Dimension ToSource(string destination)
        {
            return JsonSerializer.Deserialize<Model.Dimension>(destination);
        }

        public string ToDestination(Model.Dimension source)
        {
            return JsonSerializer.Serialize(source);
        }
    }
}