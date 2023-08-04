using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Skills;

public class StrategicIronSkin : IHandlesEvent<Events.IronSkinEvent>
{
    // Strategic: Ironskin also grants 15% Base Life (15%[x] HP) as Fortify. Double this amount if cast while below 50% Life
    public StrategicIronSkin(MaxLifeCalculator maxLifeCalculator)
    {
        _maxLifeCalculator = maxLifeCalculator;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;

    public void ProcessEvent(IronSkinEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.StrategicIronSkin) && state.Config.Skills[Skill.StrategicIronSkin] > 0)
        {
            var fortifyAmount = IronSkin.FORTIFY_FROM_STRATEGIC * state.Player.BaseLife;

            if (state.Player.GetLifePercentage(_maxLifeCalculator.Calculate(state)) < 0.5)
            {
                fortifyAmount *= 2;
            }

            state.Events.Add(new FortifyGeneratedEvent(e.Timestamp, "Strategic Iron Skin", fortifyAmount));
        }
    }
}
