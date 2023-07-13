using BarbarianSim.Config;

namespace BarbarianSim.Aspects;

public class AspectOfTheDireWhirlwind : Aspect
{
    public AspectOfTheDireWhirlwind(int critChance, int maxCritChance)
    {
        CritChance = critChance;
        MaxCritChance = maxCritChance;
    }
    
    public int CritChance { get; init; }
    public int MaxCritChance { get; init; }
}
