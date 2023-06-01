using System;

namespace HunterSim
{
    public class RandomGenerator
    {
        private static RandomGenerator _instance;
        private readonly Random _random;

        public static void Seed(int seed) => _instance = new RandomGenerator(seed);

        public static double Roll(RollType type)
        {
            if (_instance == null)
            {
                _instance = new RandomGenerator();
            }

            return _instance.RollImplementation(type);
        }

        public RandomGenerator() => _random = new Random();

        public RandomGenerator(int seed) => _random = new Random(seed);

        public static void InjectMock(RandomGenerator mock) => _instance = mock;

        public static void ClearMock() => _instance = null;

        protected virtual double RollImplementation(RollType _) => _random.NextDouble();
    }
}
