using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Skills;

public class StrategicIronSkin : IHandlesEvent<Events.IronSkinEvent>
{
    // Strategic: Ironskin also grants 15% Base Life (15%[x] HP) as Fortify. Double this amount if cast while below 50% Life
    public StrategicIronSkin(MaxLifeCalculator maxLifeCalculator, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly SimLogger _log;

    public void ProcessEvent(IronSkinEvent e, SimulationState state)
    {
        if (state.Config.HasSkill(Skill.StrategicIronSkin))
        {
            var fortifyAmount = IronSkin.FORTIFY_FROM_STRATEGIC * state.Player.BaseLife;

            if (state.Player.GetLifePercentage(_maxLifeCalculator.Calculate(state)) < 0.5)
            {
                fortifyAmount *= 2;
                _log.Verbose($"Strategic Iron Skin doubled Fortify amount to {fortifyAmount:F2} because player is below 50% life");
            }

            state.Events.Add(new FortifyGeneratedEvent(e.Timestamp, "Strategic Iron Skin", fortifyAmount));
            _log.Verbose($"Strategic Iron Skin created FortifyGeneratedEvent for {fortifyAmount:F2} Fortify");
        }
    }
}
