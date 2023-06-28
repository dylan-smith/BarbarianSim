using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BarbarianSim.Tests.TalentTests
{
    [TestClass]
    public class ExposeWeaknessTests
    {
        [TestCleanup]
        public void TestCleanup()
        {
            BaseStatCalculator.ClearMocks();
            RandomGenerator.ClearMock();
        }

        [TestMethod]
        public void ExposeWeaknessProc()
        {
            var state = new SimulationState();
            state.Config.Talents.Add(Talent.ExposeWeakness, 3);

            var e = new AutoShotCompletedEvent(7.7)
            {
                DamageEvent = new DamageEvent(7.7, 100.0, DamageType.Crit, 0.0, 1.0, 0.0)
            };

            EventPublisher.PublishEvent(e, state);

            Assert.AreEqual(1, state.Events.Count);
            Assert.AreEqual(typeof(ExposeWeaknessProcEvent), state.Events.Single().GetType());
            Assert.AreEqual(7.7, state.Events.Single().Timestamp, 0.00001);
        }

        [TestMethod]
        public void ExposeWeaknessDidNotProc()
        {
            var state = new SimulationState();
            state.Config.Talents.Add(Talent.ExposeWeakness, 2);

            var e = new AutoShotCompletedEvent(7.7)
            {
                DamageEvent = new DamageEvent(7.7, 100.0, DamageType.Crit, 0.0, 1.0, 0.0)
            };

            RandomGenerator.InjectMock(new FakeRandomGenerator(RollType.ExposeWeaknessProc, 0.7));

            EventPublisher.PublishEvent(e, state);

            Assert.AreEqual(0, state.Events.Count);
        }

        [TestMethod]
        public void ExposeWeaknessProcEvent()
        {
            var state = new SimulationState();

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(700.0));

            new ExposeWeaknessProcEvent(7.7).ProcessEvent(state);

            Assert.IsTrue(state.Auras.Contains(Aura.ExposeWeakness));
            Assert.AreEqual(700 / 4, ExposeWeakness.AttackPower);

            Assert.AreEqual(1, state.Events.Count);
            Assert.AreEqual(typeof(ExposeWeaknessExpiredEvent), state.Events.Single().GetType());
            Assert.AreEqual(14.7, state.Events.Single().Timestamp, 0.00001);
        }

        [TestMethod]
        public void ExposeWeaknessProcAlreadyActive()
        {
            var state = new SimulationState();
            state.Auras.Add(Aura.ExposeWeakness);
            state.Events.Add(new ExposeWeaknessExpiredEvent(12.1));

            BaseStatCalculator.InjectMock(typeof(AgilityCalculator), new FakeStatCalculator(700.0));

            new ExposeWeaknessProcEvent(7.7).ProcessEvent(state);

            Assert.IsTrue(state.Auras.Contains(Aura.ExposeWeakness));
            Assert.AreEqual(1, state.Events.Count);
            Assert.AreEqual(typeof(ExposeWeaknessExpiredEvent), state.Events.Single().GetType());
            Assert.AreEqual(14.7, state.Events.Single().Timestamp, 0.00001);
        }

        [TestMethod]
        public void ExposeWeaknessExpiredEvent()
        {
            var state = new SimulationState();
            state.Auras.Add(Aura.ExposeWeakness);

            new ExposeWeaknessExpiredEvent(14.7).ProcessEvent(state);

            Assert.IsFalse(state.Auras.Contains(Aura.ExposeWeakness));
        }

        [TestMethod]
        public void ExposeWeaknessAttackPowerBuff()
        {
            var state = new SimulationState();
            state.Config.PlayerSettings.Race = Race.Draenei;
            state.Auras.Add(Aura.ExposeWeakness);
            ExposeWeakness.AttackPower = 198;

            Assert.AreEqual(Constants.DRAENEI_AGI + Constants.BASE_RAP + 198, RangedAttackPowerCalculator.Calculate(state));
            Assert.AreEqual(Constants.DRAENEI_AGI + Constants.DRAENEI_STR + Constants.BASE_MAP + 198, MeleeAttackPowerCalculator.Calculate(state));
        }
    }
}
