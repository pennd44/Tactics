using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;


public class BattleStateMachine : StateMachine
{
    public List<Character> characters = new List<Character>();
    public List<Character> enemies = new List<Character>();
    public int currentPlayerIndex = 0;
    public UIController ui;
    public Board board;
    public GameObject cameraController;
    public Material moveSelect;
    public Material moveHover;
    public Material actionSelect;
    public Material actionHover;
    public VictoryCondition victoryCondition;
    private void IncrementCurrentPlayerIndex()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % characters.Count;
        if(characters[currentPlayerIndex].isDead)
        {
            IncrementCurrentPlayerIndex();
        }        
    }
    public void nextUnit()
    {
        characters[currentPlayerIndex].OnTurnEnd();
        // Debug.Log("on Next Unit index: " + currentPlayerIndex);
        // Debug.Log("on Next Unit count: " + characters.Count);
        IncrementCurrentPlayerIndex();
        // Debug.Log("on Next Unit index after: " + currentPlayerIndex);
        // Debug.Log("on Next Unit count: " + characters.Count);
        // Debug.Log(currentPlayerIndex);
        StartCoroutine(MoveCamera(characters[currentPlayerIndex].transform.position));
        characters[currentPlayerIndex].canMove = true;
        characters[currentPlayerIndex].canAct = true;
        ui.updateBars();
        characters[currentPlayerIndex].OnTurnStart();
    }
    public IEnumerator MoveCamera(Vector3 targetPosition)
    {
        while(cameraController.transform.position != targetPosition)
        {
            cameraController.transform.position = Vector3.MoveTowards(cameraController.transform.position, targetPosition, 30*Time.deltaTime);
            yield return null;
        }
    }

    public void OnUnitDeath(Character unit){
        // unit.Die();
        // Debug.Log("on Unit Death index: " + currentPlayerIndex);
        // Debug.Log("on Unit Death count: " + characters.Count);
        // // characters.Remove(unit);
        // Debug.Log("on Unit Death index after: " + currentPlayerIndex);
        // Debug.Log("on Unit Death count after: " + characters.Count);
    }
    // public void hoverCursor(Tile prevTile){
   
    //     RaycastHit hit;
    //     Tile hitTile;
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     if (Physics.Raycast(ray, out hit))
    //     {
    //         GameObject hitObj = hit.transform.gameObject;
    //         if (hitObj.tag == "Tile" && hitObj.GetComponent<Tile>().selectable)
    //         {
    //             hitTile = hitObj.GetComponent<Tile>();
    //             if (prevTile != null)
    //             {
    //                 prevTile.selected = false;
    //                 prevTile.changeHighlight(actionSelect);
    //             }
    //             prevTile = hitTile;
    //             hitTile.selected = true;
    //             hitTile.changeHighlight(actionHover);
    //             handleClick(hitTile);
    //         }
    //     }
    // }

    private void Awake() {
        characters = Object.FindObjectsOfType<Character>().ToList();
        //come back and delete this if not necessary
        foreach (Character character in characters)
        {
            if(character.alliance.type == Alliances.Enemy)
            {enemies.Add(character);}
        }
    }
    private void Start() {
        setState(new ExploringState(this));
    }

}
