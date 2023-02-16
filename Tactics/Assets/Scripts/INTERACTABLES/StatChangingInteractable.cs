using System.Collections.Generic;
using UnityEngine;
public class StatChangingInteractable : Interactable
{
    [SerializeField] List<StatObject> incrementedStats;
    [SerializeField] List<StatObject> decrementedStats;
  
    public override void ApplyEffects(Character character){
        ApplyStatChanges(character);
    }
    public void ApplyStatChanges(Character character){
        for(int i = 0; i< decrementedStats.Count;i++){
            character.findStatbyName(decrementedStats[i].stat).decrementStat(decrementedStats[i].ammount);
        }
        for(int i = 0; i< incrementedStats.Count;i++){
            character.findStatbyName(incrementedStats[i].stat).decrementStat(incrementedStats[i].ammount);
        }
    }
    public override bool CanInteract(Character character){
        for(int i = 0; i< decrementedStats.Count;i++){
            if(character.findStatbyName(decrementedStats[i].stat).baseValue < decrementedStats[i].ammount){
                return false;
            }
        }
        return true;
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);    
    }
}
