namespace BarbarianSim.Events;

public class FortifyGeneratedEvent : EventInfo
{
    public double Amount { get; init; }

    public FortifyGeneratedEvent(double timestamp, double amount) : base(timestamp) => Amount = amount;
}
