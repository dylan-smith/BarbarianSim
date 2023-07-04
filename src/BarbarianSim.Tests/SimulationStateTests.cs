using BarbarianSim.Config;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests;

public class SimulationStateTests
{
    [Fact]
    public void Sets_Enemy_Life_And_MaxLife_From_Config()
    {
        var config = new SimulationConfig();
        config.EnemySettings.Life = 1234;

        var state = new SimulationState(config);

        state.Enemy.MaxLife.Should().Be(1234);
        state.Enemy.Life.Should().Be(1234);
    }

    [Fact]
    public void Validates_Config_And_Captures_Errors_And_Warnings()
    {
        var mockConfig = new Mock<SimulationConfig>();
        var warnings = new List<string>() { "111", "222" };
        var errors = new List<string>() { "333", "444" };

        mockConfig.Setup(m => m.Validate()).Returns((warnings, errors));

        var state = new SimulationState(mockConfig.Object);
        var result = state.Validate();

        result.Should().BeFalse();
        state.Warnings.Should().HaveCount(2);
        state.Errors.Should().HaveCount(2);
        state.Warnings.Should().Contain("111");
        state.Warnings.Should().Contain("222");
        state.Errors.Should().Contain("333");
        state.Errors.Should().Contain("444");
    }

    [Fact]
    public void Validate_Succeeds_When_Only_Warnings_No_Errors()
    {
        var mockConfig = new Mock<SimulationConfig>();
        var warnings = new List<string>() { "111", "222" };
        var errors = new List<string>();

        mockConfig.Setup(m => m.Validate()).Returns((warnings, errors));

        var state = new SimulationState(mockConfig.Object);
        var result = state.Validate();

        result.Should().BeTrue();
    }
}
