namespace BarbarianSim
{
    public class DamageEvent : EventInfo
    {
        public readonly double Damage;
        public readonly DamageType DamageType;

        public DamageEvent(double timestamp, double damage, DamageType damageType) : base(timestamp)
        {
            Damage = damage;
            DamageType = damageType;
        }

        public override void ProcessEvent(SimulationState state)
        {
        }

        public override string ToString() => $"[{Timestamp:F1}] {DamageType} for {Damage:F2}";
    }
}
