using BarbarianSim.Config;

namespace BarbarianSim.Aspects
{
    public class EdgemastersAspect : Aspect
    {
        public int Damage { get; init; }

        public EdgemastersAspect(int damage)
        {
            Damage = damage;
        }
    }
}
