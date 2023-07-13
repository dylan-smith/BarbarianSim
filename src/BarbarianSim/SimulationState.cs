using BarbarianSim.Config;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim;

public class SimulationState
{
    public IList<EventInfo> Events { get; init; } = new List<EventInfo>();
    public IList<EventInfo> ProcessedEvents { get; init; } = new List<EventInfo>();
    public double CurrentTime { get; set; }
    public SimulationConfig Config { get; init; }
    public IList<string> Warnings { get; init; } = new List<string>();
    public IList<string> Errors { get; init; } = new List<string>();
    public IList<EnemyState> Enemies { get; init; } = new List<EnemyState>();
    public PlayerState Player { get; init; } = new();

    public SimulationState(SimulationConfig config)
    {
        Config = config;

        config.EnemySettings.NumberOfEnemies.Times(() => Enemies.Add(new EnemyState() { MaxLife = config.EnemySettings.Life, Life = config.EnemySettings.Life }));

        Player.BaseLife = config.PlayerSettings.Life;
        Player.Life = Player.BaseLife;
    }

    public IEnumerable<DamageEvent> DamageEvents => ProcessedEvents.Where(e => e is DamageEvent).Cast<DamageEvent>();

    public virtual bool Validate()
    {
        var (warnings, errors) = Config.Validate();

        Warnings.AddRange(warnings);
        Errors.AddRange(errors);

        return !Errors.Any();
    }
}
