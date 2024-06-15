using System;
using UnityEngine;
using Zenject;

namespace Game.States
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidbody;
        
        [Inject] private IInstantiator _instantiator;

        private StateMachine _stateMachine;

        public Animator Animator => _animator;
        public Rigidbody Rigidbody => _rigidbody;
        public LayerMask GroundMask => _groundMask;
        
        public void AnimationFinishTrigger() => _stateMachine.CurrentState.AnimationFinishTrigger();
        
        
        private void Awake()
        {
            _stateMachine = new StateMachine(_instantiator, this);
        }

        private void Start()
        {
            _stateMachine.SetState<IdleState>();
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}