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
    private UnitPawn _casterPawn;
    public event Action AnimationFinished;
    public AnimationExecutor(AbilityInvokeArgs abilityInvokeArgs)
    {
        _abilityInvokeArgs = abilityInvokeArgs;
        _animationTriggerKey = _abilityInvokeArgs.ability._data.animTrigger;
        _casterPawn = _abilityInvokeArgs.caster.GetSystem<PawnSystem>().Pawn;
        _animator = _casterPawn.animator;
        _abilityInvokeArgs.caster.GetSystem<PawnSystem>().Pawn.onAnimationFinished += OnAnimationFinished;
    }
    // Start is called before the first frame update
    public void PlayAnimation()
    {
        var targetTransform = _abilityInvokeArgs.target.GetSystem<PawnSystem>().Pawn.transform;
        _casterPawn.GoToTarget(targetTransform, _animationTriggerKey);
        _animator.SetTrigger(_animationTriggerKey);
    }

    void OnAnimationFinished()
    {
        _casterPawn.ResetToDefaultTransform();
        AnimationFinished?.Invoke();
        _casterPawn.onAnimationFinished -= OnAnimationFinished;
    }
}
