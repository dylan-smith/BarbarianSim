using BarbarianSim.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BarbarianSim.Tests
{
    [TestClass]
    public class SimulationStateTests
    {
        [TestMethod]
        public void Sets_Enemy_Life_And_MaxLife_From_Config()
        {
            var config = new SimulationConfig();
            config.EnemySettings.Life = 1234;

            var state = new SimulationState(config);

            Assert.AreEqual(1234, state.Enemy.MaxLife);
            Assert.AreEqual(1234, state.Enemy.Life);
        }
    }
}
