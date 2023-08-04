using BarbarianSim.Arsenal;
using BarbarianSim.Config;

namespace BarbarianSim.StatCalculators;

public class LuckyHitChanceCalculator
{
    public LuckyHitChanceCalculator(PolearmExpertise polearmExpertise) => _polearmExpertise = polearmExpertise;

    private readonly PolearmExpertise _polearmExpertise;

    public virtual double Calculate(SimulationState state, GearItem weapon)
    {
        var luckyHitChance = state.Config.GetStatTotal(g => g.LuckyHitChance) / 100.0;
        luckyHitChance *= _polearmExpertise.GetLuckyHitChanceMultiplier(state, weapon);

        return luckyHitChance;
    }
}
