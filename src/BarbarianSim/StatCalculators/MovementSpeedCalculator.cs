using BarbarianSim.Abilities;
using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class MovementSpeedCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<MovementSpeedCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var movementSpeed = state.Config.Gear.GetStatTotal(g => g.MovementSpeed);
        movementSpeed += state.Player.Auras.Contains(Aura.Berserking) ? 15 : 0;
        movementSpeed += state.Player.Auras.Contains(Aura.RallyingCry) ? RallyingCry.MOVEMENT_SPEED : 0;

        if (state.Config.Skills.ContainsKey(Skill.PrimeWrathOfTheBerserker) && state.Player.Auras.Contains(Aura.WrathOfTheBerserker))
        {
            movementSpeed += WrathOfTheBerserker.MOVEMENT_SPEED_FROM_PRIME;
        }

        return movementSpeed;
    }
}
