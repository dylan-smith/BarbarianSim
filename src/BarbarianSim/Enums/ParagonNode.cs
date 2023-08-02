namespace BarbarianSim.Enums;

public enum ParagonNode
{
    None,

    // Legendary Nodes
    Warbringer, // For every 75 Fury you spend, gain 12% of your Maximum Life (12% x [HP]) as Fortify.

    // Glyphs
    Exploit, // When an enemy is damaged by you, they become Vulnerable for 3 seconds. This cannot happen more than once every 20 seconds per enemy.
    Marshal, // After casting a Shout Skill, the active Cooldown of every other Shout Skill is reduced by 1.2 seconds;
    Undaunted, // You gain up to 10% Damage Reduction the more Fortify you have.
    Wrath, // Skills that Critically Strike generate 3 Fury.
}
