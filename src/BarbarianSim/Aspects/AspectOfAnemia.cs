using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfAnemia : Aspect, IHandlesEvent<LuckyHitEvent>
{
    // Lucky Hit: Direct damage against Bleeding enemies has up to a [20 - 30]% chance to Stun them for 2 seconds.
    public const double STUN_DURATION = 2.0;
    public double StunChance { get; set; }

    public AspectOfAnemia(RandomGenerator randomGenerator) => _randomGenerator = randomGenerator;

    private readonly RandomGenerator _randomGenerator;

    public void ProcessEvent(LuckyHitEvent e, SimulationState state)
    {
        // We're assuming that all lucky hits deal direct damage
        if (IsAspectEquipped(state) && e.Target.IsBleeding())
        {
            var procRoll = _randomGenerator.Roll(RollType.AspectOfAnemia);

            if (procRoll <= (StunChance / 100.0))
            {
                state.Events.Add(new AuraAppliedEvent(e.Timestamp, "Aspect of Anemia", STUN_DURATION, Aura.Stun, e.Target));
            }
        }
    }
}
