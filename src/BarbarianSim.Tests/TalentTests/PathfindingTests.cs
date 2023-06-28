using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests.TalentTests
{
    [TestClass]
    public class PathfindingTests
    {
        [TestMethod]
        public void PathfindingCheetah()
        {
            var state = new SimulationState();
            state.Config.Talents.Add(Talent.Pathfinding, 2);
            state.Auras.Add(Aura.AspectOfTheCheetah);

            Assert.AreEqual(1.38, MovementSpeedCalculator.Calculate(state), 0.00001);
        }

        [TestMethod]
        public void PathfindingPack()
        {
            var state = new SimulationState();
            state.Config.Talents.Add(Talent.Pathfinding, 1);
            state.Auras.Add(Aura.AspectOfThePack);

            Assert.AreEqual(1.34, MovementSpeedCalculator.Calculate(state), 0.00001);
        }
    }
}
