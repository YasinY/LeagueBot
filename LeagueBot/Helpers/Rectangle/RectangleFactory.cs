using System;
using System.Collections.Generic;
using System.Linq;
using LeagueBot.Exceptions;
using LeagueBot.Json;
using LeagueBot.Mapper;
using LeagueBot.Model;

namespace LeagueBot.Helpers.Rectangle
{
    public class RectangleFactory
    {
        public List<RectanglePositionsModel> Data { get; set; }


        public RectangleFactory(RectanglePositionType type)
        {
            var json = JsonLoader.ReadJson(type.ToString());
            Data = new DimensionToJsonMapper().ToSource(json);
        }

        public Dictionary<string, RectanglePosition> Produce(string widthAndHeight)
        {
            var dictionary = new List<RectanglePosition>();
            if (!Data.Any())
            {
                Console.WriteLine("Error loading data.");
            }

            Data.Find(element => element.TryGetValue(widthAndHeight, out dictionary));
            if (dictionary == null)
            {
                throw new ResolutionNotFoundException(widthAndHeight);
            }

            var rectanglePositions = dictionary.ToDictionary(x => x.Identifier, x => x);
            return rectanglePositions;
        }

        public Dictionary<string, RectanglePosition> Produce(int width, int height)
        {
            return Produce($"{width.ToString()}x{height.ToString()}");
        }
    }
}