using System;
using Game.Battle.Units.Systems.Pawn;
using UnityEngine;

namespace Game.Units
{
    [Serializable]
    public class UnitViewData
    {
        public Sprite unitIcon;
        public UnitPawn unitPawn;
    }
}