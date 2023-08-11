using BarbarianSim.Abilities;
using BarbarianSim.Arsenal;
using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class TotalDamageMultiplierCalculator
{
    public TotalDamageMultiplierCalculator(AdditiveDamageBonusCalculator additiveDamageBonusCalculator,
                                           TwoHandedWeaponDamageMultiplicativeCalculator twoHandedWeaponDamageMultiplicativeCalculator,
                                           VulnerableDamageBonusCalculator vulnerableDamageBonusCalculator,
                                           StrengthCalculator strengthCalculator,
                                           PitFighter pitFighter,
                                           WarCry warCry,
                                           PowerWarCry powerWarCry,
                                           SupremeWrathOfTheBerserker supremeWrathOfTheBerserker,
                                           UnbridledRage unbridledRage,
                                           ViolentWhirlwind violentWhirlwind,
                                           EnhancedLungingStrike enhancedLungingStrike,
                                           EdgemastersAspect edgemastersAspect,
                                           AspectOfLimitlessRage aspectOfLimitlessRage,
                                           ConceitedAspect conceitedAspect,
                                           AspectOfTheExpectant aspectOfTheExpectant,
                                           ExploitersAspect exploitersAspect,
                                           PenitentGreaves penitentGreaves,
                                           RamaladnisMagnumOpus ramaladnisMagnumOpus,
                                           BerserkingDamageCalculator berserkingDamageCalculator,
                                           PolearmExpertise polearmExpertise)
    {
        _additiveDamageBonusCalculator = additiveDamageBonusCalculator;
        _twoHandedWeaponDamageMultiplicativeCalculator = twoHandedWeaponDamageMultiplicativeCalculator;
        _vulnerableDamageBonusCalculator = vulnerableDamageBonusCalculator;
        _strengthCalculator = strengthCalculator;
        _pitFighter = pitFighter;
        _warCry = warCry;
        _powerWarCry = powerWarCry;
        _supremeWrathOfTheBerserker = supremeWrathOfTheBerserker;
        _unbridledRage = unbridledRage;
        _violentWhirlwind = violentWhirlwind;
        _enhancedLungingStrike = enhancedLungingStrike;
        _edgemastersAspect = edgemastersAspect;
        _aspectOfLimitlessRage = aspectOfLimitlessRage;
        _conceitedAspect = conceitedAspect;
        _aspectOfTheExpectant = aspectOfTheExpectant;
        _exploitersAspect = exploitersAspect;
        _penitentGreaves = penitentGreaves;
        _ramaladnisMagnumOpus = ramaladnisMagnumOpus;
        _berserkingDamageCalculator = berserkingDamageCalculator;
        _polearmExpertise = polearmExpertise;
    }

    private readonly AdditiveDamageBonusCalculator _additiveDamageBonusCalculator;
    private readonly TwoHandedWeaponDamageMultiplicativeCalculator _twoHandedWeaponDamageMultiplicativeCalculator;
    private readonly VulnerableDamageBonusCalculator _vulnerableDamageBonusCalculator;
    private readonly StrengthCalculator _strengthCalculator;
    private readonly PitFighter _pitFighter;
    private readonly WarCry _warCry;
    private readonly PowerWarCry _powerWarCry;
    private readonly SupremeWrathOfTheBerserker _supremeWrathOfTheBerserker;
    private readonly UnbridledRage _unbridledRage;
    private readonly ViolentWhirlwind _violentWhirlwind;
    private readonly EnhancedLungingStrike _enhancedLungingStrike;
    private readonly EdgemastersAspect _edgemastersAspect;
    private readonly AspectOfLimitlessRage _aspectOfLimitlessRage;
    private readonly ConceitedAspect _conceitedAspect;
    private readonly AspectOfTheExpectant _aspectOfTheExpectant;
    private readonly ExploitersAspect _exploitersAspect;
    private readonly PenitentGreaves _penitentGreaves;
    private readonly RamaladnisMagnumOpus _ramaladnisMagnumOpus;
    private readonly BerserkingDamageCalculator _berserkingDamageCalculator;
    private readonly PolearmExpertise _polearmExpertise;

    public virtual double Calculate(SimulationState state, DamageType damageType, EnemyState enemy, SkillType skillType, DamageSource damageSource, GearItem weapon)
    {
        var damageBonus = _additiveDamageBonusCalculator.Calculate(state, damageType, enemy);
        damageBonus *= _twoHandedWeaponDamageMultiplicativeCalculator.Calculate(state, weapon);
        damageBonus *= _vulnerableDamageBonusCalculator.Calculate(state, enemy, weapon);
        damageBonus *= _strengthCalculator.GetDamageMultiplier(state, skillType);
        damageBonus *= _pitFighter.GetCloseDamageBonus(state);
        damageBonus *= _warCry.GetDamageBonus(state);
        damageBonus *= _powerWarCry.GetDamageBonus(state);
        damageBonus *= _unbridledRage.GetDamageBonus(state, skillType);
        damageBonus *= _violentWhirlwind.GetDamageBonus(state, damageSource);
        damageBonus *= _enhancedLungingStrike.GetDamageBonus(state, damageSource, enemy);
        damageBonus *= _supremeWrathOfTheBerserker.GetBerserkDamageBonus(state);
        damageBonus *= _edgemastersAspect.GetDamageBonus(state, skillType);
        damageBonus *= _aspectOfLimitlessRage.GetDamageBonus(state, skillType);
        damageBonus *= _conceitedAspect.GetDamageBonus(state);
        damageBonus *= _aspectOfTheExpectant.GetDamageBonus(state, skillType);
        damageBonus *= _exploitersAspect.GetDamageBonus(state, enemy);
        damageBonus *= _penitentGreaves.GetDamageBonus(state);
        damageBonus *= _ramaladnisMagnumOpus.GetDamageBonus(state, weapon);
        damageBonus *= _berserkingDamageCalculator.Calculate(state);
        damageBonus *= _polearmExpertise.GetHealthyDamageMultiplier(state, weapon);

        return damageBonus;
    }
}
