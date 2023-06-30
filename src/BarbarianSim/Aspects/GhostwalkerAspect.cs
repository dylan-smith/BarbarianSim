using BarbarianSim.Config;

namespace BarbarianSim.Aspects
{
    public class GhostwalkerAspect : Aspect
    {
        public int Speed { get; init; }

        public GhostwalkerAspect(int speed)
        {
            Speed = speed;
        }
    }
}
