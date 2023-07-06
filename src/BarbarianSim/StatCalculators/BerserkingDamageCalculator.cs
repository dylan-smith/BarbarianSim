using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class BerserkingDamageCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<BerserkingDamageCalculator>(state);

    protected override double InstanceCalculate(SimulationState state) => state.Player.Auras.Contains(Aura.Berserking) ? 25.0 : 0.0;
}
