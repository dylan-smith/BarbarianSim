using BarbarianSim.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.Gear.Aspects
{
    public class EdgemastersAspect : Aspect
    {
        public int Damage { get; init; }

        public EdgemastersAspect(int damage)
        {
            Damage = damage;
        }
    }
}
