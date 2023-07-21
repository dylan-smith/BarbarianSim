namespace BarbarianSim.Config;

public class Aspect
{
    public bool IsAspectEquipped(SimulationState state) => state.Config.Gear.HasAspect(this);
}
