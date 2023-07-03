using BarbarianSim.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

        [TestMethod]
        public void Validates_Config_And_Captures_Errors_And_Warnings()
        {
            var mockConfig = new Mock<SimulationConfig>();
            var warnings = new List<string>() { "111", "222" };
            var errors = new List<string>() { "333", "444" };

            mockConfig.Setup(m => m.Validate()).Returns((warnings, errors));

            var state = new SimulationState(mockConfig.Object);
            var result = state.Validate();

            Assert.IsFalse(result);
            Assert.AreEqual(2, state.Warnings.Count);
            Assert.AreEqual(2, state.Errors.Count);
            Assert.IsTrue(state.Warnings.Contains("111"));
            Assert.IsTrue(state.Warnings.Contains("222"));
            Assert.IsTrue(state.Errors.Contains("333"));
            Assert.IsTrue(state.Errors.Contains("444"));
        }

        [TestMethod]
        public void Validate_Succeeds_When_Only_Warnings_No_Errors()
        {
            var mockConfig = new Mock<SimulationConfig>();
            var warnings = new List<string>() { "111", "222" };
            var errors = new List<string>();

            mockConfig.Setup(m => m.Validate()).Returns((warnings, errors));

            var state = new SimulationState(mockConfig.Object);
            var result = state.Validate();

            Assert.IsTrue(result);
        }
    }
}
