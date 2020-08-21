using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace LeagueBot.Mapper
{
    /*
     * T = source
     */
    public interface IJsonMapper<T> : IMapper<T, string>
    {
        
        public new T ToSource(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public new string ToDestination(T source)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(source, serializeOptions);
        }
        
    }
}