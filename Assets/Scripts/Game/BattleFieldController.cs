using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldController : MonoBehaviour
{
    public Transform fieldCenter;
    public float distanceBetweenTeams;
    public float distanceBetweenCharacters;
    private Team enemyTeam;
    private Team playerTeam;

    private Vector3 playerFirstPosition;
    private Vector3 enemyFirstPosition;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 inEnemyDirection = fieldCeneter.forward;
        enemyFirstPosition = fieldCenter + inEnemyDirection * (distanceBetweenTeams / 2.0);
        playerFirstPosition = fieldCenter + inEnemyDirection * (-distanceBetweenTeams / 2.0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private updateCharacterPositions(Team team, startPosition)
    {
        if (team.getCharactersCount() == 0) return;
        

        isLeft = true;
    }

    public void setEnemyTeam(Team team)
    {
        enemyTeam = team;
    }

    public void setPlayerTeam(Team team)
    {
        playerTeam = team;
    }

    const Team getEnemyTeam()
    {
        return enemyTeam;
    }

    const Team getPlayerTeam()
    {
        return playerTeam;
    }
}
