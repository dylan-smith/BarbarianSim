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
