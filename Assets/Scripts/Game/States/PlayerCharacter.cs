using System;
using UnityEngine;
using Zenject;

namespace Game.States
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private AnimationTriggers _animationTriggers;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _rigidBody;
        
        [Inject] private IInstantiator _instantiator;

        private StateMachine _stateMachine;

        public Animator Animator => _animator;
        public Rigidbody RigidBody => _rigidBody;
        public LayerMask GroundMask => _groundMask;
        public AnimationTriggers AnimationTriggers => _animationTriggers;
        
        
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

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
    }
}