using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class BerserkingDamageCalculator
{
    public BerserkingDamageCalculator(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        if (state.Player.Auras.Contains(Aura.Berserking))
        {
            _log.Verbose($"Berserking Damage Multiplier = 1.25x");
            return 1.25;
        }

        return 1.0;
    }
}
