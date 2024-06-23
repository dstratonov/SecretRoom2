using System;
using System.Collections.Generic;
using Game.Units;

namespace Game.Models
{
    [Serializable]
    public class PlayerModel
    {
        public UnitViewData playerView;
        public List<StatModel> playerStats = new();
        public List<string> abilities = new();
    }
}