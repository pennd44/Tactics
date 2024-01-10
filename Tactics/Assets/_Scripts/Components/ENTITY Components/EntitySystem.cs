using UnityEngine;

public abstract class EntitySystem : MonoBehaviour
{
    protected Entity entity;
    protected virtual void Awake() {
        entity = GetComponent<Entity>();
    }
}
