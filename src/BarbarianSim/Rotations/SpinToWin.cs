using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Enums;

namespace BarbarianSim.Rotations;

public class SpinToWin : IRotation
{
    public void Execute(SimulationState state)
    {
        if (RallyingCry.CanUse(state))
        {
            RallyingCry.Use(state);
        }

        if (Whirlwind.CanUse(state))
        {
            Whirlwind.Use(state);
        }
        else
        {
            if (state.Player.Auras.Contains(Aura.Whirlwinding))
            {
                if (state.Config.Gear.AllGear.Select(g => g.Aspect).Any(a => a is GohrsDevastatingGrips))
                {
                    var gohrsDevastatingGrips = state.Config.Gear.AllGear.Select(g => g.Aspect).First(a => a is GohrsDevastatingGrips) as GohrsDevastatingGrips;

                    if (gohrsDevastatingGrips.HitCount >= GohrsDevastatingGrips.MAX_HIT_COUNT)
                    {
                        Whirlwind.StopSpinning(state);
                    }
                }
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
}
