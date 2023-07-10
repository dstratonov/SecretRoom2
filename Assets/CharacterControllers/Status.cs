using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    private bool isFree = true;


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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
