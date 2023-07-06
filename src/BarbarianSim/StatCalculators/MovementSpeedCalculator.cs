using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class MovementSpeedCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<MovementSpeedCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var movementSpeed = state.Config.Gear.GetStatTotal(g => g.MovementSpeed);
        movementSpeed += state.Player.Auras.Contains(Aura.Berserking) ? 15 : 0;

        return movementSpeed;
    }
}
