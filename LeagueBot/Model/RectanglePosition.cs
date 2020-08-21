﻿using System;
using System.Text.Json.Serialization;

namespace LeagueBot.Model
{ //TODO unfortunately c# doesnt have extension variable support *yet*
    public class RectanglePosition
    {
        [JsonPropertyName("identifier")] public string Identifier { get; set; }

        [JsonPropertyName("x1")] public int X1 { get; }

        [JsonPropertyName("y2")] public int Y2 { get; }

        [JsonPropertyName("x2")] public int X2 { get; }

        [JsonPropertyName("y1")] public int Y1 { get; }

        public RectanglePosition(int x1, int y2)
        {
            X1 = x1;
            X2 = Math.Abs(x1 - 41);
            Y2 = y2;
            Y1 = Math.Abs(y2 - 14);
        }

        public RectanglePosition(int x1, int y2, int x2, int y1)
        {
            X1 = x1;
            Y2 = y2;
            X2 = x2;
            Y1 = y1;
        }
    }
}