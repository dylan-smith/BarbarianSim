using System;

namespace BarbarianSim.Config
{
    public class PlayerSettings
    {
        public int Level { get; set; }
        public Expertise ExpertiseTechnique { get; set; }

        public double Strength
        {
            get
            {
                // TODO: return base value based on level
                return 0;
            }
        }

        public double Intelligence
        {
            get
            {
                // TODO: return base value based on level
                return 0;
            }
        }

        public double Willpower
        {
            get
            {
                // TODO: return base value based on level
                return 0;
            }
        }

        public double Dexterity
        {
            get
            {
                // TODO: return base value based on level
                return 0;
            }
        }

        public double Life
        {
            get
            {
                // TODO: return base value based on level
                return 0;
            }
        }
    }
}