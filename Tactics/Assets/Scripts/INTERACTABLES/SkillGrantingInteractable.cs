using System.Collections.Generic;
using UnityEngine;
public class SkillGrantingInteractable : Interactable
{
    public List<Ability> skillsAdded = new List<Ability>();
    public List<Ability> skillsRemoved = new List<Ability>();
    public override void ApplyEffects(Character character){
        ApplySkillChanges(character);
    }
    private void ApplySkillChanges(Character character){
         for(int i = 0; i < skillsAdded.Count; i++){
            character.AquireSkill(skillsAdded[i]);
        }
         for(int i = 0; i < skillsRemoved.Count; i++){
            if(character.skills.Contains(skillsRemoved[i])){
                character.skills.Remove(skillsRemoved[i]);
            }
        }
    }
    public override bool CanInteract(Character character){
        return true;
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);    
    }
}