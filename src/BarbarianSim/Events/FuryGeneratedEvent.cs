using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class FuryGeneratedEvent : EventInfo
{
    public double BaseFury { get; init; }
    public double FuryGenerated { get; set; }

    public FuryGeneratedEvent(double timestamp, double fury) : base(timestamp) => BaseFury = fury;

    public override string ToString() => $"{base.ToString()} - {FuryGenerated} fury generated";
}
