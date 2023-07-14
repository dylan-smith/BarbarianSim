namespace BarbarianSim.Events;

public class HealingEvent : EventInfo
{
    public HealingEvent(double timestamp, double baseAmountHealed) : base(timestamp) => BaseAmountHealed = baseAmountHealed;

    public double BaseAmountHealed { get; set; }
    public double AmountHealed { get; set; }
    public double OverHeal { get; set; }
}
