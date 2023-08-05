using BarbarianSim.Abilities;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class ResourceGenerationCalculator
{
    public ResourceGenerationCalculator(WillpowerCalculator willpowerCalculator,
                                        RallyingCry rallyingCry,
                                        ProlificFury prolificFury,
                                        TacticalRallyingCry tacticalRallyingCry,
                                        PrimeWrathOfTheBerserker primeWrathOfTheBerserker,
                                        SimLogger log)
    {
        _willpowerCalculator = willpowerCalculator;
        _rallyingCry = rallyingCry;
        _prolificFury = prolificFury;
        _tacticalRallyingCry = tacticalRallyingCry;
        _primeWrathOfTheBerserker = primeWrathOfTheBerserker;
        _log = log;
    }

    private readonly WillpowerCalculator _willpowerCalculator;
    private readonly RallyingCry _rallyingCry;
    private readonly ProlificFury _prolificFury;
    private readonly TacticalRallyingCry _tacticalRallyingCry;
    private readonly PrimeWrathOfTheBerserker _primeWrathOfTheBerserker;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var resourceGeneration = 0.0;

        var configBonus = state.Config.GetStatTotal(g => g.ResourceGeneration);
        if (configBonus > 0)
        {
            resourceGeneration += configBonus;
            _log.Verbose($"Resource Generation bonus from Gear/Paragon = {resourceGeneration}%");
        }

        var willpowerBonus = _willpowerCalculator.Calculate(state) * 0.03;
        if (willpowerBonus > 0)
        {
            resourceGeneration += willpowerBonus;
            _log.Verbose($"Resource Generation bonus from Willpower = {willpowerBonus}%");
        }

        var result = 1.0 + (resourceGeneration / 100.0);

        var rallyingCryBonus = _rallyingCry.GetResourceGeneration(state);
        if (rallyingCryBonus > 1)
        {
            result *= rallyingCryBonus;
            _log.Verbose($"Resource Generation bonus from Rallying Cry = {rallyingCryBonus}x");
        }

        var tacticalRallyingCryBonus = _tacticalRallyingCry.GetResourceGeneration(state);
        if (tacticalRallyingCryBonus > 1)
        {
            result *= tacticalRallyingCryBonus;
            _log.Verbose($"Resource Generation bonus from Tactical Rallying Cry = {tacticalRallyingCryBonus}x");
        }

        var prolificFuryBonus = _prolificFury.GetFuryGeneration(state);
        if (prolificFuryBonus > 1)
        {
            result *= prolificFuryBonus;
            _log.Verbose($"Resource Generation bonus from Prolific Fury = {prolificFuryBonus}x");
        }

        var primeWrathOfTheBerserkerBonus = _primeWrathOfTheBerserker.GetResourceGeneration(state);
        if (primeWrathOfTheBerserkerBonus > 1)
        {
            result *= primeWrathOfTheBerserkerBonus;
            _log.Verbose($"Resource Generation bonus from Prime Wrath of the Berserker = {primeWrathOfTheBerserkerBonus}x");
        }

        _log.Verbose($"Resource Generation multiplier = {result:F2}x");
        return result;
    }
}
