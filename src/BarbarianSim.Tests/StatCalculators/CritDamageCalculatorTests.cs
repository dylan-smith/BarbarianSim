using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class CritDamageCalculatorTests
{
    [Fact]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.CritDamage = 12.0;
        var state = new SimulationState(config);

        var result = CritDamageCalculator.Calculate(state, Expertise.NA);

        // 1.5 base + 0.12 from helm == 1.62
        result.Should().Be(1.62);
    }

    [Fact]
    public void Base_Crit_Is_50()
    {
        var config = new SimulationConfig();
        var state = new SimulationState(config);

        var result = CritDamageCalculator.Calculate(state, Expertise.NA);

        result.Should().Be(1.50);
    }

    [Fact]
    public void Includes_HeavyHanded_Bonus()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.HeavyHanded, 3);

        var result = CritDamageCalculator.Calculate(state, Expertise.TwoHandedSword);

        result.Should().Be(1.59);
    }
}
