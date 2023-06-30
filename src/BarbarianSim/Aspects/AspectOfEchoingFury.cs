using BarbarianSim.Config;

namespace BarbarianSim.Aspects
{
    public class AspectOfEchoingFury : Aspect
    {
        public int Fury { get; init; }

        public AspectOfEchoingFury(int fury)
        {
            Fury = fury;
        }
    }
}
