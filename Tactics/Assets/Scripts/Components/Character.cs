using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
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
        transform.localEulerAngles = dir.ToEuler();
    }
}
