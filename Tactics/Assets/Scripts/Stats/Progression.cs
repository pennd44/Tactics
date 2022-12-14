using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
public class Progression : ScriptableObject{
    [SerializeField] ProgressionCharacterClass [] characterClass = null;
    [System.Serializable]
    class ProgressionCharacterClass
    {
        [SerializeField] CharacterClass characterClass;
        [SerializeField] int[] health;
    }
}