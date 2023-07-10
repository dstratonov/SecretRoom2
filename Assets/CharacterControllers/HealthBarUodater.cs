using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUodater : MonoBehaviour
{
    public Image healthBar;
    private Status status;

    private Stat health;
    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<Status>();
        health = status.statsDictionary[Stats.Health];
    }

    // Update is called once per frame
    void Update()
    {
        float healthPercentage = health.CurrentValue / health.BaseValue;
        healthBar.fillAmount = healthPercentage;
    }
}
