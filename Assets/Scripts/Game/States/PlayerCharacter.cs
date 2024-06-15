using System;
using UnityEngine;
using Zenject;

namespace Game.States
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        [Inject] private IInstantiator _instantiator;

        private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new StateMachine(_instantiator, _animator);
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