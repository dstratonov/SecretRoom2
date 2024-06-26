using System.Collections.Generic;
using Cinemachine;
using Game.Battle.Models;
using Game.Battle.Units;
using Game.Battle.Units.Systems.Pawn;
using UnityEngine;

namespace Game.Battle
{
    public class BattleField : MonoBehaviour
    {
        [SerializeField] private Transform _fieldCenter;
        [SerializeField] private float _distanceBetweenTeams;
        [SerializeField] private float _distanceBetweenCharacters;
        [SerializeField] private CinemachineVirtualCamera _targetCamera;
        [SerializeField] private CinemachineTargetGroup _targetGroup;

        public CinemachineVirtualCamera TargetCamera => _targetCamera;
        public CinemachineTargetGroup TargetGroup => _targetGroup;

        public void SetTeam(TeamModel teamModel)
        {
            Vector3 initialPosition = teamModel.Team == Team.Player
                ? GetPlayerInitialPosition()
                : GetEnemyInitialPosition();

            Quaternion unitsRotation = teamModel.Team == Team.Player
                ? Quaternion.LookRotation(_fieldCenter.forward, _fieldCenter.up)
                : Quaternion.LookRotation(-_fieldCenter.forward, _fieldCenter.up);

            UpdateTeamTransform(teamModel, initialPosition, unitsRotation);
        }

        private Vector3 GetEnemyInitialPosition()
        {
            Vector3 inEnemyDirection = _fieldCenter.forward;
            print(inEnemyDirection + "[LOG]");
            return _fieldCenter.position + inEnemyDirection * (_distanceBetweenTeams * 0.5f);
        }

        private Vector3 GetPlayerInitialPosition()
        {
            Vector3 inEnemyDirection = _fieldCenter.forward;
            return _fieldCenter.position - inEnemyDirection * (_distanceBetweenTeams / 2.0f);
        }

        private void UpdateTeamTransform(TeamModel team, Vector3 startPosition, Quaternion rot)
        {
            if (team.GetCharactersCount() == 0)
            {
                return;
            }

            IReadOnlyList<BattleUnitModel> units = team.GetUnits();

            bool isOdd = units.Count % 2 == 1;

            if (isOdd)
            {
                units[0].GetSystem<PawnSystem>().Pawn.SetDefaultPosition(startPosition);
                for (var i = 1; i < units.Count; i++)
                {
                    int direction = i % 2 == 0 ? 1 : -1;
                    Vector3 currentPosition = startPosition + _fieldCenter.right * direction *
                        _distanceBetweenCharacters *
                        Mathf.Floor((i + 1) * 0.5f);
                    units[i].GetSystem<PawnSystem>().Pawn.SetDefaultPosition(currentPosition);
                }
            }
            else
            {
                units[0].GetSystem<PawnSystem>().Pawn
                    .SetDefaultPosition(startPosition + _fieldCenter.right * _distanceBetweenCharacters * 0.5f);
                units[1].GetSystem<PawnSystem>().Pawn
                    .SetDefaultPosition(startPosition + -_fieldCenter.right * _distanceBetweenCharacters * 0.5f);
                
                for (var i = 2; i < units.Count; i++)
                {
                    int direction = i % 2 == 0 ? 1 : -1;
                    Vector3 currentPosition = startPosition + _fieldCenter.right * direction *
                        _distanceBetweenCharacters *
                        Mathf.Floor(i * 0.5f) + _fieldCenter.right * direction * _distanceBetweenCharacters * 0.5f;
                    units[i].GetSystem<PawnSystem>().Pawn.SetDefaultPosition(currentPosition);
                }
            }

            foreach (BattleUnitModel unit in units)
            {
                unit.GetSystem<PawnSystem>().Pawn.SetDefaultRotation(rot);
            }
        }
    }
}