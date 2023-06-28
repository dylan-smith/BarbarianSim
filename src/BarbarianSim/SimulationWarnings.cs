namespace BarbarianSim
{
    public static class SimulationWarnings
    {
        public const string TooManyFoodBuffs = "You can only have one food buff active at a time";
        public const string TooManyTalentPoints = "You have more than the max talent points for your level";
        public const string MissingTalentPoints = "You have not used all the talent points available for your level";
        public const string MissingGear = "One or more gear slots have no item selected";
        public const string PlayerNotMaxLevel = "The player level is below max level, this simulator likely will not produce correct results";
        public const string BattleShoutAndEnhancedBattleShoutDoNotStack = "Battle Shout and Enhanced Battle Shout do not stack, you should pick one or the other";
        public const string MetaGemInactive = "Meta gem is not activated";
        public const string TooManyMetaGems = "More than one meta gem equipped";
        public const string CantPutMetaGemInNonMetaSocket = "There is a meta gem in a non-meta socket";
        public const string CantPutNonMetaGemInMetaSocket = "There is a non-meta gem in a meta socket";
        // TODO: Check only battle/guardian elixir or flask active
        // TODO: Can't have Battle Shout and Improved Battle Shout at same time
        // TODO: You ran out of mana during the fight
        // TODO: weapon stone on wrong type of weapon
        // TODO: sniper scope not on a ranged weapon
        // TODO: missing enchants
        // TODO: missing gems
        // TODO: meta not active
        // TODO: missing battle elixir (or flask)
        // TODO: missing food buff
        // TODO: missing guardian elixir (or flask)
        // TODO: missing weapon stones
        // TODO: 2H but not melee weaving
        // TODO: can't have grace of air and improved grace of air at same time
        // TODO: can't have strength of earth and improved soe at same time
        // TODO: can't have blessing of might and improved blessing of might at same time
        // TODO: can't have mark of the wild and improved mark of the wild at same time
        // TODO: multiple unique gems
    }
}
