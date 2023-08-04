namespace BarbarianSim.Events;

public class FortifyGeneratedEvent : EventInfo
{
    public double Amount { get; init; }

    public FortifyGeneratedEvent(double timestamp, string source, double amount) : base(timestamp, source) => Amount = amount;

    public override string ToString() => $"{base.ToString()} - {Amount:F2} fortify generated (Source: {Source})";
}
