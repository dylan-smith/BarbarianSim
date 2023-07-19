using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class PrimeWrathOfTheBerserker
{
    // While Wrath of the Berserker is active, gain 20%[+] increased Movement Speed and increase Fury Generation by 30%[x]
    public const double MOVEMENT_SPEED_INCREASE = 20;
    public const double FURY_GENERATION_INCREASE = 1.3;

    public virtual double GetMovementSpeedIncrease(SimulationState state)
    {
        return state.Config.Skills.ContainsKey(Skill.PrimeWrathOfTheBerserker) &&
               state.Config.Skills[Skill.PrimeWrathOfTheBerserker] > 0 &&
               state.Player.Auras.Contains(Aura.WrathOfTheBerserker)
               ? MOVEMENT_SPEED_INCREASE
               : 0;
    }
}
