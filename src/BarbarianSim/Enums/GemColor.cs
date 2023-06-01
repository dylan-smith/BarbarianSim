using System;

namespace HunterSim
{
    public enum GemColor
    {
        Red,
        Yellow,
        Blue,
        Meta,
        Orange,
        Purple,
        Green
    }

    public static class GemColorExtensions
    {
        public static GemColor ToGemColor(this string value)
        {
            return value switch
            {
                "red" => GemColor.Red,
                "yellow" => GemColor.Yellow,
                "blue" => GemColor.Blue,
                "orange" => GemColor.Orange,
                "green" => GemColor.Green,
                "purple" => GemColor.Purple,
                "meta" => GemColor.Meta,
                _ => throw new Exception("Unrecognized gem color"),// TODO: Richer exceptions
            };
        }
    }
}
