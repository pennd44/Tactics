using UnityEngine;

[CreateAssetMenu(fileName = "EntityID", menuName = "Tactics/EntityID", order = 0)]
public class EntityID : ScriptableObject {
    public string name;
    public EntityEvents events; 
}