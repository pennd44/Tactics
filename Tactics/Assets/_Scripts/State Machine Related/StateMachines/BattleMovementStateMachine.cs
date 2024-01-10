using System.Collections.Generic;
using UnityEngine;

public class BattleMovementStateMachine : MonoBehaviour
{ 
    //if not inheriting from stateMachine
    public BattleMovement currentMovement;

    public void SetMovement(BattleMovement movement)
    {
        // if (currentMovement != null)
        // {
        //     currentMovement.exit();
        // }
        currentMovement = movement;
        // if (currentMovement != null)
        // {
        //     currentMovement.enter();
        // }
    }
    // end
    public Character unit;
    public Board board;
    public BattleCameraMovement cameraController;

    protected virtual void Awake() {
        unit = GetComponent<Character>();
        board = GameObject.FindObjectOfType<Board>();
        cameraController = GameObject.FindObjectOfType<BattleCameraMovement>();
        // jumper = transform.FindChild("Jumper");
    }
    private void Start() {
        SetMovement(new WalkMovement(this));
    }
    public List<Tile> GetTilesInRange(Board board)
    {
        return currentMovement.GetTilesInRange(board);
    }
}