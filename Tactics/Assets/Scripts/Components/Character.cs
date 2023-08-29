using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Items;
public class Character : Entity
{
    /// <summary>
    /// how to split up: Battle Movement, Battle Acter?, Abitlity, Equiper, Stats, EntityAnimator,
    /// </summary>
    private BattleStateMachine battleStateMachine;
    public BattleMovementStateMachine mover;
    public AbilityHolder abilityHolder;

    public Alliance alliance;
    public List<Stat> stats;  /// testing
    public EquipmentLoadout equipmentLoadout;

    [SerializeField] public Transform rightHandTransform = null;
    [SerializeField] public Transform leftHandTransform = null;
    private void Awake() {
        alliance = GetComponent<Alliance>();
        Stat newStat; 
        foreach (var val in Enum.GetValues(typeof(Stats)))
        {
            newStat = new Stat((Stats)val);
            stats.Add(newStat);
        }
        battleStateMachine = GameObject.FindObjectOfType<BattleStateMachine>();
        mover = GetComponent<BattleMovementStateMachine>();
        equipmentLoadout = GetComponent<EquipmentLoadout>();
    }
    [SerializeField] public Animator unitAnimator;
    [Header("Exploring Phase Variables")]
    public float interactDistance = 3f;
    [Header("Battle Phase Variables")]
    public bool canMove = true;
    public bool canAct = true;
    public Tile currentTile;
    public Directions dir;
    public int intiative = 0;
    [Header("Stats")]
    [SerializeField] public int range = 4;
    [SerializeField] public int jumpHeight = 4;
    [SerializeField] public int attackRange = 2;
    [SerializeField] public int speed = 4;
    [SerializeField] public int currentHealth = 10;
    [SerializeField] public int maxHealth = 10;
    // [SerializeField] public int currentMana = 10;
    [SerializeField] public int maxMana = 10;
    [SerializeField] public int currentStamina = 10;
    [SerializeField] public int maxStamina = 10;
    [SerializeField] public int attack = 2;
    public bool isDead = false;
    public List<Ability> skills = new List<Ability>();
    public void Place(Tile target){
        if( currentTile != null && currentTile.content == gameObject)
            currentTile.content = null;
            currentTile.occupied = false;
            currentTile = target;
        if(target != null)
            target.content = gameObject;
            target.occupied = true;
    }
    public void snapToTile(){
        transform.position = new Vector3(currentTile.pos.x, currentTile.height, currentTile.pos.y);
        currentTile.occupied = true;
        currentTile.content = this.gameObject;
        SetDir();
        transform.localEulerAngles = dir.ToEuler();
    }
   public void SetDir(){
        var v = transform.forward;
        v.y = 0;
        v.Normalize();     
        if (Vector3.Angle(v, Vector3.forward) <= 45.0)
            dir = Directions.North;
        else if (Vector3.Angle(v, Vector3.right) <= 45.0)
            dir =  Directions.East;
        else if (Vector3.Angle(v, Vector3.back) <= 45.0)
            dir =  Directions.South;
        else
            dir = Directions.West;
    }
    public void Die(){
        Debug.Log(name + "is Dead");
        isDead = true;
        // battleStateMachine.OnUnitDeath(this);
        unitAnimator.SetBool("Dead", true);
        battleStateMachine.victoryCondition.CheckForGameOver();
        if(battleStateMachine.victoryCondition.victor == Alliances.Hero)
        {
            battleStateMachine.setState(new ExploringState(battleStateMachine));
        }
    }
    // public int damageAmount;
    public void GetHit(){
        unitAnimator.SetTrigger("hit");
        // DamagePopup.Create(transform.position, damageAmount, false);
    }
        // turn this to lifecycle event

    /// testing animation target system
    public void UseAbility(){
        abilityHolder.ability.Use(tiles);
    }
    public List<Tile> tiles;
    public void Hit()
    {
        UseAbility();
    }
    // turn this to lifecycle event

    public Tile projectileTarget;
    public void LaunchProjectile(){
        Projectile projectileInstance = Instantiate(abilityHolder.projectile, abilityHolder.ability.GetTransform(rightHandTransform, leftHandTransform).position, Quaternion.identity);
        projectileInstance.SetTarget(projectileTarget);
        projectileInstance.unit = this;
    }
    // public void Hit(Character target){
    //     target.unitAnimator.SetTrigger("hit");
    // }
    public void AquireSkill(Ability skill)
    {
        skills.Add(skill);
        // skill.FindAbilityComponents();
    }
    public Stat findStatbyName(Stats statName){
        for(int i = 0; i < stats.Count; i ++)
        {
            if( statName == stats[i].name)
            {
                return stats[i];
            }
        }
        return null;
    }

    //To be moved later
    //Lifecycle events

    // [SerializeField] Ability testLifeCycleAbility;
    [SerializeField] ActionEffect testEffect;
    
    [SerializeField] GameObject gb;
    private void setTarget(GameObject chara){
        gb = chara;
    }

    public void OnGetHit(){
        // Debug.Log(this.name + "on get hit");
    }
    //found in specific ability effects
    public void OnAttack(){
        // Debug.Log(this.name + "on attack");
    }
    //found in action select state
    public void OnTurnStart(){
        // Debug.Log(this.name + "on turn start");
    }
    //found in Battle State Machine, in increment current player index *NOTE needs to happen when battle starts too.  Will natuarally happen when turn order is determined by speed
    public void OnTurnEnd(){
        // Debug.Log(this.name + "on turn end");
    }
    //found in Battle State Machine, in increment current player index
    public void OnTimer(){}

    // private void ActivateEffect(Ability ability, Action<> situation){}
}
