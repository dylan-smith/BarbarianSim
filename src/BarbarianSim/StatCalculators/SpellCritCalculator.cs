namespace HunterSim
{
    public class SpellCritCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<SpellCritCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            // Hunters don't use spell crit, even abilities considered spells like Arcane Shot use ranged crit
            // https://classic.wowhead.com/guide=10453/classic-spell-power-hunter-the-little-arcane-shot-that-could
            var spellCrit = 0.0;

            if (state.Config.Talents.ContainsKey(Talent.KillerInstinct))
            {
                spellCrit += state.Config.Talents[Talent.KillerInstinct] * 0.01;
            }

            return spellCrit;
        }
    }
}
