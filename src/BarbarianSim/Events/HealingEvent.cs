namespace BarbarianSim.Events;

public class HealingEvent : EventInfo
{
    public HealingEvent(double timestamp, string source, double baseAmountHealed) : base(timestamp, source) => BaseAmountHealed = baseAmountHealed;

    public double BaseAmountHealed { get; set; }
    public double AmountHealed { get; set; }
    public double OverHeal { get; set; }

    public override string ToString() => $"{base.ToString()} - Healed for {AmountHealed:F2} ({OverHeal:F2} overheal) (Source: {Source})";
}
