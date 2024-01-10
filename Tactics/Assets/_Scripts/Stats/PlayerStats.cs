using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Health health;
    public Ki ki;
    public Stamina stamina;
    [Header("Stats")]
    [Header("Resources")]
    public int gold = 0;
    public int wood = 0;
    public List<Stat> stats;
    private void Awake() {
        Stat newStat; 
        foreach (var val in Enum.GetValues(typeof(Stats)))
        {
            newStat = new Stat((Stats)val);
            stats.Add(newStat);
        }
    }
    private Stat findStatbyName(Stats statName){
        for(int i = 0; i < stats.Count; i ++)
        {
            if( statName == stats[i].name)
            {
                return stats[i];
            }
        }
        return null;
    }
    //Activities
    // public void startActivity(){
    //     // InvokeRepeating();
    // }
    // public void endActivity(){
    //     CancelInvoke();
    // }
    // public void addStat(Stat stat){
        
    // }
    // // public Stat findStat(string statName){

    // // }
    // public bool MeetRequirements(){
    //     return true;
    // }
}