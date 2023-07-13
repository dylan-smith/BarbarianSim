using BarbarianSim.Enums;
using BarbarianSim.EventFactories;

namespace BarbarianSim.Events
{
    public class PressurePointProcEvent : EventInfo
    {
        public PressurePointProcEvent(AuraAppliedEventFactory auraAppliedEventFactory,
                                      double timestamp,
                                      EnemyState target) : base(timestamp)
        {
            _auraAppliedEventFactory = auraAppliedEventFactory;
            Target = target;
        }

        private const double VULNERABLE_DURATION = 2.0;

        private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

        public EnemyState Target { get; init; }
        public AuraAppliedEvent VulnerableAppliedEvent { get; set; }

        public override void ProcessEvent(SimulationState state)
        {
            VulnerableAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, VULNERABLE_DURATION, Aura.Vulnerable, Target);

            state.Events.Add(VulnerableAppliedEvent);
        }
    }
}
