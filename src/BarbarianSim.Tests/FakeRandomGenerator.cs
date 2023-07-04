using BarbarianSim.Enums;

namespace BarbarianSim.Tests;

public class FakeRandomGenerator : RandomGenerator
{
    private readonly IList<double> _values;

    private readonly IDictionary<RollType, double[]> _typeValues = new Dictionary<RollType, double[]>();

    public FakeRandomGenerator(params double[] values) => _values = new List<double>(values);

    public FakeRandomGenerator(RollType type, double value) => SetRolls(type, value);

    public void SetRolls(RollType type, params double[] values)
    {
        if (_typeValues.ContainsKey(type))
        {
            _typeValues.Remove(type);
        }

        _typeValues.Add(type, values);
    }

    protected override double RollImplementation(RollType type)
    {
        if (_typeValues.ContainsKey(type) && _typeValues[type].Any())
        {
            var result = _typeValues[type].First();
            _typeValues[type] = _typeValues[type].SkipLast(1).ToArray();
            return result;
        }

        if (_values.Any())
        {
            var result = _values.First();
            _values.RemoveAt(0);
            return result;
        }

        // TODO: Should probably throw an exception if there's no value found in typeValues

        return 0.0;
    }
}
