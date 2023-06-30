using BarbarianSim.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.Gear.Aspects
{
    public class AspectOfNumbingWraith : Aspect
    {
        public int Fortify { get; init; }

        public AspectOfNumbingWraith(int fortify)
        {
            Fortify = fortify;
        }
    }
}
