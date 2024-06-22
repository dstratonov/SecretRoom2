using Cinemachine;
using UnityEngine;

namespace Game.Battle.Units
{
    public class UnitPawn : MonoBehaviour
    {
        readonly public CinemachineVirtualCamera playerUnitCamera;
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Quaternion quat)
        {
            transform.rotation = quat;
        }
    }
}