using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Paragon;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.Paragon;

public class WarbringerTests
{
    private readonly Mock<MaxLifeCalculator> _mockMaxLifeCalculator = TestHelpers.CreateMock<MaxLifeCalculator>();
    private readonly Mock<SimLogger> _mockSimLogger = TestHelpers.CreateMock<SimLogger>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly Warbringer _paragon;

    public WarbringerTests()
    {
        _mockMaxLifeCalculator.Setup(m => m.Calculate(It.IsAny<SimulationState>())).Returns(1200);
        _paragon = new(_mockMaxLifeCalculator.Object, _mockSimLogger.Object);
        _state.Config.ParagonNodes.Add(ParagonNode.Warbringer);
    }

    [Fact]
    public void Creates_WarbringerProcEvent()
    {
        var furySpentEvent = new FurySpentEvent(0, null, 77, SkillType.None) { FurySpent = 77 };
        _state.ProcessedEvents.Add(furySpentEvent);

        _paragon.ProcessEvent(furySpentEvent, _state);

        _state.Events.Should().ContainSingle(e => e is WarbringerProcEvent);
        _state.Events.OfType<WarbringerProcEvent>().Single().Timestamp.Should().Be(0);
    }

    [Fact]
    public void Sums_All_FurySpentEvents()
    {
        _state.ProcessedEvents.Add(new WarbringerProcEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(123.5, null, 46, SkillType.None) { FurySpent = 46 });
        _state.ProcessedEvents.Add(new FurySpentEvent(127, null, 57, SkillType.None) { FurySpent = 57 });

        _paragon.ProcessEvent((FurySpentEvent)_state.ProcessedEvents.Last(), _state);

        _state.Events.Should().ContainSingle(e => e is WarbringerProcEvent);
        _state.Events.OfType<WarbringerProcEvent>().Single().Timestamp.Should().Be(127);
    }

    [Fact]
    public void Only_Looks_At_FurySpentEvents_After_Last_Proc()
    {
        _state.ProcessedEvents.Add(new WarbringerProcEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(123, null, 46, SkillType.None) { FurySpent = 46 });
        _state.ProcessedEvents.Add(new FurySpentEvent(127, null, 57, SkillType.None) { FurySpent = 57 });

        _paragon.ProcessEvent((FurySpentEvent)_state.ProcessedEvents.Last(), _state);

        _state.Events.Should().NotContain(e => e is WarbringerProcEvent);
    }

    [Fact]
    public void Does_Nothing_If_Paragon_Not_Active()
    {
        _state.Config.ParagonNodes.Remove(ParagonNode.Warbringer);
        var furySpentEvent = new FurySpentEvent(0, null, 77, SkillType.None) { FurySpent = 77 };
        _state.ProcessedEvents.Add(furySpentEvent);

        _paragon.ProcessEvent(furySpentEvent, _state);

        _state.Events.Should().NotContain(e => e is WarbringerProcEvent);
    }

    [Fact]
    public void Does_Nothing_If_Not_Enough_Fury_Spent()
    {
        var furySpentEvent = new FurySpentEvent(0, null, 74, SkillType.None) { FurySpent = 74 };
        _state.ProcessedEvents.Add(furySpentEvent);

        _paragon.ProcessEvent(furySpentEvent, _state);

        _state.Events.Should().NotContain(e => e is WarbringerProcEvent);
    }

    [Fact]
    public void Does_Not_Double_Count_FurySpentEvents()
    {
        _state.ProcessedEvents.Add(new FurySpentEvent(123, null, 76, SkillType.None) { FurySpent = 76 });
        _state.ProcessedEvents.Add(new WarbringerProcEvent(123));
        _state.ProcessedEvents.Add(new FurySpentEvent(124, null, 46, SkillType.None) { FurySpent = 46 });

        _paragon.ProcessEvent((FurySpentEvent)_state.ProcessedEvents.Last(), _state);

        _state.Events.Should().NotContain(e => e is WarbringerProcEvent);
    }

    [Fact]
    public void GetFortifyGenerated_Returns_12_Percent_Of_MaxLife()
    {
        _paragon.GetFortifyGenerated(_state).Should().Be(144);
    }
}
