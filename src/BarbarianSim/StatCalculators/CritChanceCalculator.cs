using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.StatCalculators
{
    public class CritChanceCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<CritChanceCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            return 0.0;
        }
    }
}
