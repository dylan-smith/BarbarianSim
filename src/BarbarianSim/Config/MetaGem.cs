namespace BarbarianSim
{
    public abstract class MetaGem : GearItem
    {
        public MetaGem()
        {
            Color = GemColor.Meta;
        }

        public abstract void Apply(SimulationState state);
    }
}
