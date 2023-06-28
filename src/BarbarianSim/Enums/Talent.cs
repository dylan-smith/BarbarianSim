namespace BarbarianSim
{
    public enum Talent
    {
        // Beast Mastery
        ImprovedAspectOfTheHawk, // While Aspect of the Hawk is active, all normal ranged attacks have a 10% chance of increasing ranged attack speed by 3% for 12 sec
        EnduranceTraining, // Increases the health of your pet by 2% and your total health by 1%
        FocusedFire, // All damage caused by you is increased by 1% while your pet is active and the critical strike chance of your Kill command ability is increased by 10%
        ImprovedAspectOfTheMonkey, // Increases the Dodge bonus of your Aspect of the Monkey by 2%
        ThickHide, // Increases the Armor rating of your pets by 7% and your armor contribution from items by 4%
        ImprovedRevivePet, // Revive Pet's casting time is reduced by 3 sec, mana cost is reduced by 20%, and increses the health your pet returns with by an additional 15%
        Pathfinding, // Increases the speed bonus of your Aspect of the Cheetah and Aspect of the Pack by 4%
        BestialSwiftness, // Increases the outdoor movement speed of your pets by 30%
        UnleashedFury, // Increases the damage done by your pets by 4%
        ImprovedMendPet, // Reduces the mana cost of your Mend Pet spell by 10% and gives the Mend Pet spell a 25% chance of cleansing 7 Curse, Disease, Magic or Poison effect from the pet each tick
        Ferocity, // Increases the critical strike chance of your pet by 2%
        SpiritBond, // While your pet is active, you and your pet will regenerate 1% of total health every 10 sec
        Intimidation, // Command your pet to intimidate the target on the next successful melee attack, causing a high amount of threat and stunning the target for 3 sec (1 min CD)
        BestialDiscipline, // Increases the Focus regeneration of your pets by 50%
        AnimalHandler, // Increases your speed while mounted by 4% and your pet's chance to hit by 2%. The mounted movement speed increase does not stack with other effects
        Frenzy, // Gives your pet a 20% chance to gain a 30% attack speed increase for 8 sec after dealing a critical strike
        FerociousInspiration, // When your pet scores a critical hit, all party members have all damage increased by 1% for 10 seconds
        BestialWrath, // Send your pet into a rage causing 50% additional damage for 18 sec. While enraged, the beast does not feel pity or remorse or fear and it cannot be stopped unless killed (2 min CD)
        CatlikeReflexes, // Increases your chance to dodge by 1% and your pet's chance to dodge by an additional 3%
        SerpentsSwiftness, // Increases ranged combat attack speed by 4% and your pet's melee attack speed by 4%
        TheBeastWithin, // When your pet is under the effects of Bestial Wrath, you also go into a rage causing 10% additional damage and reducing mana costs of all spells by 20% for 18 seconds. While enraged, you do not feel pity or remorse or fear and you cannot be stopped unless killed

        // Marksmanship
        ImprovedConcussiveShot, // Gives your Concussive Shot a 4% chance to stun the target for 3 sec (4% proc chance)
        LethalShots, // Increases your critical strike chance with ranged weapons by 1%
        ImprovedHuntersMark, // Causes 20% of your Hunter's Mark ability's base attack power to apply to melee attack power as well
        Efficiency, // Reduces the Mana cost of your Shots and Stings by 2%
        GoForTheThroat, // Your ranged critical hits cause your pet to generate 25 focus
        ImprovedArcaneShot, // Reduces the cooldown of your Arcane Shot by 0.2 sec
        AimedShot, // An aimed shot that increases ranged damage by 70 and reduces healing done to that target by 50%. Lasts 10 sec (6 sec CD)
        RapidKilling, // Reduces the cooldown of your Rapid Fire ability by 1 min. In addition, after killing and opponent that yields experience or honor, your next Aimed Shot, Arcane Shot or Auto Shot causes 10% additional damage. Lasts 20 sec
        ImprovedStings, // Increases the damage done by your Serpent Sting and Wyvern Sting by 6% and the mana drained by your Viper Sting by 6%. In addition, reduces the chance your Stings will be dispelled by 6%
        MortalShots, // Increases your ranged weapon critical strike damage bonus by 6%
        ConcussiveBarrage, // Your successful Auto Shot attacks have a 2% chance to Daze the target for 4 sec (2% proc chance)
        ScatterShot, // A short-range shot that deals 50% weapon damage and disorients the target for 4 sec. Any damage caused will remove the effect. Turns off your attack when used (30 sec CD)
        Barrage, // Increases the damage done by your Multi-Shot and Volley spells by 4%
        CombatExperience, // Increases your total Agility by 1% and your total Intellect by 3%
        RangedWeaponSpecialization, // Increases the damage you deal with ranged weapons by 1%
        CarefulAim, // Increases your ranged attack power by an amount equal to 15% of your total Intellect
        TrueshotAura, // Increases the attack power of party members within 45 yards by 125 (Rank 4). Lasts until cancelled
        ImprovedBarrage, // Increases the critical strike chance of your Multi-Shot ability by 4% and gives you a 33% chance to avoid interruption caused by damage while channeling Volley
        MasterMarksman, // Increases your ranged attack power by 2%
        SilencingShot, // A shot that deals 50% weapon damage and Silences the target for 3 sec (20 sec CD)

        // Survival
        MonsterSlaying, // Increases all damage caused against Beasts, Giants and Dragonkin targets by 1% and increases critical damage caused by an additional 1%
        HumanoidSlaying, // Increases all damaage caused against Humanoid targets by 1% and increases critical damage by an additional 1%
        HawkEye, // Increases the range of your ranged weapons by 2 yards
        SavageStrikes, // Increases the critical strike chance of Raptor Strike and Mongoose Bite by 10%
        Entrapment, // Gives your Immolation Trap, Frost Trap, Explosive Trap, and Snake Trap a 8% chance to entrap the target, preventing them from moving for 4 sec
        Deflection, // Increases your Parry chance by 1%
        ImprovedWingClip, // Gives your Wing Clip ability a 7% chance to immobilize the target for 5 sec
        CleverTraps, // Increases the duration of Freezing and Frost trap effects by 15% and the damage of Immolation and Explosive trap effects by 15%, and the number of snakes summoned from Snake Traps by 15%
        Survivalist, // Increases total health by 2%
        Deterrence, // When activated, increases your Dodge and Parry chance by 25% for 10 sec (5 min CD)
        TrapMastery, // Decreases the chance enemies will resist trap effects by 5%
        Surefooted, // Increases hit chance by 1% and increases the chance movement impairing effects will be resisted by an additional 5%
        ImprovedFeignDeath, // Reduces the chance your Feign Death ability will be resisted by 2%
        SurvivalInstincts, // Reduces all damage taken by 2% and increases attack power by 2%
        KillerInstinct, // Increases your critical strike chance with all attacks by 1%
        Counterattack, // A strike that becomes active after parrying an opponent's attack. This attack deals 40 damage and immobilizes the target for 5 sec. Counterattack cannot be blocked, dodged or parried (5 sec CD)
        Resourcefulness, // Reduces the mana cost of all traps and melee abilities by 20% and reduces the cooldown of all traps by 2 sec
        LightningReflexes, // Increases your agility by 3%
        ThrillOfTheHunt, // Gives you a 33% chance to regain 40% of the mana cost of any shot when it critically hits
        WyvernSting, // A stinging shot that puts the target to sleep for 12 sec. Any damage will cancel the effect. When the target wakes up, the Sting causes 300 Nature damage over 12 sec. Only one Sting per Hunter can be active on the target at a time (2 min CD)
        ExposeWeakness, // Your ranged criticals have a 33% chance to apply an Expose Weakness effect to the target. Expose Weakness increases the attack power of all attackers against that target by 25% of your Agility for 7 sec
        MasterTactician, // Your successful ranged attacks have a 6% chance to increase your critical strike chance with all attacks by 2% for 8 sec
        Readiness // When activated, this ability immediately finishes the cooldown on your other Hunter abilities
    }
}