﻿using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public abstract class BaseStatCalculator
{
    private static IDictionary<Type, BaseStatCalculator> _instances = new Dictionary<Type, BaseStatCalculator>();

    protected static double Calculate<T>(SimulationState state) where T : BaseStatCalculator
    {
        if (!_instances.ContainsKey(typeof(T)))
        {
            _instances.Add(typeof(T), Activator.CreateInstance<T>());
        }

        return _instances[typeof(T)].InstanceCalculate(state);
    }

    protected static double Calculate<T>(SimulationState state, DamageType damageType) where T : BaseStatCalculator
    {
        if (!_instances.ContainsKey(typeof(T)))
        {
            _instances.Add(typeof(T), Activator.CreateInstance<T>());
        }

        return _instances[typeof(T)].InstanceCalculate(state, damageType);
    }

    protected static double Calculate<T>(SimulationState state, EnemyState enemy) where T : BaseStatCalculator
    {
        if (!_instances.ContainsKey(typeof(T)))
        {
            _instances.Add(typeof(T), Activator.CreateInstance<T>());
        }

        return _instances[typeof(T)].InstanceCalculate(state, enemy);
    }

    protected static double Calculate<T>(SimulationState state, SkillType skillType) where T : BaseStatCalculator
    {
        if (!_instances.ContainsKey(typeof(T)))
        {
            _instances.Add(typeof(T), Activator.CreateInstance<T>());
        }

        return _instances[typeof(T)].InstanceCalculate(state, skillType);
    }

    protected static double Calculate<T>(SimulationState state, Expertise expertise) where T : BaseStatCalculator
    {
        if (!_instances.ContainsKey(typeof(T)))
        {
            _instances.Add(typeof(T), Activator.CreateInstance<T>());
        }

        return _instances[typeof(T)].InstanceCalculate(state, expertise);
    }

    protected static double Calculate<T>(SimulationState state, DamageType damageType, EnemyState enemy) where T : BaseStatCalculator
    {
        if (!_instances.ContainsKey(typeof(T)))
        {
            _instances.Add(typeof(T), Activator.CreateInstance<T>());
        }

        return _instances[typeof(T)].InstanceCalculate(state, damageType, enemy);
    }

    protected static double Calculate<T>(SimulationState state, DamageType damageType, EnemyState enemy, SkillType skillType) where T : BaseStatCalculator
    {
        if (!_instances.ContainsKey(typeof(T)))
        {
            _instances.Add(typeof(T), Activator.CreateInstance<T>());
        }

        return _instances[typeof(T)].InstanceCalculate(state, damageType, enemy, skillType);
    }

    protected static double Calculate<T>(SimulationState state, DamageType damageType, EnemyState enemy, SkillType skillType, DamageSource damageSource) where T : BaseStatCalculator
    {
        if (!_instances.ContainsKey(typeof(T)))
        {
            _instances.Add(typeof(T), Activator.CreateInstance<T>());
        }

        return _instances[typeof(T)].InstanceCalculate(state, damageType, enemy, skillType, damageSource);
    }

    public static void InjectMock(Type type, BaseStatCalculator mock)
    {
        if (_instances.ContainsKey(type))
        {
            _instances.Remove(type);
        }

        _instances.Add(type, mock);
    }

    public static void ClearMocks() => _instances = new Dictionary<Type, BaseStatCalculator>();

    protected virtual double InstanceCalculate(SimulationState state) => 0.0;

    protected virtual double InstanceCalculate(SimulationState state, DamageType damageType) => 0.0;

    protected virtual double InstanceCalculate(SimulationState state, EnemyState enemy) => 0.0;

    protected virtual double InstanceCalculate(SimulationState state, SkillType skillType) => 0.0;

    protected virtual double InstanceCalculate(SimulationState state, Expertise expertise) => 0.0;

    protected virtual double InstanceCalculate(SimulationState state, SkillType skillType, Expertise expertise) => 0.0;

    protected virtual double InstanceCalculate(SimulationState state, DamageType damageType, EnemyState enemy) => 0.0;

    protected virtual double InstanceCalculate(SimulationState state, DamageType damageType, EnemyState enemy, SkillType skillType) => 0.0;

    protected virtual double InstanceCalculate(SimulationState state, DamageType damageType, EnemyState enemy, SkillType skillType, DamageSource damageSource) => 0.0;
}
