namespace BarbarianSim.StatCalculators;

public class DodgeCalculator
{
    public DodgeCalculator(DexterityCalculator dexterityCalculator, SimLogger log)
    {
        _dexterityCalculator = dexterityCalculator;
        _log = log;
    }

    private readonly DexterityCalculator _dexterityCalculator;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var dodgeFromConfig = state.Config.GetStatTotalMultiplied(g => 1 - (g.Dodge / 100.0));
        _log.Verbose($"Dodge From Config = {1 - dodgeFromConfig:P2}");

        var dodgeFromDexterity = 1 - (_dexterityCalculator.Calculate(state) * 0.01 / 100.0);
        _log.Verbose($"Dodge From Dexterity = {1 - dodgeFromDexterity:P2}");

        var result = 1 - (dodgeFromConfig * dodgeFromDexterity);
        _log.Verbose($"Total Dodge = {result:P2}");

        return result;
    }
}
