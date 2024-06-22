using Cinemachine;
using UnityEngine;

namespace Game.Battle.Units.Systems.Pawn
{
    public class UnitPawn : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _playerUnitCamera;
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Quaternion quat)
        {
            transform.rotation = quat;
        }

        public CinemachineVirtualCamera VirtualCamera => _playerUnitCamera;
    }
}