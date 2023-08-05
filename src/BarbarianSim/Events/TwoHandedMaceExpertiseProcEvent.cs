namespace BarbarianSim.Events;

[Proc("2-Handed Mace Expertise")]
public class TwoHandedMaceExpertiseProcEvent : EventInfo
{
    public TwoHandedMaceExpertiseProcEvent(double timestamp) : base(timestamp, null)
    {
    }

    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }

    public override string ToString() => $"{base.ToString()} - Gain 2-4 Fury";
}
