namespace BarbarianSim.Enums;

public enum Aura
{
    None,
    WeaponCooldown,
    Vulnerable,
    Slow,
    Immobilize,
    Stun,
    Knockback,
    Knockdown,
    Taunt,
    Fear,
    Tether,
    Daze,
    Chill,
    Freeze,
    Stagger,
    AspectOfTheProtectorCooldown,
    Whirlwinding,
    Berserking,
    Bleeding,
    ViolentWhirlwind,
    RallyingCry,
    RallyingCryCooldown,
    Unstoppable,
    ChallengingShout,
    ChallengingShoutCooldown,
    WarCry,
    WarCryCooldown,
    IronSkin,
    IronSkinCooldown,
    GutteralYell,
    WrathOfTheBerserker,
    WrathOfTheBerserkerCooldown,
    Ghostwalker,
}

public static class AuraExtensions
{
    public static bool IsCrowdControl(this Aura aura) => aura is Aura.Chill or Aura.Daze or Aura.Fear or Aura.Freeze or Aura.Immobilize or Aura.Knockback or Aura.Knockdown or Aura.Slow or Aura.Stagger or Aura.Stun or Aura.Taunt or Aura.Tether;
}
