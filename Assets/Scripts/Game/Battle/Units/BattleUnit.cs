using UnityEngine;

namespace Game.Battle.Units
{
    public class BattleUnit : MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}