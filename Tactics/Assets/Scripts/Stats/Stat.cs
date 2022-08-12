using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] protected int baseValue;
    public int GetValue ()
    {
        return baseValue;
    }
}
