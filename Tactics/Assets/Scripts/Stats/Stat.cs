using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stat
{
    public Stats name;
    public int baseValue;
    public Stat(Stats name){
        this.name = name;
        this.baseValue = 0;
    }
    public void incrementStat(int amount){
        baseValue += amount;
    }
    public void decrementStat(int amount){
        baseValue -= amount;
    }
   
    // [SerializeField] protected int baseValue;
    // public int GetValue ()
    // {
    //     return baseValue;
    // }
    public string Stringify(){
        return $"{name}: {baseValue}";
    }
}
