using UnityEngine;

namespace Game.Units
{
    [CreateAssetMenu(fileName = nameof(UnitConfig), menuName = "Configs/Game/Unit")]
    public class UnitConfig : ScriptableObject
    {
        [UnitId]
        public string id;

        public UnitViewData viewData;
        public UnitBattleData battleData;
    }
}