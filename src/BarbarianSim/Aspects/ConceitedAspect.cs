using BarbarianSim.Config;

namespace BarbarianSim.Aspects
{
    public class ConceitedAspect : Aspect
    {
        public int Damage { get; init; }

        public ConceitedAspect(int damage)
        {
            Damage = damage;
        }
    }
}
