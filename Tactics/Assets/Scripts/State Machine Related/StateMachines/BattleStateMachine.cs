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
    public void nextUnit()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % characters.Count;
        characters[currentPlayerIndex].canMove = true;
        characters[currentPlayerIndex].canAct = true;
        ui.updateBars();
    }

    private void Awake() {
        characters = Object.FindObjectsOfType<Character>().ToList();
    }
    private void Start() {
        setState(new ExploringState(this));
    }

}
