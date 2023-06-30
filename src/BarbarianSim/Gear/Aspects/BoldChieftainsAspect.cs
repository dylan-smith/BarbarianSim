using BarbarianSim.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.Gear.Aspects
{
    public class BoldChieftainsAspect : Aspect
    {
        public double Cooldown { get; init; }

        public BoldChieftainsAspect(double cooldown)
        {
            Cooldown = cooldown;
        }
    }
}
