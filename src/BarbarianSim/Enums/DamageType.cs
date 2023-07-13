namespace BarbarianSim.Enums;

[Flags]
public enum DamageType
{
    None = 0,
    Direct = 1,
    DamageOverTime = 2,
    CriticalStrike = 4,
    Overpower = 8,
    Physical = 16,
    Fire = 32,
}
