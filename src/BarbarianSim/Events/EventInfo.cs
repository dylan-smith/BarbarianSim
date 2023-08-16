namespace BarbarianSim.Events;

public abstract class EventInfo
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public double Timestamp { get; set; }
    public string Source { get; init; }

    protected EventInfo(double timestamp, string source)
    {
        Timestamp = timestamp;
        Source = source;
    }

    public override string ToString() => $"{GetType().Name}";

    public ICollection<string> VerboseLog { get; init; } = new List<string>();

    public void AddVerboseLog(string msg) => VerboseLog.Add(msg);
}

[AttributeUsage(AttributeTargets.Class)]
public sealed class AbilityAttribute : Attribute
{
    public AbilityAttribute(string name) => Name = name;
    public string Name { get; init; }
}

[AttributeUsage(AttributeTargets.Class)]
public sealed class ProcAttribute : Attribute
{
    public ProcAttribute(string name) => Name = name;
    public string Name { get; init; }
}
