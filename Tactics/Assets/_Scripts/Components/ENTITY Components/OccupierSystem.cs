using UnityEngine;

public abstract class OccupierSystem : MonoBehaviour
{
    protected Entity entity;
    protected virtual void Awake() {
        entity = GetComponent<Entity>();
    }
}
