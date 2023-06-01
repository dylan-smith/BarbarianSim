namespace HunterSim
{
    public class Socket
    {
        public SocketColor Color { get; set; }
        public GearItem Gem { get; set; }

        public Socket(SocketColor color)
        {
            Color = color;
        }

        public bool IsColorMatch()
        {
            if (Gem == null)
            {
                return false;
            }

            if (Color == SocketColor.Blue && (Gem.Color == GemColor.Blue || Gem.Color == GemColor.Purple || Gem.Color == GemColor.Green))
            {
                return true;
            }

            if (Color == SocketColor.Red && (Gem.Color == GemColor.Red || Gem.Color == GemColor.Purple || Gem.Color == GemColor.Orange))
            {
                return true;
            }

            if (Color == SocketColor.Yellow && (Gem.Color == GemColor.Yellow || Gem.Color == GemColor.Orange || Gem.Color == GemColor.Green))
            {
                return true;
            }

            if (Color == SocketColor.Meta && Gem.Color == GemColor.Meta)
            {
                return true;
            }

            return false;
        }
    }
}
