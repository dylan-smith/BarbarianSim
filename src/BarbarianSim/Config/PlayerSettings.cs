using BarbarianSim.Enums;

namespace BarbarianSim.Config;

public class PlayerSettings
{
    public int Level { get; set; }
    public Expertise ExpertiseTechnique { get; set; }

    // https://gamerant.com/diablo-4-stats-explained/

    public double Strength => 10;

    public double Intelligence => 7;

    public double Willpower => 7;

    public double Dexterity => 8;

    public int Life => 40;
}
