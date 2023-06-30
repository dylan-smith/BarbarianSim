using BarbarianSim.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianSim.Gear.Aspects
{
    public class AspectOfEchoingFury : Aspect
    {
        public int Fury { get; init; }

        public AspectOfEchoingFury(int fury)
        {
            Fury = fury;
        }
    }
}
