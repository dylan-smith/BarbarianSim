using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class FuryCostReductionCalculatorTests
{
    [Fact]
    public void Includes_Stats_From_Gear()
    {
        var config = new SimulationConfig();
        config.Gear.Helm.FuryCostReduction = 12.0;
        var state = new SimulationState(config);

        var result = FuryCostReductionCalculator.Calculate(state, SkillType.Basic);

        result.Should().Be(0.88);
    }

    [Fact]
    public void Double_The_Cost_With_Unbridled_Rage()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = FuryCostReductionCalculator.Calculate(state, SkillType.Core);

        result.Should().Be(2.0);
    }

    [Fact]
    public void Unbridled_Rage_Only_Doubles_Core_Skills()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.UnbridledRage, 1);

        var result = FuryCostReductionCalculator.Calculate(state, SkillType.Basic);

        result.Should().Be(1.0);
    }

    [Fact]
    public void Unbridled_Rage_Multiplies_Properly_With_Other_Bonuses()
    {
        var state = new SimulationState(new SimulationConfig());
        state.Config.Skills.Add(Skill.UnbridledRage, 1);
        state.Config.Gear.Helm.FuryCostReduction = 12.0;

        var result = FuryCostReductionCalculator.Calculate(state, SkillType.Core);

        result.Should().Be(1.76);
    }
}
