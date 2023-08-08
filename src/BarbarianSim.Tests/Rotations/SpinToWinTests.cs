using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Rotations;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Rotations;

public class SpinToWinTests
{
    private readonly Mock<RallyingCry> _mockRallyingCry = TestHelpers.CreateMock<RallyingCry>();
    private readonly Mock<ChallengingShout> _mockChallengingShout = TestHelpers.CreateMock<ChallengingShout>();
    private readonly Mock<WarCry> _mockWarCry = TestHelpers.CreateMock<WarCry>();
    private readonly Mock<WrathOfTheBerserker> _mockWrathOfTheBerserker = TestHelpers.CreateMock<WrathOfTheBerserker>();
    private readonly Mock<Whirlwind> _mockWhirlwind = TestHelpers.CreateMock<Whirlwind>();
    private readonly Mock<LungingStrike> _mockLungingStrike = TestHelpers.CreateMock<LungingStrike>();
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly SpinToWin _rotation;

    public SpinToWinTests()
    {
        _mockRallyingCry.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(true);
        _mockChallengingShout.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(true);
        _mockWarCry.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(true);
        _mockWrathOfTheBerserker.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(true);
        _mockWhirlwind.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(true);
        _mockLungingStrike.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(true);

        _rotation = new SpinToWin(_mockRallyingCry.Object,
                                  _mockChallengingShout.Object,
                                  _mockWarCry.Object,
                                  _mockWrathOfTheBerserker.Object,
                                  _mockWhirlwind.Object,
                                  _mockLungingStrike.Object);
    }

    [Fact]
    public void Uses_LungingStrike_Whirlwind_Unavailable()
    {
        _mockWhirlwind.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(false);

        _rotation.Execute(_state);

        _mockLungingStrike.Verify(m => m.Use(_state, _state.Enemies.First()));
    }

    [Fact]
    public void Uses_Whirlwind_When_Available()
    {
        _rotation.Execute(_state);

        _mockWhirlwind.Verify(m => m.Use(_state));
    }

    [Fact]
    public void Stops_Whirlwind_When_Gohrs_Reaches_Max_HitCount()
    {
        _state.Player.Auras.Add(Aura.Whirlwinding);
        _state.CurrentTime = 123;
        var aspect = new GohrsDevastatingGrips(new Mock<SimLogger>().Object)
        {
            HitCount = GohrsDevastatingGrips.MAX_HIT_COUNT
        };
        _state.Config.Gear.Helm.Aspect = aspect;
        _mockWhirlwind.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(false);

        _rotation.Execute(_state);

        _mockWhirlwind.Verify(m => m.StopSpinning(_state));
    }

    [Fact]
    public void Uses_All_Shouts_When_Not_On_Cooldown()
    {
        _rotation.Execute(_state);

        _mockRallyingCry.Verify(m => m.Use(_state));
        _mockChallengingShout.Verify(m => m.Use(_state));
        _mockWarCry.Verify(m => m.Use(_state));
    }

    [Fact]
    public void Uses_WrathOfTheBerserker_When_Not_On_Cooldown()
    {
        _rotation.Execute(_state);

        _mockWrathOfTheBerserker.Verify(m => m.Use(_state));
    }

    [Fact]
    public void Does_Nothing_When_Shouts_On_Cooldown_Wrath_On_Cooldown_And_No_Fury()
    {
        _mockRallyingCry.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(false);
        _mockChallengingShout.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(false);
        _mockWarCry.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(false);
        _mockWrathOfTheBerserker.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(false);
        _mockWhirlwind.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(false);
        _mockLungingStrike.Setup(m => m.CanUse(It.IsAny<SimulationState>())).Returns(false);

        _rotation.Execute(_state);

        _state.Events.Should().BeEmpty();
        _mockRallyingCry.Verify(m => m.Use(It.IsAny<SimulationState>()), Times.Never);
        _mockChallengingShout.Verify(m => m.Use(It.IsAny<SimulationState>()), Times.Never);
        _mockWarCry.Verify(m => m.Use(It.IsAny<SimulationState>()), Times.Never);
        _mockWrathOfTheBerserker.Verify(m => m.Use(It.IsAny<SimulationState>()), Times.Never);
        _mockWhirlwind.Verify(m => m.Use(It.IsAny<SimulationState>()), Times.Never);
        _mockLungingStrike.Verify(m => m.Use(It.IsAny<SimulationState>(), It.IsAny<EnemyState>()), Times.Never);
    }
}
