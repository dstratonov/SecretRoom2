using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Stats
{
    Health,
    Armor,
}

public class Status : MonoBehaviour
{
    private bool isFree = true;
    public Dictionary<Stats, Stat> statsDictionary = new Dictionary<Stats, Stat>();


    public bool SetLock()
    {
        if (isFree)
        {
            isFree = false;
            return true;
        }

        return false;
    }

    public bool IsFree()
    {
        return isFree;
    }

    public void Release()
    {
        if (!isFree)
        {
            isFree = true;
        }
    }
    void Awake()
    {
        statsDictionary[Stats.Health] = new Stat(100.0f);
        statsDictionary[Stats.Armor] = new Stat(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
