using System;
using System.Collections.Generic;
using System.Linq;
using LeagueBot.Loader;
using LeagueBot.Mapper;
using LeagueBot.Model;

namespace LeagueBot.Dimension
{
    public class DimensionFactory
    {
        public List<DimensionModel> Data { get; set; }


        public DimensionFactory()
        {
            var json = JsonLoader.ReadJson("Dimensions");
            Data = new DimensionToJsonMapper().ToSource(json);
        }

        public Dictionary<string, RectanglePosition> Produce(string widthAndHeight)
        {
            List<RectanglePosition> dictionary = new List<RectanglePosition>();

            Data.Find(element => element.TryGetValue(widthAndHeight, out dictionary));

            var rectanglePositions = dictionary.ToDictionary(x => x.Identifier, x => x);
            return rectanglePositions;
        }

        public Dictionary<string, RectanglePosition> Produce(int width, int height)
        {
            return Produce($"{width.ToString()}x{height.ToString()}");
        }
    }
}