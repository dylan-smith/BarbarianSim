using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Tests;

public class FakeStatCalculator : BaseStatCalculator
{
    private readonly double _value;
    private readonly DamageType _damageType;
    private readonly EnemyState _enemyState;
    private readonly SkillType _skillType;
    private readonly Expertise _expertise;
    private readonly DamageSource _damageSource;

    public FakeStatCalculator(double returnValue) => _value = returnValue;

    public FakeStatCalculator(double returnValue, DamageType damageType)
    {
        _value = returnValue;
        _damageType = damageType;
    }

    public FakeStatCalculator(double returnValue, EnemyState enemyState)
    {
        _value = returnValue;
        _enemyState = enemyState;
    }

    public FakeStatCalculator(double returnValue, SkillType skillType)
    {
        _value = returnValue;
        _skillType = skillType;
    }

    public FakeStatCalculator(double returnValue, Expertise expertise)
    {
        _value = returnValue;
        _expertise = expertise;
    }

    public FakeStatCalculator(double returnValue, DamageType damageType, EnemyState enemyState)
    {
        _value = returnValue;
        _damageType = damageType;
        _enemyState = enemyState;
    }

    public FakeStatCalculator(double returnValue, DamageType damageType, SkillType skillType)
    {
        _value = returnValue;
        _damageType = damageType;
        _skillType = skillType;
    }

    public FakeStatCalculator(double returnValue, EnemyState enemyState, SkillType skillType)
    {
        _value = returnValue;
        _enemyState = enemyState;
        _skillType = skillType;
    }

    public FakeStatCalculator(double returnValue, DamageType damageType, EnemyState enemyState, SkillType skillType)
    {
        _value = returnValue;
        _damageType = damageType;
        _enemyState = enemyState;
        _skillType = skillType;
    }

    public FakeStatCalculator(double returnValue, DamageType damageType, EnemyState enemyState, SkillType skillType, DamageSource damageSource)
    {
        _value = returnValue;
        _damageType = damageType;
        _enemyState = enemyState;
        _skillType = skillType;
        _damageSource = damageSource;
    }

    protected override double InstanceCalculate(SimulationState state) => _value;

    protected override double InstanceCalculate(SimulationState state, DamageType damageType) =>
        (_damageType == default || _damageType == damageType) ? _value : 0.0;

    protected override double InstanceCalculate(SimulationState state, EnemyState enemy) =>
        (_enemyState == null || _enemyState == enemy) ? _value : 0.0;

    protected override double InstanceCalculate(SimulationState state, SkillType skillType) =>
        (_skillType == default || _skillType == skillType) ? _value : 0.0;

    protected override double InstanceCalculate(SimulationState state, Expertise expertise) =>
        (_expertise == default || _expertise == expertise) ? _value : 0.0;

    protected override double InstanceCalculate(SimulationState state, DamageType damageType, EnemyState enemy) =>
        (_damageType == default || _damageType == damageType) &&
        (_enemyState == null || _enemyState == enemy) ? _value : 0.0;

    protected override double InstanceCalculate(SimulationState state, DamageType damageType, EnemyState enemy, SkillType skillType) =>
        (_damageType == default || _damageType == damageType) &&
        (_enemyState == null || _enemyState == enemy) &&
        (_skillType == default || _skillType == skillType) ? _value : 0.0;

    protected override double InstanceCalculate(SimulationState state, DamageType damageType, EnemyState enemy, SkillType skillType, DamageSource damageSource) =>
        (_damageType == default || _damageType == damageType) &&
        (_enemyState == null || _enemyState == enemy) &&
        (_skillType == default || _skillType == skillType) &&
        (_damageSource == default || _damageSource == damageSource) ? _value : 0.0;
}
