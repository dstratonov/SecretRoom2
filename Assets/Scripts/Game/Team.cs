using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public List<Character> charList = new List<Character>();

    public int getCharactersCount()
    {
        return charList.Count;
    }

}
