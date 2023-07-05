using BarbarianSim.Abilities;

namespace BarbarianSim.Rotations;

public class SpinToWin : IRotation
{
    public void Execute(SimulationState state)
    {
        if (Whirlwind.CanUse(state))
        {
            Whirlwind.Use(state);
        }
        else
        {
            if (LungingStrike.CanUse(state))
            {
                LungingStrike.Use(state);
            }
        }
    }
}
