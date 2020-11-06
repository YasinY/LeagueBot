using System;
using System.IO;
using System.Reflection;
using LeagueBot.Exceptions;

namespace LeagueBot.Json
{
    public class JsonLoader
    {
        public static string ReadJson(string jsonName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"LeagueBot.Resources.Json.{jsonName}.json";

            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new JsonNotFoundException(resourceName);
            }

            using var reader = new StreamReader(stream);
            var jsonFile = reader.ReadToEnd(); //Make string equal to full file

            return jsonFile;
        }
    }
}