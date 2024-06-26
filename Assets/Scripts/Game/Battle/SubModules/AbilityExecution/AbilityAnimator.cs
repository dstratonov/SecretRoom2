using System;
using Game.Battle.Units.Systems.Pawn;
using UnityEngine;

namespace Game.Battle.SubModules.AbilityExecution
{
    public class AbilityAnimator
    {
        private readonly string _animationTriggerKey;
        private readonly Animator _animator;
        private readonly UnitPawn _casterPawn;
        private readonly UnitPawn _targetPawn;

        public event Action AnimationFinished;

        public AbilityAnimator(string animTrigger, UnitPawn casterPawn, UnitPawn targetPawn)
        {
            _animationTriggerKey = animTrigger;
            _casterPawn = casterPawn;
            _targetPawn = targetPawn;
            _animator = _casterPawn.animator;
        }

        public void PlayAnimation()
        {
            _casterPawn.OnAnimationFinished += OnAnimationFinished;

            Transform targetTransform = _targetPawn.transform;
            _casterPawn.GoToTarget(targetTransform, _animationTriggerKey);
            _animator.SetTrigger(_animationTriggerKey);
        }

        private void OnAnimationFinished()
        {
            _casterPawn.ResetToDefaultTransform();
            AnimationFinished?.Invoke();
            _casterPawn.OnAnimationFinished -= OnAnimationFinished;
        }
    }
}