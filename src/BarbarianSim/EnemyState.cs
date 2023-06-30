namespace BarbarianSim
{
    public class EnemyState
    {
        public int Life { get; set; }
        public int MaxLife { get; set; }
        public ISet<Aura> Auras { get; init; } = new HashSet<Aura>();

        public bool IsSlowed() => false;

        public bool IsCrowdControlled() => false;

        public bool IsInjured() => ((double)Life / MaxLife) <= 0.35;

        public bool IsVulnerable() => Auras.Contains(Aura.Vulnerable);
    }
}
