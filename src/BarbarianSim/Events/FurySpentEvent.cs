﻿using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class FurySpentEvent : EventInfo
{
    public FurySpentEvent(double timestamp, double furySpent, SkillType skillType) : base(timestamp)
    {
        BaseFurySpent = furySpent;
        SkillType = skillType;
    }

    public double BaseFurySpent { get; init; }
    public double FurySpent { get; set; }
    public SkillType SkillType { get; init; }

    public override string ToString() => $"{base.ToString()} - {FurySpent} fury spent";
}
