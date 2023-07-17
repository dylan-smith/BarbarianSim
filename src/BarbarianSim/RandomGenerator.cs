using BarbarianSim.Enums;

namespace BarbarianSim;

public class RandomGenerator
{
    private Random _random;

    public RandomGenerator() => _random = new Random();

    public RandomGenerator(int seed) => _random = new Random(seed);

    public virtual double Roll(RollType type) => _random.NextDouble();

    public virtual void Seed(int seed)
    {
        _random = new Random(seed);
    }
}
