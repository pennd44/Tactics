using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ability), true)]
public class AbilityInspector : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        Ability ability = (Ability) target; 
        // if(GUILayout.Button("Create Ability")){
        //     ability.FindAbilityComponents();
        // }
        if(GUILayout.Button("Log Components")){
            ability.LogComponents();
        }
    }
}