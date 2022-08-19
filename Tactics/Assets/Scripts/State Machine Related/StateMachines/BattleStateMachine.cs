using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;


public class BattleStateMachine : StateMachine
{
    public List<Character> characters = new List<Character>();
    public int currentPlayerIndex = 0;
    public UIController ui;
    public Board board;
    public GameObject cameraController;
    public Material moveSelect;
    public Material moveHover;
    public Material actionSelect;
    public Material actionHover;
    public void nextUnit()
    {
        Debug.Log(currentPlayerIndex);
        currentPlayerIndex = (currentPlayerIndex + 1) % characters.Count;
        Debug.Log(currentPlayerIndex);
        StartCoroutine(MoveCamera(characters[currentPlayerIndex].transform.position));
        characters[currentPlayerIndex].canMove = true;
        characters[currentPlayerIndex].canAct = true;
        ui.updateBars();
    }
    public IEnumerator MoveCamera(Vector3 targetPosition)
    {
        Debug.Log("start");
        while(cameraController.transform.position != targetPosition)
        {
        Debug.Log("mid");
            cameraController.transform.position = Vector3.MoveTowards(cameraController.transform.position, targetPosition, 30*Time.deltaTime);
            yield return null;
        }
        Debug.Log("done");
    }

    public void OnUnitDeath(Character unit){
        // unit.Die();
        characters.Remove(unit);
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
    }
    private void Start() {
        setState(new ExploringState(this));
    }

}
