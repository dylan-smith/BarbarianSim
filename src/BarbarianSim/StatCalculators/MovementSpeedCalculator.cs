using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class MovementSpeedCalculator
{
    public virtual double Calculate(SimulationState state)
    {
        var movementSpeed = state.Config.Gear.GetStatTotal(g => g.MovementSpeed);
        movementSpeed += state.Player.Auras.Contains(Aura.Berserking) ? 15 : 0;
        movementSpeed += state.Player.Auras.Contains(Aura.RallyingCry) ? RallyingCry.MOVEMENT_SPEED : 0;

        if (state.Config.Skills.ContainsKey(Skill.PrimeWrathOfTheBerserker) && state.Player.Auras.Contains(Aura.WrathOfTheBerserker))
        {
            movementSpeed += WrathOfTheBerserker.MOVEMENT_SPEED_FROM_PRIME;
        }

        if (state.Player.Auras.Contains(Aura.Ghostwalker))
        {
            movementSpeed += state.Config.Gear.GetAllAspects<GhostwalkerAspect>().Single().Speed;
        }

        return 1.0 + (movementSpeed / 100.0);
    }
}
