using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Paragon;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class DamageReductionCalculatorTests
{
    private readonly Mock<DamageReductionFromBleedingCalculator> _mockDamageReductionFromBleedingCalculator = TestHelpers.CreateMock<DamageReductionFromBleedingCalculator>();
    private readonly Mock<DamageReductionFromCloseCalculator> _mockDamageReductionFromCloseCalculator = TestHelpers.CreateMock<DamageReductionFromCloseCalculator>();
    private readonly Mock<DamageReductionWhileFortifiedCalculator> _mockDamageReductionWhileFortifiedCalculator = TestHelpers.CreateMock<DamageReductionWhileFortifiedCalculator>();
    private readonly Mock<DamageReductionWhileInjuredCalculator> _mockDamageReductionWhileInjuredCalculator = TestHelpers.CreateMock<DamageReductionWhileInjuredCalculator>();
    private readonly Mock<AggressiveResistance> _mockAggressiveResistance = TestHelpers.CreateMock<AggressiveResistance>();
    private readonly Mock<ChallengingShout> _mockChallengingShout = TestHelpers.CreateMock<ChallengingShout>();
    private readonly Mock<GutteralYell> _mockGutteralYell = TestHelpers.CreateMock<GutteralYell>();
    private readonly Mock<AspectOfTheIronWarrior> _mockAspectOfTheIronWarrior = TestHelpers.CreateMock<AspectOfTheIronWarrior>();
    private readonly Mock<IronBloodAspect> _mockIronBloodAspect = TestHelpers.CreateMock<IronBloodAspect>();
    private readonly Mock<Undaunted> _mockUndaunted = TestHelpers.CreateMock<Undaunted>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly DamageReductionCalculator _calculator;

    public DamageReductionCalculatorTests()
    {
        _mockDamageReductionFromBleedingCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>(), It.IsAny<EnemyState>())).Returns(0.0);
        _mockDamageReductionFromCloseCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _mockDamageReductionWhileFortifiedCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _mockDamageReductionWhileInjuredCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(0.0);
        _mockAggressiveResistance.Setup(m => m.GetDamageReduction(It.IsAny<SimulationState>())).Returns(0.0);
        _mockChallengingShout.Setup(m => m.GetDamageReduction(It.IsAny<SimulationState>())).Returns(0.0);
        _mockGutteralYell.Setup(m => m.GetDamageReduction(It.IsAny<SimulationState>())).Returns(0.0);
        _mockAspectOfTheIronWarrior.Setup(m => m.GetDamageReductionBonus(It.IsAny<SimulationState>())).Returns(0.0);
        _mockIronBloodAspect.Setup(m => m.GetDamageReductionBonus(It.IsAny<SimulationState>())).Returns(0.0);
        _mockUndaunted.Setup(m => m.GetDamageReduction(It.IsAny<SimulationState>())).Returns(0.0);

        _calculator = new DamageReductionCalculator(
            _mockDamageReductionFromBleedingCalculator.Object,
            _mockDamageReductionFromCloseCalculator.Object,
            _mockDamageReductionWhileFortifiedCalculator.Object,
            _mockDamageReductionWhileInjuredCalculator.Object,
            _mockAggressiveResistance.Object,
            _mockChallengingShout.Object,
            _mockGutteralYell.Object,
            _mockAspectOfTheIronWarrior.Object,
            _mockIronBloodAspect.Object,
            _mockUndaunted.Object,
            _mockSimLogger.Object);
    }

    [Fact]
    public void Returns_Base_DamageReduction_Of_10_Percent()
    {
        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.9);
    }

    [Fact]
    public void Includes_DamageReduction_From_Gear()
    {
        _state.Config.Gear.Helm.DamageReduction = 12.0;

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_DamageReduction_From_Paragon()
    {
        _state.Config.Paragon.DamageReduction = 12.0;

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_DamageReductionFromBleeding()
    {
        _mockDamageReductionFromBleedingCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First())).Returns(12.0);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_DamageReductionFromClose()
    {
        _mockDamageReductionFromCloseCalculator.Setup(m => m.Calculate(_state)).Returns(12.0);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_DamageReductionWhileFortified()
    {
        _mockDamageReductionWhileFortifiedCalculator.Setup(m => m.Calculate(_state)).Returns(12.0);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_DamageReductionWhileInjured()
    {
        _mockDamageReductionWhileInjuredCalculator.Setup(m => m.Calculate(_state)).Returns(12.0);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_Bonus_From_ChallengingShout()
    {
        _state.Player.Auras.Add(Aura.ChallengingShout);
        _mockChallengingShout.Setup(m => m.GetDamageReduction(_state)).Returns(12.0);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_Bonus_From_GutteralYell()
    {
        _state.Player.Auras.Add(Aura.GutteralYell);
        _mockGutteralYell.Setup(m => m.GetDamageReduction(_state)).Returns(12.0);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_Bonus_From_AggressiveResistance()
    {
        _mockAggressiveResistance.Setup(m => m.GetDamageReduction(_state)).Returns(12.0);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_Bonus_From_AspectOfTheIronWarrior()
    {
        _mockAspectOfTheIronWarrior.Setup(m => m.GetDamageReductionBonus(_state)).Returns(12.0);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_Bonus_From_IronBloodAspect()
    {
        _mockIronBloodAspect.Setup(m => m.GetDamageReductionBonus(_state)).Returns(12.0);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.792);
    }

    [Fact]
    public void Includes_Bonus_From_Undaunted()
    {
        _mockUndaunted.Setup(m => m.GetDamageReduction(_state)).Returns(10.0);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().Be(0.9 * 0.9);
    }

    [Fact]
    public void Multiplies_Damage_Reduction_Bonuses()
    {
        _state.Config.Gear.Helm.DamageReduction = 8.0;

        _mockDamageReductionFromBleedingCalculator.Setup(m => m.Calculate(_state, _state.Enemies.First())).Returns(8.0);
        _mockDamageReductionFromCloseCalculator.Setup(m => m.Calculate(_state)).Returns(8.0);
        _mockDamageReductionWhileFortifiedCalculator.Setup(m => m.Calculate(_state)).Returns(8.0);
        _mockDamageReductionWhileInjuredCalculator.Setup(m => m.Calculate(_state)).Returns(8.0);
        _mockAggressiveResistance.Setup(m => m.GetDamageReduction(_state)).Returns(3.0);
        _state.Player.Auras.Add(Aura.ChallengingShout);
        _mockChallengingShout.Setup(m => m.GetDamageReduction(_state)).Returns(42.0);
        _state.Player.Auras.Add(Aura.GutteralYell);
        _mockGutteralYell.Setup(m => m.GetDamageReduction(_state)).Returns(12.0);

        var result = _calculator.Calculate(_state, _state.Enemies.First());

        result.Should().BeApproximately(0.9 * 0.92 * 0.92 * 0.92 * 0.92 * 0.92 * 0.97 * 0.58 * 0.88, 0.0000001);
    }
}
