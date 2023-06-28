using System.Linq;

namespace BarbarianSim.MetaGems
{
    public class PotentUnstableDiamond : MetaGem
    {
        public PotentUnstableDiamond() : base()
        {
            Name = "Potent Unstable Diamond";
            Wowhead = 32640;
            Phase = 1;
            Source = GearSource.Vendor;
        }

        public override void Apply(SimulationState state)
        {
            var orange = state.Config.Gear.GetAllGems().Count(g => g.Color == GemColor.Orange);
            var purple = state.Config.Gear.GetAllGems().Count(g => g.Color == GemColor.Purple);
            var green = state.Config.Gear.GetAllGems().Count(g => g.Color == GemColor.Green);
            var red = state.Config.Gear.GetAllGems().Count(g => g.Color == GemColor.Red);
            var blue = state.Config.Gear.GetAllGems().Count(g => g.Color == GemColor.Blue);
            var yellow = state.Config.Gear.GetAllGems().Count(g => g.Color == GemColor.Yellow);

            red += orange + purple;
            yellow += orange + green;
            blue += purple + green;

            if (yellow >= blue)
            {
                state.Warnings.RemoveAll(x => x == SimulationWarnings.MetaGemInactive);
                state.Warnings.Add(SimulationWarnings.MetaGemInactive);
                return;
            }

            this.AttackPower = 24;
        }
    }
}
