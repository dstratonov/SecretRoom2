using System.Collections.Generic;
using Game.Battle.Models;
using UnityEngine;

namespace Game.Battle
{
    public class BattleField : MonoBehaviour
    {
        [SerializeField] private Transform _fieldCenter;
        [SerializeField] private float _distanceBetweenTeams;
        [SerializeField] private float _distanceBetweenCharacters;

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
            return _fieldCenter.position + inEnemyDirection * (_distanceBetweenTeams * 0.5f);
        }

        private Vector3 GetPlayerInitialPosition()
        {
            Vector3 inEnemyDirection = _fieldCenter.forward;
            return _fieldCenter.position + inEnemyDirection * (-_distanceBetweenTeams / 2.0f);
        }

        private void UpdateTeamTransform(TeamModel team, Vector3 startPosition, Quaternion rot)
        {
            if (team.GetCharactersCount() == 0)
            {
                return;
            }

            IReadOnlyList<BattleUnitModel> units = team.GetUnits();

            bool isOdd = (units.Count % 2) == 1;

            if (isOdd)
            {
                units[0].Unit.SetPosition(startPosition);
                for (var i = 1; i < units.Count; i++)
                {
                    int direction = i % 2 == 0 ? 1 : -1;
                    Vector3 currentPosition = startPosition + _fieldCenter.right * direction * _distanceBetweenCharacters *
                        Mathf.Floor((i + 1) * 0.5f);
                    units[i].Unit.SetPosition(currentPosition);
                }
            }
            else
            {
                units[0].Unit.SetPosition(startPosition + _fieldCenter.right * _distanceBetweenCharacters * 0.5f);
                units[1].Unit.SetPosition(startPosition + -_fieldCenter.right * _distanceBetweenCharacters * 0.5f);
                for (var i = 2; i < units.Count; i++)
                {
                    int direction = i % 2 == 0 ? 1 : -1;
                    Vector3 currentPosition = startPosition + _fieldCenter.right * direction * _distanceBetweenCharacters *
                        Mathf.Floor(i * 0.5f) + _fieldCenter.right * direction * _distanceBetweenCharacters * 0.5f;
                    units[i].Unit.SetPosition(currentPosition);
                }
            }

            for (var i = 0; i < units.Count; i++)
            {
                units[i].Unit.SetRotation(rot);
            }
        }
    }
}