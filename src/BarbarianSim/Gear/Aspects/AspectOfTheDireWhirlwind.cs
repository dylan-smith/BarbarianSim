using BarbarianSim.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.Gear.Aspects
{
    public class AspectOfTheDireWhirlwind : Aspect
    {
        public int CritChance { get; init; }
        public int MaxCritChance { get; init; }
    }
}
