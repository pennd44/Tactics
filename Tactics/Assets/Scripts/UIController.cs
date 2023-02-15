using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public BattleStateMachine stateMachine;
    public Character unit;
    #region Battle Menu Variables
    public Button Actions;
    public Button Move;
    public Button Wait;
    // public Button Attack;
    // public Button Skills;
    public Button BackToBM;
    public VisualElement BattleMenu;
    public ScrollView ActionsMenu;
    public ProgressBar HealthBar;
    public ProgressBar ManaBar;
    public ProgressBar StaminaBar;
    
    #endregion
    #region Character Menu Variables
    public VisualElement CharacterMenu;
    public Label Name;
    public Label Level;
    public Label Experience;
    public Label HealthStat;
    public Label MaxHealthStat;
    public Label KiStat;
    public Label MaxKiStat;
    public Label StaminaStat;
    public Label MaxStaminaStat;
    public Label AttackStat;
    public Label DefenseStat;
    public Label MagicAttackStat;
    public Label MagicDefenseStat;
    public Label EvadeStat;
    public Label ResistanceStat;
    public Label MoveStat;
    public Label JumpStat;
    public Label InteligenceStat;
    public Label CharismaStat;
    public VisualElement SkillMenu;
    public VisualElement Inventory;
    public VisualElement BattleUi;
    #endregion
    void Start()
    {
        // unit = stateMachine.characters[stateMachine.currentPlayerIndex];
        var root = GetComponent<UIDocument>().rootVisualElement;
        Actions = root.Q<Button>("Actions");
        Move = root.Q<Button>("Move");
        Wait = root.Q<Button>("Wait");
        // Attack = root.Q<Button>("Attack");
        // Skills = root.Q<Button>("Skills");
        BackToBM = root.Q<Button>("BackToBM");
        BattleMenu = root.Q<VisualElement>("BattleMenu");
        BattleUi = root.Q<VisualElement>("BattleUi");
        CharacterMenu = root.Q<VisualElement>("CharacterMenu");
        ActionsMenu = root.Q<ScrollView>("ActionsMenu");
        HealthBar = root.Q<ProgressBar>("HealthBar");
        ManaBar = root.Q<ProgressBar>("ManaBar");
        StaminaBar = root.Q<ProgressBar>("StaminaBar");

        Name = root.Q<Label>("Name");
        Experience = root.Q<Label>("Experience");
        HealthStat = root.Q<Label>("HealthStat");
        MaxHealthStat = root.Q<Label>("MaxHealthStat");
        KiStat = root.Q<Label>("KiStat");
        MaxKiStat = root.Q<Label>("MaxKiStat");
        StaminaStat = root.Q<Label>("StaminaStat");
        MaxStaminaStat = root.Q<Label>("MaxStaminaStat");
        Level = root.Q<Label>("Level");
        AttackStat = root.Q<Label>("AttackStat");
        DefenseStat = root.Q<Label>("DefenseStat");
        MagicAttackStat = root.Q<Label>("MagicAttackStat");
        MagicDefenseStat = root.Q<Label>("MagicDefenseStat");
        EvadeStat = root.Q<Label>("EvadeStat");
        ResistanceStat = root.Q<Label>("ResistanceStat");
        MoveStat = root.Q<Label>("MoveStat");
        JumpStat = root.Q<Label>("JumpStat");
        InteligenceStat = root.Q<Label>("InteligenceStat");
        CharismaStat = root.Q<Label>("CharismaStat");


        Actions.clicked += actionsButtonPressed;
        Move.clicked += moveButtonPressed;
        Wait.clicked += waitButtonPressed;
        // Attack.clicked += attackButtonPressed;
        // Skills.clicked += skillsButtonPressed;
        BackToBM.clicked += backToBMPressed;
        
    }
    #region  Battle Menu
    public void alternateBattleUi(){
         if(BattleUi.style.display == DisplayStyle.Flex){
                BattleUi.style.display = DisplayStyle.None;
            } else {
                BattleUi.style.display = DisplayStyle.Flex;
                displayBattleMenu();
            }
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
        displaySkills();
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
    // public void attackButtonPressed(){
    //     stateMachine.setState(new ActionSelectState(stateMachine));
    // }
    // public void skillsButtonPressed(){
    //     Attack.style.display = DisplayStyle.None;
    //     Skills.style.display = DisplayStyle.None;
    //             BackToBM.style.display = DisplayStyle.Flex;
    //     displaySkills();

    //     //iterate through skills and show buttons for each
    // }
    public void backToBMPressed(){
        stateMachine.setState(new BattleMenuState(stateMachine));
    }
    public void displayResourceBars(){
        HealthBar.style.display = DisplayStyle.Flex;
        ManaBar.style.display = DisplayStyle.Flex;      
        StaminaBar.style.display = DisplayStyle.Flex;  
    }
    public void hideResourceBars(){
        HealthBar.style.display = DisplayStyle.None;
        ManaBar.style.display = DisplayStyle.None;      
        StaminaBar.style.display = DisplayStyle.None;  
    }
    public void updateBars(){
        Character unit = stateMachine.characters[stateMachine.currentPlayerIndex];
        Health health = unit.GetComponent<Health>();
        Ki ki = unit.GetComponent<Ki>();
        Stamina stamina = unit.GetComponent<Stamina>();
        HealthBar.value = health.current;
        HealthBar.highValue = health.max;
        ManaBar.value = ki.current;
        ManaBar.highValue = ki.max;
        StaminaBar.value = stamina.current;
        StaminaBar.highValue = stamina.max;
    }
    public void displaySkills(){
        foreach (Ability skill in stateMachine.characters[stateMachine.currentPlayerIndex].skills)
        {
         Button skillButton = new Button() { text = skill.name}; 
         ActionsMenu.Add(skillButton);
         skillButton.clicked += delegate{SelectSkill(skill, skillButton);};   
        }
    }
    public void clearSkills(){
        ActionsMenu.Clear();
    }
    public void SelectSkill(Ability skill, Button skillButton){
        skillButton.clicked -= delegate{SelectSkill(skill, skillButton);};
        stateMachine.characters[stateMachine.currentPlayerIndex].GetComponent<AbilityHolder>().ChangeAbility(skill);
        stateMachine.setState(new ActionSelectState(stateMachine));
        clearSkills();
    }
        
    #endregion
    #region  Test Character Menu
        public void alternateCharacterMenu(){
            if(CharacterMenu.style.display == DisplayStyle.Flex){
                CharacterMenu.style.display = DisplayStyle.None;
            } else {
                CharacterMenu.style.display = DisplayStyle.Flex;
                populateCharacterMenu(stateMachine.characters[stateMachine.currentPlayerIndex]);
            }
        }
        public void displayCharacterMenu(){
            CharacterMenu.style.display = DisplayStyle.Flex;
            populateCharacterMenu(stateMachine.characters[stateMachine.currentPlayerIndex]);
        }
        private void populateCharacterMenu(Character current)
        {
            Name.text = current.name;
            Experience.text  = current.findStatbyName(Stats.EXP).Stringify();
            HealthStat.text  = current.findStatbyName(Stats.HP).Stringify();
            MaxHealthStat.text  = current.findStatbyName(Stats.MHP).Stringify();
            KiStat.text  = current.findStatbyName(Stats.KI).Stringify();
            MaxKiStat.text  = current.findStatbyName(Stats.MKI).Stringify();
            StaminaStat.text  = current.findStatbyName(Stats.STA).Stringify();
            MaxStaminaStat.text  = current.findStatbyName(Stats.MST).Stringify();
            Level.text  = current.findStatbyName(Stats.LVL).Stringify();
            AttackStat.text  = current.findStatbyName(Stats.ATK).Stringify();
            DefenseStat.text  = current.findStatbyName(Stats.DEF).Stringify();
            MagicAttackStat.text  = current.findStatbyName(Stats.MAT).Stringify();
            MagicDefenseStat.text  = current.findStatbyName(Stats.MDF).Stringify();
            EvadeStat.text  = current.findStatbyName(Stats.EVD).Stringify();
            ResistanceStat.text  = current.findStatbyName(Stats.RES).Stringify();
            MoveStat.text  = current.findStatbyName(Stats.MOV).Stringify();
            JumpStat.text  = current.findStatbyName(Stats.JMP).Stringify();
            InteligenceStat.text  = current.findStatbyName(Stats.INT).Stringify();
            CharismaStat.text  = current.findStatbyName(Stats.CHA).Stringify();
        }
    #endregion

    public void updateUi(){
        populateCharacterMenu(stateMachine.characters[stateMachine.currentPlayerIndex]);
        updateBars();
    }
    private void OnDisable() {
        //unlisten    
    }
}
