using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class MovementSpeedCalculator
{
    public MovementSpeedCalculator(PrimeWrathOfTheBerserker primeWrathOfTheBerserker, GhostwalkerAspect ghostwalkerAspect, RallyingCry rallyingCry, SimLogger log)
    {
        _primeWrathOfTheBerserker = primeWrathOfTheBerserker;
        _ghostwalkerAspect = ghostwalkerAspect;
        _rallyingCry = rallyingCry;
        _log = log;
    }

    private readonly PrimeWrathOfTheBerserker _primeWrathOfTheBerserker;
    private readonly GhostwalkerAspect _ghostwalkerAspect;
    private readonly RallyingCry _rallyingCry;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var speedFromConfig = state.Config.GetStatTotal(g => g.MovementSpeed);
        if (speedFromConfig > 0)
        {
            _log.Verbose($"Movement Speed from Config = {speedFromConfig:F2}%");
        }

        var speedFromBerserking = state.Player.Auras.Contains(Aura.Berserking) ? 15 : 0;
        if (speedFromBerserking > 0)
        {
            _log.Verbose($"Movement Speed from Berserking = {speedFromBerserking:F2}%");
        }

        var speedFromRallyingCry = _rallyingCry.GetMovementSpeedIncrease(state);
        var speedFromWrathOfTheBerserker = _primeWrathOfTheBerserker.GetMovementSpeedIncrease(state);
        var speedFromGhostwalker = _ghostwalkerAspect.GetMovementSpeedIncrease(state);

        var result = speedFromConfig + speedFromBerserking + speedFromRallyingCry + speedFromWrathOfTheBerserker + speedFromGhostwalker;
        result = 1.0 + (result / 100.0);

        if (result > 1.0)
        {
            _log.Verbose($"Total Movement Speed = {result:F2}x");
        }

        return result;
    }
}
