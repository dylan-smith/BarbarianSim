namespace BarbarianSim.Enums;

public enum Aura
{
    None,
    WeaponCooldown,
    AspectOfTheProtectorCooldown,
    RallyingCryCooldown,
    ChallengingShoutCooldown,
    WarCryCooldown,
    IronSkinCooldown,
    WrathOfTheBerserkerCooldown,
    Whirlwinding,
    ViolentWhirlwind,

    [Debuff]
    Vulnerable,
    [Debuff]
    Slow,
    [Debuff]
    Immobilize,
    [Debuff]
    Stun,
    [Debuff]
    Knockback,
    [Debuff]
    Knockdown,
    [Debuff]
    Taunt,
    [Debuff]
    Fear,
    [Debuff]
    Tether,
    [Debuff]
    Daze,
    [Debuff]
    Chill,
    [Debuff]
    Freeze,
    [Debuff]
    Stagger,
    [Debuff]
    Bleeding,

    [Buff]
    Berserking,
    [Buff]
    RallyingCry,
    [Buff]
    Unstoppable,
    [Buff]
    ChallengingShout,
    [Buff]
    WarCry,
    [Buff]
    IronSkin,
    [Buff]
    GutteralYell,
    [Buff]
    WrathOfTheBerserker,
    [Buff]
    Ghostwalker,
}

public static class AuraExtensions
{
    public static bool IsCrowdControl(this Aura aura) => aura is Aura.Chill or Aura.Daze or Aura.Fear or Aura.Freeze or Aura.Immobilize or Aura.Knockback or Aura.Knockdown or Aura.Slow or Aura.Stagger or Aura.Stun or Aura.Taunt or Aura.Tether;

    public static bool IsBuff(this Aura aura) => aura.HasAttribute<BuffAttribute>();

    public static bool IsDebuff(this Aura aura) => aura.HasAttribute<DebuffAttribute>();
}

[AttributeUsage(AttributeTargets.Field)]
public sealed class BuffAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Field)]
public sealed class DebuffAttribute : Attribute { }
