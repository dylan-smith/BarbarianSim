namespace HunterSim.GearSets
{
    public class FelstalkerArmor : IGearSet
    {
        public void Apply(SimulationState state)
        {
            var count = state.Config.Gear.GetGearCount(25695, 25697, 25696);

            state.Config.Gear.Other.RemoveAll(x => x.Wowhead == 41749);

            if (count >= 3)
            {
                var bonus = new GearItem();
                bonus.HitRating = 20;
                bonus.Wowhead = 41749;
                bonus.Name = "Felstalker Armor";

                state.Config.Gear.Other.Add(bonus);
            }
        }
    }
}
