using BarbarianSim.Config;

namespace BarbarianSim.Aspects
{
    public class AspectOfBerserkRipping : Aspect
    {
        public int Damage { get; init; }

        public AspectOfBerserkRipping(int damage)
        {
            Damage = damage;
        }
    }
}
