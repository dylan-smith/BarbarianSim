namespace BarbarianSim
{
    public static class SimulationWarnings
    {
        public const string TooManyTalentPoints = "You have more than the max talent points for your level";
        public const string MissingTalentPoints = "You have not used all the talent points available for your level";
        public const string MissingAspect = "One or more pieces of gear are missing an aspect";
        public const string PlayerNotMaxLevel = "The player level is below max level, this simulator likely will not produce correct results";
        public const string MoreSocketsAvailable = "One or more pieces of gear have missing sockets or gems";
        public const string HigherLevelGemsAvailable = "One or more gems are not the highest level available";
        public const string ExpertiseMissing = "One or more weapons are missing an expertise category";
    }
}
