using BarbarianSim.Arsenal;
using BarbarianSim.Config;

namespace BarbarianSim.StatCalculators;

public class LuckyHitChanceCalculator
{
    public LuckyHitChanceCalculator(PolearmExpertise polearmExpertise, SimLogger log)
    {
        _polearmExpertise = polearmExpertise;
        _log = log;
    }

    private readonly PolearmExpertise _polearmExpertise;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, GearItem weapon)
    {
        var luckyHitChance = state.Config.GetStatTotal(g => g.LuckyHitChance) / 100.0;
        if (luckyHitChance > 0)
        {
            _log.Verbose($"Lucky Hit Chance from Config = {luckyHitChance:P2}");
        }

        luckyHitChance *= _polearmExpertise.GetLuckyHitChanceMultiplier(state, weapon);

        if (luckyHitChance > 0)
        {
            _log.Verbose($"Total Lucky Hit Chance Bonus = {luckyHitChance:P2}");
        }

        return luckyHitChance;
    }
}
