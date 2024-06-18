using System.Collections;
using System.Collections.Generic;
using Game.Battle.Models;
using Game.Battle.Configs;
using UnityEngine.UI;
using UnityEngine;

public class IconSetter : MonoBehaviour
{
    public List<Image> enemySlots = new List<Image>();
    public List<Image> playerSlots = new List<Image>();

    public void SetEnemyTeam(List<UnitConfig> enemyUnits)
    {

        for (var i = 0; i < enemyUnits.Count; i++)
        {
            enemySlots[i].sprite = enemyUnits[i].unitIcon;
        }
    }

    public void SetPlayerTeam(List<UnitConfig> playerUnits)
    {
        for (var i = 0; i < playerUnits.Count; i++)
        {
            playerSlots[i].sprite = playerUnits[i].unitIcon;
        }
    }
}
