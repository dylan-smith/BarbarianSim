using System;
using System.Collections.Generic;

namespace HunterSim
{
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

        protected static double Calculate<T>(GearItem weapon, SimulationState state) where T : BaseStatCalculator
        {
            if (!_instances.ContainsKey(typeof(T)))
            {
                _instances.Add(typeof(T), Activator.CreateInstance<T>());
            }

            return _instances[typeof(T)].InstanceCalculate(weapon, state);
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

        protected virtual double InstanceCalculate(GearItem weapon, SimulationState state) => 0.0;
    }
}
