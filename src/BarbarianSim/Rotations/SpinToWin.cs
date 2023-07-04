using BarbarianSim.Abilities;

namespace BarbarianSim.Rotations;

public class SpinToWin : IRotation
{
    public void Execute(SimulationState state)
    {
        if (LungingStrike.CanUse(state))
        {
            LungingStrike.Use(state);
        }
    }
}
