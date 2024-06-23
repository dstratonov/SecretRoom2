using UnityEngine;

namespace Game.Battle.Configs
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