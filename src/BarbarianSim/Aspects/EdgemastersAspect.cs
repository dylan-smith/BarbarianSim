using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Aspects;

public class EdgemastersAspect : Aspect
{
    // Skills deal up to 10-20%[x] increased damage based on your available Primary Resource when cast, receiving the maximum benefit while you have full Primary Resource
    public EdgemastersAspect(MaxFuryCalculator maxFuryCalculator, SimLogger log)
    {
        _maxFuryCalculator = maxFuryCalculator;
        _log = log;
    }

    private readonly MaxFuryCalculator _maxFuryCalculator;
    private readonly SimLogger _log;

    public int Damage { get; set; }

    public virtual double GetDamageBonus(SimulationState state, SkillType skillType)
    {
        if (IsAspectEquipped(state) && skillType != SkillType.None)
        {
            var maxFury = _maxFuryCalculator.Calculate(state);
            var furyMultiplier = state.Player.Fury / maxFury;

            var result = 1 + (Damage / 100.0 * furyMultiplier);

            _log.Verbose($"Damage bonus from Edgemasters Aspect = {result:F2}x");

            return result;
        }

        return 1.0;
    }
}
