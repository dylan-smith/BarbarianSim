using BarbarianSim.Enums;
using BarbarianSim.Rotations;

namespace BarbarianSim.Config;

public class SimulationConfig
{
    public Gear Gear { get; init; } = new();
    public PlayerSettings PlayerSettings { get; init; } = new();
    public EnemySettings EnemySettings { get; init; } = new();
    public SimulationSettings SimulationSettings { get; init; } = new();
    public IDictionary<Skill, int> Skills { get; init; } = new Dictionary<Skill, int>();
    public IRotation Rotation { get; set; }
    public GearItem Paragon { get; set; } = new();

    public double GetStatTotal(Func<GearItem, double> stat) => Gear.GetStatTotal(stat) + stat(Paragon);

    public double GetStatTotalMultiplied(Func<GearItem, double> stat) => Gear.GetStatTotalMultiplied(stat) * stat(Paragon);

    public virtual (IEnumerable<string> Warnings, IEnumerable<string> Errors) Validate()
    {
        var warnings = new List<string>();
        var errors = new List<string>();

        if (!ValidateTooManySkillPoints())
        {
            warnings.Add(SimulationWarnings.TooManySkillPoints);
        }

        if (!ValidateNotEnoughSkillPoints())
        {
            warnings.Add(SimulationWarnings.MissingSkillPoints);
        }

        if (!ValidateAllGearHasAspects())
        {
            warnings.Add(SimulationWarnings.MissingAspect);
        }

        if (!ValidatePlayerMaxLevel())
        {
            warnings.Add(SimulationWarnings.PlayerNotMaxLevel);
        }

        if (!ValidateAllSocketsUsed())
        {
            warnings.Add(SimulationWarnings.MoreSocketsAvailable);
        }

        if (!ValidateMaxLevelGemsUsed())
        {
            warnings.Add(SimulationWarnings.HigherLevelGemsAvailable);
        }

        if (!ValidateWeaponsHaveExpertise())
        {
            warnings.Add(SimulationWarnings.ExpertiseMissing);
        }

        return (warnings, errors);
    }


    private bool ValidatePlayerMaxLevel() => PlayerSettings.Level == 100;

    private bool ValidateTooManySkillPoints()
    {
        var points = PlayerSettings.Level - 9;

        return Skills.Values.Sum() <= points;
    }

    private bool ValidateNotEnoughSkillPoints()
    {
        var points = Math.Min(PlayerSettings.Level - 1, 48);

        return Skills.Values.Sum() >= points;
    }

    private bool ValidateAllGearHasAspects() => Gear.AllGear.All(x => x.Aspect != null);

    private bool ValidateAllSocketsUsed() => Gear.GetAllGems().Count() == 14;

    private bool ValidateMaxLevelGemsUsed() => Gear.GetAllGems().All(gem => gem.IsMaxLevel());

    private bool ValidateWeaponsHaveExpertise() =>
        Gear.TwoHandBludgeoning.Expertise != Expertise.NA &&
        Gear.OneHandLeft.Expertise != Expertise.NA &&
        Gear.OneHandRight.Expertise != Expertise.NA &&
        Gear.TwoHandSlashing.Expertise != Expertise.NA;
}
