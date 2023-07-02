﻿using BarbarianSim.Config;

namespace BarbarianSim.Aspects
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