using System;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Battle.Units.Systems.Pawn
{
    public class UnitPawn : SerializedMonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _playerUnitCamera;

        public event Action onAnimationFinished;

        public Animator animator;

        private Vector3 _defaultPosition;
        private Quaternion _defaultRotation;

        [SerializeField] private GameObject _selectionBox;

        public Dictionary<string, Vector3> posOffsets = new Dictionary<string, Vector3>
        {
            {"attack0" , new Vector3(0.0f, 0.0f, 2.0f)},
            {"attack1" , new Vector3(0.0f, 0.0f, 2.0f)}
        };
        
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

        public void ResetToDefaultTransform()
        {
            transform.position = _defaultPosition;
            transform.rotation = _defaultRotation;
        }

        public void GoToTarget(Transform targetTransform, string animationKey)
        {
            transform.position = BuildPosition(targetTransform, animationKey);
            transform.rotation = BuildRotation(targetTransform);
        }

        public void AnimationFinished()
        {
            onAnimationFinished?.Invoke();
        }

        private Quaternion BuildRotation(Transform targetTransform)
    {
        Vector3 upVector = targetTransform.up;
        Vector3 forwardVector = -targetTransform.forward;
        return Quaternion.LookRotation(forwardVector, upVector);
    }

    public void Select()
    {
        _selectionBox.SetActive(true);
    }
    
    public void Deselect()
    {
         _selectionBox.SetActive(false);
    }

    private Vector3 BuildPosition(Transform targetTransform, string animationKey)
    {
        var animationOffset = posOffsets[animationKey];
        Vector3 upVector = targetTransform.up;
        Vector3 forwardVector = targetTransform.forward;
        Vector3 rightVector = targetTransform.right;

        return targetTransform.position + rightVector * animationOffset.x + upVector * animationOffset.y + forwardVector * animationOffset.z;
    }

        public CinemachineVirtualCamera VirtualCamera => _playerUnitCamera;
    }
}