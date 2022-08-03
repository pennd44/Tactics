using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public BattleStateMachine stateMachine;
    public Character unit;
    public Button Actions;
    public Button Move;
    public Button Wait;
    public Button Attack;
    public Button Skills;
    public Button BackToBM;
    public VisualElement BattleMenu;
    public ScrollView ActionsMenu;
    public ProgressBar HealthBar;
    public ProgressBar ManaBar;
    public ProgressBar StaminaBar;
    void Start()
    {
        // unit = stateMachine.characters[stateMachine.currentPlayerIndex];
        var root = GetComponent<UIDocument>().rootVisualElement;
        Actions = root.Q<Button>("Actions");
        Move = root.Q<Button>("Move");
        Wait = root.Q<Button>("Wait");
        Attack = root.Q<Button>("Attack");
        Skills = root.Q<Button>("Skills");
        BackToBM = root.Q<Button>("BackToBM");
        BattleMenu = root.Q<VisualElement>("BattleMenu");
        ActionsMenu = root.Q<ScrollView>("ActionsMenu");
        HealthBar = root.Q<ProgressBar>("HealthBar");
        ManaBar = root.Q<ProgressBar>("ManaBar");
        StaminaBar = root.Q<ProgressBar>("StaminaBar");

        Actions.clicked += actionsButtonPressed;
        Move.clicked += moveButtonPressed;
        Wait.clicked += waitButtonPressed;
        Attack.clicked += attackButtonPressed;
        Skills.clicked += skillsButtonPressed;
        BackToBM.clicked += backToBMPressed;
        
    }
    public void displayBattleMenu(){
        BattleMenu.style.display = DisplayStyle.Flex;
        if (stateMachine.characters[stateMachine.currentPlayerIndex].canMove){
            Move.style.display = DisplayStyle.Flex;
        }
        else{
            Move.style.display = DisplayStyle.None;
        }
        if (stateMachine.characters[stateMachine.currentPlayerIndex].canAct)
            Actions.style.display = DisplayStyle.Flex;
        else
            Actions.style.display = DisplayStyle.None;
    }
    public void hideBattleMenu(){
        BattleMenu.style.display = DisplayStyle.None;
    }
    public void resetBattleMenu(){
        BattleMenu.style.display = DisplayStyle.Flex;
        ActionsMenu.style.display = DisplayStyle.None;
        BackToBM.style.display = DisplayStyle.None;
    }
    public void actionsButtonPressed(){
        hideBattleMenu();
        ActionsMenu.style.display = DisplayStyle.Flex;
        BackToBM.style.display = DisplayStyle.Flex;
    }
    public void moveButtonPressed(){
       BackToBM.style.display = DisplayStyle.Flex;
        stateMachine.setState(new MoveSelectState(stateMachine));
    }
    public void waitButtonPressed(){
        stateMachine.nextUnit();
        stateMachine.setState(new BattleMenuState(stateMachine));
        resetBattleMenu();
    }
    public void attackButtonPressed(){
        stateMachine.setState(new ActionSelectState(stateMachine));
    }
    public void skillsButtonPressed(){
        Attack.style.display = DisplayStyle.None;
        Skills.style.display = DisplayStyle.None;
                BackToBM.style.display = DisplayStyle.Flex;
        displaySkills();

        //iterate through skills and show buttons for each
    }
    public void backToBMPressed(){
        stateMachine.setState(new BattleMenuState(stateMachine));
    }
    public void updateBars(){
        HealthBar.value = stateMachine.characters[stateMachine.currentPlayerIndex].currentHealth;
        HealthBar.highValue = stateMachine.characters[stateMachine.currentPlayerIndex].maxHealth;
        ManaBar.value = stateMachine.characters[stateMachine.currentPlayerIndex].currentMana;
        ManaBar.highValue = stateMachine.characters[stateMachine.currentPlayerIndex].maxMana;
        StaminaBar.value = stateMachine.characters[stateMachine.currentPlayerIndex].currentStamina;
        StaminaBar.highValue = stateMachine.characters[stateMachine.currentPlayerIndex].maxStamina;
    }
    public void displaySkills(){
        foreach (Ability skill in stateMachine.characters[stateMachine.currentPlayerIndex].skills)
        {
         ActionsMenu.Add( new Button() { text = skill.name});   
        }
    }
}
