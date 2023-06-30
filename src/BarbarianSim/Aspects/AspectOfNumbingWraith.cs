using BarbarianSim.Config;

namespace BarbarianSim.Aspects
{
    public class AspectOfNumbingWraith : Aspect
    {
        public int Fortify { get; init; }

        public AspectOfNumbingWraith(int fortify)
        {
            Fortify = fortify;
        }
    }
}
