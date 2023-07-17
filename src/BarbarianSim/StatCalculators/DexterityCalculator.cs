namespace BarbarianSim.StatCalculators;

public class DexterityCalculator
{
    public virtual double Calculate(SimulationState state)
    {
        var dexterity = state.Config.Gear.GetStatTotal(g => g.Dexterity);
        dexterity += state.Config.Gear.GetStatTotal(g => g.AllStats);
        dexterity += state.Config.PlayerSettings.Dexterity;
        dexterity += state.Config.PlayerSettings.Level - 1;

        return dexterity;
    }
}
