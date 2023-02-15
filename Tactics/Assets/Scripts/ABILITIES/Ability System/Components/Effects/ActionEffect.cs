using UnityEngine;
// [System.Serializable]
public abstract class ActionEffect : ScriptableObject {
    // [SerializeField] public int ammount;
    public Character user;
    public abstract void AffectTarget(GameObject target, int ammount);
}
