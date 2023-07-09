using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class ResourceGenerationCalculatorTests : IDisposable
{
    public void Dispose() => BaseStatCalculator.ClearMocks();

    [Fact]
    public void Returns_1_By_Default()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(0.0));

        var result = ResourceGenerationCalculator.Calculate(state);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Includes_ResourceGeneration_Gear_Bonus()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.ResourceGeneration = 42;
        var state = new SimulationState(config);
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(0.0));

        var result = ResourceGenerationCalculator.Calculate(state);

        result.Should().Be(1.42);
    }

    [Fact]
    public void Includes_Willpower_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(400.0));

        var result = ResourceGenerationCalculator.Calculate(state);

        result.Should().Be(1.12);
    }

    [Fact]
    public void Includes_RallyingCry_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(0.0));
        state.Config.Skills.Add(Skill.RallyingCry, 5);
        state.Player.Auras.Add(Aura.RallyingCry);

        var result = ResourceGenerationCalculator.Calculate(state);

        result.Should().Be(1.56);
    }

    [Fact]
    public void RallyingCry_Bonus_And_Gear_Multiply_Together()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(0.0));
        state.Config.Skills.Add(Skill.RallyingCry, 5);
        state.Player.Auras.Add(Aura.RallyingCry);
        state.Config.Gear.Helm.ResourceGeneration = 30;

        var result = ResourceGenerationCalculator.Calculate(state);

        result.Should().Be(2.028); // (1 + 30%) * 1.56
    }

    [Fact]
    public void TacticalRallyingCry_Bonus_Multiplied_In()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(0.0));
        state.Config.Skills.Add(Skill.RallyingCry, 5);
        state.Config.Skills.Add(Skill.TacticalRallyingCry, 1);
        state.Player.Auras.Add(Aura.RallyingCry);
        state.Config.Gear.Helm.ResourceGeneration = 30;

        var result = ResourceGenerationCalculator.Calculate(state);

        result.Should().BeApproximately(2.4336, 0.00000001); // (1 + 30%) * 1.56 * 1.2
    }

    [Fact]
    public void ProlificFury_Bonus_Multiplied_In()
    {
        var state = new SimulationState(new SimulationConfig());
        BaseStatCalculator.InjectMock(typeof(WillpowerCalculator), new FakeStatCalculator(0.0));
        state.Config.Skills.Add(Skill.ProlificFury, 3);
        state.Player.Auras.Add(Aura.Berserking);
        state.Config.Gear.Helm.ResourceGeneration = 30;

        var result = ResourceGenerationCalculator.Calculate(state);

        result.Should().Be(1.534); // (1 + 30%) * 1.18
    }
}
