using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Paragon;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Paragon;

public class UndauntedTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly Undaunted _paragon;

    public UndauntedTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(1200);
        _paragon = new(_mockMaxLifeCalculator.Object);
        _state.Config.ParagonNodes.Add(ParagonNode.Undaunted);
    }

    [Fact]
    public void GetDamageReduction_Returns_10_At_Max_Fortify()
    {
        _state.Player.Fortify = 1200;
        _paragon.GetDamageReduction(_state).Should().Be(10);
    }

    [Fact]
    public void GetDamageReduction_Returns_0_If_Not_Active()
    {
        _state.Config.ParagonNodes.Remove(ParagonNode.Undaunted);
        _state.Player.Fortify = 1200;
        _paragon.GetDamageReduction(_state).Should().Be(0);
    }

    [InlineData(0, 0)]
    [InlineData(402, 3.35)]
    [InlineData(600, 5)]
    [InlineData(1002, 8.35)]
    [InlineData(1200, 10)]
    [Theory]
    public void GetDamageReduction_Should_Scale_With_Fortify(double fortify, double damageReduction)
    {
        _state.Player.Fortify = fortify;
        _paragon.GetDamageReduction(_state).Should().Be(damageReduction);
    }
}
