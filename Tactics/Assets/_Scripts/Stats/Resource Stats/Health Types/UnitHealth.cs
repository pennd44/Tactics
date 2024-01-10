using UnityEngine;

[CreateAssetMenu(fileName = "UnitHealth", menuName = "CharacterComponents/HealthTypes/UnitHealth", order = 0)]
public class UnitHealth : ScriptableObject {
    // bool isDead = false;
    // [SerializeField] BattleStateMachine battleStateMachine;
    // [SerializeField] Character character;
    // //RESEARCH EVENTS AND COME Back
    // public void Die(){
    //     isDead = true;
    //     battleStateMachine.OnUnitDeath(this);
    //     unitAnimator.SetBool("Dead", true);
    //     battleStateMachine.victoryCondition.CheckForGameOver();
    //     if(battleStateMachine.victoryCondition.victor == Alliances.Hero)
    //     {
    //         battleStateMachine.setState(new ExploringState(battleStateMachine));
    //     }
    // }
    // // public int damageAmount;
    // public void GetHit(){
    //     unitAnimator.SetTrigger("hit");
    //     // DamagePopup.Create(transform.position, damageAmount, false);
    // }
}
