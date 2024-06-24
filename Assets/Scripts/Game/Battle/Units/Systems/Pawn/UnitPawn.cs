using System;
using Cinemachine;
using UnityEngine;

namespace Game.Battle.Units.Systems.Pawn
{
    public class UnitPawn : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _playerUnitCamera;

        public event Action onAnimationFinished;

        public Animator animator;
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Quaternion quat)
        {
            transform.rotation = quat;
        }

        public void AnimationFinished()
        {
            onAnimationFinished?.Invoke();
        }

        public CinemachineVirtualCamera VirtualCamera => _playerUnitCamera;
    }
}