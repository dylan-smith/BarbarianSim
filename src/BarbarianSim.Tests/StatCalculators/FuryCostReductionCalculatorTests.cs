using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public sealed class FuryCostReductionCalculatorTests
{
    private readonly Mock<UnbridledRage> _mockUnbridledRage = TestHelpers.CreateMock<UnbridledRage>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly FuryCostReductionCalculator _calculator;

    public FuryCostReductionCalculatorTests()
    {
        _mockUnbridledRage.Setup(m => m.GetFuryCostReduction(It.IsAny<SimulationState>(), It.IsAny<SkillType>()))
                           .Returns(1.0);

        _calculator = new FuryCostReductionCalculator(_mockUnbridledRage.Object);
    }

    [Fact]
    public void Includes_Stats_From_Gear()
    {
        _state.Config.Gear.Helm.FuryCostReduction = 12.0;

        var result = _calculator.Calculate(_state, SkillType.Basic);

        result.Should().Be(0.88);
    }

    [Fact]
    public void Includes_Stats_From_Paragon()
    {
        _state.Config.Paragon.FuryCostReduction = 12.0;

        var result = _calculator.Calculate(_state, SkillType.Basic);

        result.Should().Be(0.88);
    }

    [Fact]
    public void Unbridled_Rage_Multiplies_Properly_With_Other_Bonuses()
    {
        _state.Config.Gear.Helm.FuryCostReduction = 12.0;
        _mockUnbridledRage.Setup(m => m.GetFuryCostReduction(_state, SkillType.Core))
                           .Returns(2);

        var result = _calculator.Calculate(_state, SkillType.Core);

        result.Should().Be(1.76);
    }
}
