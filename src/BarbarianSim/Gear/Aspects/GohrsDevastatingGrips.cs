using BarbarianSim.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.Gear.Aspects
{
    public class GohrsDevastatingGrips : Aspect
    {
        public int Damage { get; init; }

        public GohrsDevastatingGrips(int damage)
        {
            Damage = damage;
        }
    }
}
