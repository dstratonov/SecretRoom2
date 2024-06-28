using Game.Battle.Units;

namespace Game.Battle.Damage
{
    public struct DamageData
    {
        public BattleUnitModel dealer;
        public BattleUnitModel taker;
        public int damage;
        public DamageType type;
    }
}