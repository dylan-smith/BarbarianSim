namespace BarbarianSim.Events;

public class FuryGeneratedEvent : EventInfo
{
    public double BaseFury { get; init; }
    public double FuryGenerated { get; set; }
    public double OverflowFury { get; set; }

    public FuryGeneratedEvent(double timestamp, string source, double fury) : base(timestamp, source) => BaseFury = fury;

    public override string ToString() => $"{base.ToString()} - {FuryGenerated:F2} fury generated (Source: {Source})";
}
