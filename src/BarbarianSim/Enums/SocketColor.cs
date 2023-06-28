using System;

namespace BarbarianSim
{
    public enum SocketColor
    {
        Red,
        Yellow,
        Blue,
        Meta
    }

    public static class SocketColorExtensions
    {
        public static SocketColor ToSocketColor(this string value)
        {
            return value switch
            {
                "red" => SocketColor.Red,
                "yellow" => SocketColor.Yellow,
                "blue" => SocketColor.Blue,
                "meta" => SocketColor.Meta,
                _ => throw new ArgumentException($"Unrecognized socket color {value}"),
            };
        }
    }
}
