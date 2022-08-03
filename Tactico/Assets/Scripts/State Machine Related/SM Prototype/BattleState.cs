using System.Collections.Generic;

public abstract class BattleState : State
{
    protected BattleStateMachine stateMachine;
    protected List<Character> characters;
    protected Board board;
    protected UIController ui;
    protected Character unit;
    public BattleState(BattleStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.characters = stateMachine.characters;
        this.board = stateMachine.board;
        this.ui = stateMachine.ui;
        this.unit = stateMachine.characters[stateMachine.currentPlayerIndex];
    }
}