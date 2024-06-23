using System.Collections.Generic;
using System.Linq;
using Common.Reactive;
using Game.Battle.Configs;
using Game.Battle.Factories;
using Game.Battle.Models;
using Game.Battle.Stats;
using Game.Battle.Units;
using Game.Models;
using UnityEngine;

namespace Game.Battle
{
    public class BattleBuilder
    {
        private readonly BattleField _battleField;
        private readonly UnitFactory _unitFactory;
        
        //todo rn it's for debug purpose
        private readonly GameModel _gameModel;

        public BattleBuilder(BattleField battleField, UnitFactory unitFactory, GameModel gameModel)
        {
            _battleField = battleField;
            _unitFactory = unitFactory;
            _gameModel = gameModel;
        }

        public BattleModel Build(BattleConfig battleConfig) =>
            new(
                CreatePlayerTeam(_gameModel.player, battleConfig.playerUnits),
                CreateEnemyTeam(battleConfig.enemyUnits));
        
        private TeamModel CreatePlayerTeam(PlayerModel playerModel, IReadOnlyList<UnitConfig> configs)
        {
            const Team team = Team.Player;
            var teamModel = new TeamModel(team);
            Dictionary<Stat, ReactiveValue> playerStats 
                = playerModel.playerStats.ToDictionary(x => x.stat, x => new ReactiveValue(x.value));
            
            BattleUnitModel playerUnit = _unitFactory.Create(
                "Player",
                playerModel.playerView,
                playerStats,
                playerModel.abilities.ToArray());
            
            playerUnit.SetTeam(team);
            teamModel.AddUnit(playerUnit);

            foreach (UnitConfig unitConfig in configs)
            {
                var stats = new Dictionary<Stat, ReactiveValue>();

                foreach (StatModel statModel in unitConfig.battleData.statMultipliers)
                {
                    stats.Add(
                        statModel.stat,
                        new ReactiveValue(Mathf.Floor(statModel.value * playerStats[statModel.stat].Max)));
                }

                if (!playerStats.TryGetValue(Stat.EN, out ReactiveValue energy))
                {
                    energy = new ReactiveValue();
                    playerStats.Add(Stat.EN, energy);
                }

                StatModel energyStat = unitConfig.battleData.rawStats.FirstOrDefault(x => x.stat == Stat.EN);

                if (energyStat != null)
                {
                    energy.AddMax(energyStat.value, false);
                }
                
                stats.Add(Stat.EN, energy);
                
                StatModel energyRegenStat = unitConfig.battleData.rawStats.FirstOrDefault(x => x.stat == Stat.ENR);

                if (energyRegenStat != null)
                {
                    stats.Add(Stat.ENR, new ReactiveValue(energyRegenStat.value));
                }
                
                BattleUnitModel unit = _unitFactory.Create(
                    unitConfig.id,
                    unitConfig.viewData,
                    stats,
                    unitConfig.battleData.abilities);
                
                unit.SetTeam(team);
                teamModel.AddUnit(unit);
            }
            
            return teamModel;
        }

        private TeamModel CreateEnemyTeam(IEnumerable<UnitConfig> configs)
        {
            var teamModel = new TeamModel(Team.Enemy);

            foreach (UnitConfig unitConfig in configs)
            {
                teamModel.AddUnit(_unitFactory.Create(unitConfig));
            }

            _battleField.SetTeam(teamModel);

            return teamModel;
        }
    }
}