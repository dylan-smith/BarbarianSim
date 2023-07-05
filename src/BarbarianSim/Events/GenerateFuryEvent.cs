using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events
{
    public class GenerateFuryEvent : EventInfo
    {
        public double Fury { get; init; }

        public GenerateFuryEvent(double timestamp, double fury) : base(timestamp) => Fury = fury;

        public override void ProcessEvent(SimulationState state)
        {
            var multiplier = ResourceGenerationCalculator.Calculate(state);
            state.Player.Fury += Fury * multiplier;

            state.Player.Fury = Math.Min(state.Player.MaxFury, state.Player.Fury);
        }
    }
}
