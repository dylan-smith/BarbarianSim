using BarbarianSim.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.Gear.Aspects
{
    public class RageOfHarrogath : Aspect
    {
        public int Chance { get; init; }

        public RageOfHarrogath(int chance)
        {
            Chance = chance;
        }
    }
}
