using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Items;
public class Character : MonoBehaviour
{
    private BattleStateMachine battleStateMachine;
    public BattleMovementStateMachine mover;

    public List<Stat> stats;  /// testing
    public EquipmentLoadout equipmentLoadout;


    private void Awake() {
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
    [SerializeField] public int currentMana = 10;
    [SerializeField] public int maxMana = 10;
    [SerializeField] public int currentStamina = 10;
    [SerializeField] public int maxStamina = 10;
    [SerializeField] public int attack = 2;
    public bool isDead = false;
    public List<Ability> skills = new List<Ability>();
    public void Place(Tile target){
        if( currentTile != null && currentTile.content == gameObject)
            currentTile.content = null;
        currentTile = target;
        if(target != null)
            target.content = gameObject;
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
        isDead = true;
        battleStateMachine.OnUnitDeath(this);
        unitAnimator.SetBool("Dead", true);
        battleStateMachine.victoryCondition.CheckForGameOver();
        if(battleStateMachine.victoryCondition.victor == Alliances.Hero)
        {
            battleStateMachine.setState(new ExploringState(battleStateMachine));
        }
    }
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
}
