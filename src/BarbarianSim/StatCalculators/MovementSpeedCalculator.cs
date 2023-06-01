namespace HunterSim
{
    public class MovementSpeedCalculator : BaseStatCalculator
    {
        public static double Calculate(SimulationState state) => Calculate<MovementSpeedCalculator>(state);

        protected override double InstanceCalculate(SimulationState state)
        {
            var movementSpeed = 1.0;

            if (state.Auras.Contains(Aura.AspectOfTheCheetah) || state.Auras.Contains(Aura.AspectOfThePack))
            {
                movementSpeed += 0.3;

                if (state.Config.Talents.ContainsKey(Talent.Pathfinding))
                {
                    movementSpeed += 0.04 * state.Config.Talents[Talent.Pathfinding];
                }
            }

            return movementSpeed;
        }
    }
}
