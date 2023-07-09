using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class MaxFuryCalculatorTests
{
    [Fact]
    public void Includes_Base_Fury()
    {
        var state = new SimulationState(new SimulationConfig());

        var result = MaxFuryCalculator.Calculate(state);

        result.Should().Be(100);
    }

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.MaxFury = 8;
        config.Gear.Chest.MaxFury = 12;
        var state = new SimulationState(config);

        var result = MaxFuryCalculator.Calculate(state);

        result.Should().Be(120);
    }

    [Fact]
    public void Includes_Bonus_From_TemperedFury()
    {
        var config = new SimulationConfig();
        config.Skills.Add(Skill.TemperedFury, 2);
        var state = new SimulationState(config);

        var result = MaxFuryCalculator.Calculate(state);

        result.Should().Be(106);
    }
}
