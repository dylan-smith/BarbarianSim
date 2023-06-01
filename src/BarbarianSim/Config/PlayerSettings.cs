using System;

namespace HunterSim
{
    public class PlayerSettings
    {
        public Race Race { get; set; }
        public int Level { get; set; }

        // TODO: Calc stat values modified by level (can't find a good source of info)
        // TODO: Draenai

        public double Strength
        {
            get
            {
                switch (Race)
                {
                    case Race.Dwarf:
                        return 66.0;
                    case Race.NightElf:
                        return 61.0;
                    case Race.Orc:
                        return 67.0;
                    case Race.Tauren:
                        return 69.0;
                    case Race.Troll:
                        return 65.0;
                    case Race.Draenei:
                        return 65.0;
                    case Race.BloodElf:
                        return 61.0;
                    default:
                        // TODO: Richer exceptions
                        throw new Exception("Race not set");
                }
            }
        }

        public double Agility
        {
            get
            {
                switch (Race)
                {
                    case Race.Dwarf:
                        return 147.0;
                    case Race.NightElf:
                        return 156.0;
                    case Race.Orc:
                        return 148.0;
                    case Race.Tauren:
                        return 146.0;
                    case Race.Troll:
                        return 153.0;
                    case Race.Draenei:
                        return 148.0;
                    case Race.BloodElf:
                        return 153.0;
                    default:
                        // TODO: Richer exceptions
                        throw new Exception("Race not set");
                }
            }
        }

        public double Stamina
        {
            get
            {
                switch (Race)
                {
                    case Race.Dwarf:
                        return 111.0;
                    case Race.NightElf:
                        return 107.0;
                    case Race.Orc:
                        return 110.0;
                    case Race.Tauren:
                        return 110.0;
                    case Race.Troll:
                        return 109.0;
                    case Race.Draenei:
                        return 107.0;
                    case Race.BloodElf:
                        return 106.0;
                    default:
                        // TODO: Richer exceptions
                        throw new Exception("Race not set");
                }
            }
        }

        public double Intellect
        {
            get
            {
                switch (Race)
                {
                    case Race.Dwarf:
                        return 76.0;
                    case Race.NightElf:
                        return 77.0;
                    case Race.Orc:
                        return 74.0;
                    case Race.Tauren:
                        return 72.0;
                    case Race.Troll:
                        return 73.0;
                    case Race.Draenei:
                        return 78.0;
                    case Race.BloodElf:
                        return 81.0;
                    default:
                        // TODO: Richer exceptions
                        throw new Exception("Race not set");
                }
            }
        }

        public double Spirit
        {
            get
            {
                switch (Race)
                {
                    case Race.Dwarf:
                        return 82.0;
                    case Race.NightElf:
                        return 83.0;
                    case Race.Orc:
                        return 86.0;
                    case Race.Tauren:
                        return 85.0;
                    case Race.Troll:
                        return 84.0;
                    case Race.Draenei:
                        return 85.0;
                    case Race.BloodElf:
                        return 82.0;
                    default:
                        // TODO: Richer exceptions
                        throw new Exception("Race not set");
                }
            }
        }

        public double Health => 3488.0;

        public double Mana => 3253.0;
    }
}