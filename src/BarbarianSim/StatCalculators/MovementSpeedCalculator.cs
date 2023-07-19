using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class MovementSpeedCalculator
{
    public MovementSpeedCalculator(PrimeWrathOfTheBerserker primeWrathOfTheBerserker, GhostwalkerAspect ghostwalkerAspect)
    {
        _primeWrathOfTheBerserker = primeWrathOfTheBerserker;
        _ghostwalkerAspect = ghostwalkerAspect;
    }

    private readonly PrimeWrathOfTheBerserker _primeWrathOfTheBerserker;
    private readonly GhostwalkerAspect _ghostwalkerAspect;

    public virtual double Calculate(SimulationState state)
    {
        var movementSpeed = state.Config.Gear.GetStatTotal(g => g.MovementSpeed);
        movementSpeed += state.Player.Auras.Contains(Aura.Berserking) ? 15 : 0;
        movementSpeed += state.Player.Auras.Contains(Aura.RallyingCry) ? RallyingCry.MOVEMENT_SPEED : 0;

        movementSpeed += _primeWrathOfTheBerserker.GetMovementSpeedIncrease(state);

        movementSpeed += _ghostwalkerAspect.GetMovementSpeedIncrease(state);

        return 1.0 + (movementSpeed / 100.0);
    }
}
