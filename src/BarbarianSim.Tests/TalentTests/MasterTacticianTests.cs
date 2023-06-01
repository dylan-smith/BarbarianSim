using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace HunterSim.Tests.TalentTests
{
    [TestClass]
    public class MasterTacticianTests
    {
        [TestCleanup]
        public void TestCleanup()
        {
            BaseStatCalculator.ClearMocks();
            RandomGenerator.ClearMock();
        }

        [TestMethod]
        public void MasterTacticianProc()
        {
            var state = new SimulationState();
            state.Config.Talents.Add(Talent.MasterTactician, 5);

            var e = new AutoShotCompletedEvent(7.7)
            {
                DamageEvent = new DamageEvent(7.7, 100.0, DamageType.Hit, 0.0, 1.0, 0.0)
            };

            RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.MasterTacticianProc, 0.05));

            EventPublisher.PublishEvent(e, state);

            Assert.AreEqual(1, state.Events.Count);
            Assert.AreEqual(typeof(MasterTacticianProcEvent), state.Events.Single().GetType());
            Assert.AreEqual(7.7, state.Events.Single().Timestamp, 0.00001);
        }

        [TestMethod]
        public void MasterTacticianDidNotProc()
        {
            var state = new SimulationState();
            state.Config.Talents.Add(Talent.MasterTactician, 5);

            var e = new AutoShotCompletedEvent(7.7)
            {
                DamageEvent = new DamageEvent(7.7, 100.0, DamageType.Hit, 0.0, 1.0, 0.0)
            };

            RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.MasterTacticianProc, 0.07));

            EventPublisher.PublishEvent(e, state);

            Assert.AreEqual(0, state.Events.Count);
        }

        [TestMethod]
        public void MasterTacticianProcEvent()
        {
            var state = new SimulationState();
            state.Config.Talents.Add(Talent.MasterTactician, 5);

            new MasterTacticianProcEvent(7.7).ProcessEvent(state);

            Assert.IsTrue(state.Auras.Contains(Aura.MasterTactician));

            Assert.AreEqual(1, state.Events.Count);
            Assert.AreEqual(typeof(MasterTacticianExpiredEvent), state.Events.Single().GetType());
            Assert.AreEqual(15.7, state.Events.Single().Timestamp, 0.00001);
        }

        [TestMethod]
        public void MasterTacticianProcAlreadyActive()
        {
            var state = new SimulationState();
            state.Auras.Add(Aura.MasterTactician);
            state.Events.Add(new MasterTacticianExpiredEvent(12.1));

            new MasterTacticianProcEvent(7.7).ProcessEvent(state);

            Assert.IsTrue(state.Auras.Contains(Aura.MasterTactician));
            Assert.AreEqual(1, state.Events.Count);
            Assert.AreEqual(typeof(MasterTacticianExpiredEvent), state.Events.Single().GetType());
            Assert.AreEqual(15.7, state.Events.Single().Timestamp, 0.00001);
        }

        [TestMethod]
        public void MasterTacticianExpiredEvent()
        {
            var state = new SimulationState();
            state.Auras.Add(Aura.MasterTactician);

            new MasterTacticianExpiredEvent(15.7).ProcessEvent(state);

            Assert.IsFalse(state.Auras.Contains(Aura.MasterTactician));
        }

        [TestMethod]
        public void MasterTacticianCritBuff()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Config.PlayerSettings.Level = 70;
            state.Config.BossSettings.Level = 73;
            state.Auras.Add(Aura.MasterTactician);
            state.Config.Talents.Add(Talent.MasterTactician, 4);

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(Constants.AGI_FOR_ZERO_CRIT));

            Assert.AreEqual(0.08, RangedCritCalculator.Calculate(state), 0.000001);
            Assert.AreEqual(0.08, MeleeCritCalculator.Calculate(state), 0.000001);
        }
    }
}
