namespace BarbarianSim.Events;

public abstract class EventInfo
{
    public double Timestamp { get; set; }

    public string Source { get; init; }

    protected EventInfo(double timestamp, string source)
    {
        Timestamp = timestamp;
        Source = source;
    }

    public override string ToString() => $"[{Timestamp:F1}] {GetType().Name}";
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
