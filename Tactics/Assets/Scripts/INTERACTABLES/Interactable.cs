using System.Collections.Generic;
using UnityEngine;
public class Interactable : MonoBehaviour
{
    public float radius = 2f;
    public List<Stats> statsDecremented = new List<Stats>();
    public List<Stats> statsIncremented = new List<Stats>();
    public List<int> amountsDecremented = new List<int>();
    public List<int> amountsIncremented = new List<int>();
    public List<Ability> skillsAdded = new List<Ability>();
    public List<Ability> skillsRemoved = new List<Ability>();
    public void ApplyEffects(Character character){
        ApplyStatChanges(character);
        ApplySkillChanges(character);
    }
    public void ApplyStatChanges(Character character){
        for(int i = 0; i < statsDecremented.Count; i++){
            character.findStatbyName(statsDecremented[i]).decrementStat(amountsDecremented[i]);
        }
        for(int i = 0; i < statsIncremented.Count; i++){
            character.findStatbyName(statsIncremented[i]).incrementStat(amountsIncremented[i]);
        }
    }
    public void ApplySkillChanges(Character character){
         for(int i = 0; i < skillsAdded.Count; i++){
            character.skills.Add(skillsAdded[i]);
            character.AquireSkill(skillsAdded[i]);
        }
         for(int i = 0; i < skillsRemoved.Count; i++){
            if(character.skills.Contains(skillsRemoved[i])){
                character.skills.Remove(skillsRemoved[i]);
            }
        }
    }
    public bool CanInteract(Character character){
        for(int i = 0; i < statsDecremented.Count; i++){
            if(character.findStatbyName(statsDecremented[i]).baseValue > amountsDecremented[i]){
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
