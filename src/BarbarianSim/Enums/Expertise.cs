namespace BarbarianSim.Enums;

public enum Expertise
{
    NA,
    OneHandedAxe,
    OneHandedMace,
    OneHandedSword,
    TwoHandedAxe,
    Polearm,
    TwoHandedSword,
    TwoHandedMace,
}

public static class ExpertiseExtensions
{
    public static bool IsOneHanded(this Expertise expertise) => expertise switch
    {
        Expertise.OneHandedAxe => true,
        Expertise.OneHandedMace => true,
        Expertise.OneHandedSword => true,
        _ => false,
    };

    public static bool IsTwoHanded(this Expertise expertise) => expertise switch
    {
        Expertise.TwoHandedAxe => true,
        Expertise.Polearm => true,
        Expertise.TwoHandedSword => true,
        Expertise.TwoHandedMace => true,
        _ => false,
    };
}
