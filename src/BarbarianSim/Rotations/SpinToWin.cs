using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Enums;

namespace BarbarianSim.Rotations;

public class SpinToWin : IRotation
{
    public SpinToWin(RallyingCry rallyingCry,
                     ChallengingShout challengingShout,
                     WarCry warCry,
                     WrathOfTheBerserker wrathOfTheBerserker,
                     Whirlwind whirlwind,
                     LungingStrike lungingStrike)
    {
        _rallyingCry = rallyingCry;
        _challengingShout = challengingShout;
        _warCry = warCry;
        _wrathOfTheBerserker = wrathOfTheBerserker;
        _whirlwind = whirlwind;
        _lungingStrike = lungingStrike;
    }

    private readonly RallyingCry _rallyingCry;
    private readonly ChallengingShout _challengingShout;
    private readonly WarCry _warCry;
    private readonly WrathOfTheBerserker _wrathOfTheBerserker;
    private readonly Whirlwind _whirlwind;
    private readonly LungingStrike _lungingStrike;

    public void Execute(SimulationState state)
    {
        if (_rallyingCry.CanUse(state))
        {
            _rallyingCry.Use(state);
        }

        if (_challengingShout.CanUse(state))
        {
            _challengingShout.Use(state);
        }

        if (_warCry.CanUse(state))
        {
            _warCry.Use(state);
        }

        if (_wrathOfTheBerserker.CanUse(state))
        {
            _wrathOfTheBerserker.Use(state);
        }

        if (_whirlwind.CanUse(state))
        {
            _whirlwind.Use(state);
        }
        else
        {
            if (state.Player.Auras.Contains(Aura.Whirlwinding))
            {
                if (state.Config.Gear.HasAspect<GohrsDevastatingGrips>())
                {
                    var gohrsDevastatingGrips = state.Config.Gear.GetAllAspects<GohrsDevastatingGrips>().Single();

                    if (gohrsDevastatingGrips.HitCount >= GohrsDevastatingGrips.MAX_HIT_COUNT)
                    {
                        _whirlwind.StopSpinning(state);
                    }
                }
            }
            else
            {
                if (_lungingStrike.CanUse(state))
                {
                    _lungingStrike.Use(state, state.Enemies.First());
                }
            }
        }
    }
}
