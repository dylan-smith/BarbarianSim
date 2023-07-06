using BarbarianSim.Enums;

namespace BarbarianSim.Tests;

public class FakeRandomGenerator : RandomGenerator
{
    private readonly IDictionary<RollType, double[]> _typeValues = new Dictionary<RollType, double[]>();

    public FakeRandomGenerator(RollType type, params double[] values) => FakeRoll(type, values);

    public FakeRandomGenerator() { }

    public void FakeRoll(RollType type, params double[] values)
    {
        if (_typeValues.ContainsKey(type))
        {
            _typeValues.Remove(type);
        }

        _typeValues.Add(type, values);
    }

    protected override double RollImplementation(RollType type)
    {
        if (_typeValues.TryGetValue(type, out var value) && value.Any())
        {
            var result = value.First();
            if (value.Length > 1)
            {
                _typeValues[type] = value.Skip(1).ToArray();
            }
            return result;
        }

        return 0.0;
    }
}
