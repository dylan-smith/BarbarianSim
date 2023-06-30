using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.StatCalculators
{
    public class MultiplicativeDamageBonusCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<MultiplicativeDamageBonusCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            return 0.0;
        }
    }
}
