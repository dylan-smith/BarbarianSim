using BarbarianSim.Config;

namespace BarbarianSim.Aspects;

public class GhostwalkerAspect : Aspect
{
    // While Unstoppable and for 4 seconds after, you gain 10-25%[+] increased Movement Speed and can move freely through enemies
    public int Speed { get; init; }

    public GhostwalkerAspect(int speed) => Speed = speed;
}
