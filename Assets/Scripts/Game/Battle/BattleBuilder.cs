using System.Collections.Generic;
using System.Linq;
using Common.Reactive;
using Game.Battle.Configs;
using Game.Battle.Factories;
using Game.Battle.Models;
using Game.Battle.Stats;
using Game.Battle.Units;
using Game.Models;
using Game.Units;
using UnityEngine;

namespace Game.Battle
{
    public class BattleBuilder
    {
        private readonly BattleField _battleField;

        //todo rn it's for debug purpose
        private readonly GameModel _gameModel;
        private readonly UnitFactory _unitFactory;

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

        private BattleUnitModel CreateAllyUnit(UnitConfig unitConfig, IDictionary<Stat, ReactiveValue> playerStats)
        {
            var stats = new Dictionary<Stat, ReactiveValue>();

            foreach (StatModel statModel in unitConfig.battleData.statMultipliers)
            {
                var value = new ReactiveValue(Mathf.FloorToInt(statModel.value * playerStats[statModel.stat].Max));
                value.Set(value.Max);
                
                stats.Add(statModel.stat, value);
            }

            if (!playerStats.TryGetValue(Stat.EN, out ReactiveValue energy))
            {
                energy = new ReactiveValue();
                playerStats.Add(Stat.EN, energy);
            }

            StatModel energyStat = unitConfig.battleData.rawStats.FirstOrDefault(x => x.stat == Stat.EN);

            if (energyStat != null)
            {
                energy.AddMax(Mathf.FloorToInt(energyStat.value));
            }

            stats.Add(Stat.EN, energy);

            StatModel energyRegenStat = unitConfig.battleData.rawStats.FirstOrDefault(x => x.stat == Stat.ENR);

            if (energyRegenStat != null)
            {
                var value = new ReactiveValue(Mathf.FloorToInt(energyRegenStat.value));
                value.Set(value.Max);
                
                stats.Add(Stat.ENR, value);
            }

            BattleUnitModel unit = _unitFactory.Create(
                unitConfig.id,
                unitConfig.viewData,
                stats,
                unitConfig.battleData.abilities);

            return unit;
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

        private TeamModel CreatePlayerTeam(PlayerModel playerModel, IReadOnlyList<UnitConfig> configs)
        {
            const Team team = Team.Player;
            var teamModel = new TeamModel(team);

            Dictionary<Stat, ReactiveValue> playerStats
                = playerModel.playerStats
                    .ToDictionary(x => x.stat, x =>
                    {
                        var value = new ReactiveValue(Mathf.FloorToInt(x.value));
                        value.Set(value.Max);
                        return value;
                    });

            BattleUnitModel playerUnit = CreatePlayerUnit(playerModel, team, playerStats);

            teamModel.AddUnit(playerUnit);

            foreach (UnitConfig unitConfig in configs)
            {
                BattleUnitModel unit = CreateAllyUnit(unitConfig, playerStats);

                unit.SetTeam(team);
                teamModel.AddUnit(unit);
            }

            _battleField.SetTeam(teamModel);

            return teamModel;
        }

        private BattleUnitModel CreatePlayerUnit(PlayerModel playerModel, Team team,
            Dictionary<Stat, ReactiveValue> stats)
        {
            BattleUnitModel playerUnit = _unitFactory.Create(
                "Player",
                playerModel.playerView,
                stats,
                playerModel.abilities.ToArray());

            playerUnit.SetTeam(team);

            return playerUnit;
        }
    }
}