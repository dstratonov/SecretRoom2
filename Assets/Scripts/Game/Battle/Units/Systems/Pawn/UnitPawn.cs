using System;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Battle.Units.Systems.Pawn
{
    public class UnitPawn : SerializedMonoBehaviour
    {
        public Animator animator;

        public Dictionary<string, Vector3> posOffsets = new()
        {
            { "attack0", new Vector3(0.0f, 0.0f, 2.0f) },
            { "attack1", new Vector3(0.0f, 0.0f, 2.0f) },
        };
        
        [SerializeField] private CinemachineVirtualCamera _playerUnitCamera;

        [SerializeField] private GameObject _selectionBox;

        private Vector3 _defaultPosition;
        private Quaternion _defaultRotation;

        public event Action OnAnimationFinished;

        public CinemachineVirtualCamera VirtualCamera => _playerUnitCamera;

        public void AnimationFinished()
        {
            OnAnimationFinished?.Invoke();
        }

        public void Deselect()
        {
            _selectionBox.SetActive(false);
        }

        public void GoToTarget(Transform targetTransform, string animationKey)
        {
            transform.position = BuildPosition(targetTransform, animationKey);
            transform.rotation = BuildRotation(targetTransform);
        }

        public void ResetToDefaultTransform()
        {
            transform.position = _defaultPosition;
            transform.rotation = _defaultRotation;
        }

        public void Select()
        {
            _selectionBox.SetActive(true);
        }

        public void SetDefaultPosition(Vector3 position)
        {
            _defaultPosition = position;
            transform.position = _defaultPosition;
        }

        public void SetDefaultRotation(Quaternion quat)
        {
            _defaultRotation = quat;
            transform.rotation = _defaultRotation;
        }

        private Vector3 BuildPosition(Transform targetTransform, string animationKey)
        {
            Vector3 animationOffset = posOffsets[animationKey];
            Vector3 upVector = targetTransform.up;
            Vector3 forwardVector = targetTransform.forward;
            Vector3 rightVector = targetTransform.right;

            return targetTransform.position + rightVector * animationOffset.x + upVector * animationOffset.y +
                   forwardVector * animationOffset.z;
        }

        private Quaternion BuildRotation(Transform targetTransform)
        {
            Vector3 upVector = targetTransform.up;
            Vector3 forwardVector = -targetTransform.forward;
            return Quaternion.LookRotation(forwardVector, upVector);
        }
    }
}