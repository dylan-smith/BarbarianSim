using BarbarianSim.Config;

namespace BarbarianSim.Aspects
{
    public class RageOfHarrogath : Aspect
    {
        public int Chance { get; init; }

        public RageOfHarrogath(int chance)
        {
            Chance = chance;
        }
    }
}
