using System;
using System.Collections;
using System.Collections.Generic;
using Game.Battle.Abilities;
using Game.Battle.Units.Systems.Pawn;
using UnityEngine;

public class AnimationExecutor
{
    private AbilityInvokeArgs _abilityInvokeArgs;
    private string _animationTriggerKey;
    private Animator _animator;
    public event Action AnimationFinished;
    public AnimationExecutor(AbilityInvokeArgs abilityInvokeArgs)
    {
        _abilityInvokeArgs = abilityInvokeArgs;
        _animationTriggerKey = _abilityInvokeArgs.ability._data.animTrigger;
        _animator = _abilityInvokeArgs.caster.GetSystem<PawnSystem>().Pawn.animator;
        _abilityInvokeArgs.caster.GetSystem<PawnSystem>().Pawn.onAnimationFinished += OnAnimationFinished;
    }
    // Start is called before the first frame update
    public void PlayAnimation()
    {
        _animator.SetTrigger(_animationTriggerKey);
    }

    void OnAnimationFinished()
    {
        AnimationFinished?.Invoke();
        _abilityInvokeArgs.caster.GetSystem<PawnSystem>().Pawn.onAnimationFinished -= OnAnimationFinished;
    }
}
