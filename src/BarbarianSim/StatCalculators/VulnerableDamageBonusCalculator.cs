using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.StatCalculators
{
    public class VulnerableDamageBonusCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<VulnerableDamageBonusCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            return 0.0;
        }
    }
}
