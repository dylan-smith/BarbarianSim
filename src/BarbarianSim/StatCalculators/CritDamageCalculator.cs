using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.StatCalculators
{
    public class CritDamageCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<CritDamageCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            return 0.0;
        }
    }
}
