using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class ThornsCalculatorTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    public ThornsCalculatorTests() => BaseStatCalculator.InjectMock(typeof(MaxLifeCalculator), new FakeStatCalculator(1000));

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.Thorns = 100;
        config.Gear.Chest.Thorns = 100;
        var state = new SimulationState(config);

        var result = ThornsCalculator.Calculate(state);

        result.Should().Be(200);
    }

    [Fact]
    public void Includes_Bonus_From_StrategicChallengingShout()
    {
        var config = new SimulationConfig();
        config.Skills.Add(Skill.StrategicChallengingShout, 1);
        var state = new SimulationState(config);
        state.Player.Auras.Add(Aura.ChallengingShout);

        var result = ThornsCalculator.Calculate(state);

        result.Should().Be(300);
    }

    [Fact]
    public void Returns_0_When_No_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig());

        var result = ThornsCalculator.Calculate(state);

        result.Should().Be(0);
    }
}
