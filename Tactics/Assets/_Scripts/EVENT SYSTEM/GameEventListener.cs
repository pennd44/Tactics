using UnityEngine;
using UnityEngine.Events;
public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public UnityEvent onEventTriggered;
    
    public CharacterEvent onCharacterEventTriggered;
    void OnEnable()
    {
        gameEvent.AddListener(this);
    }
    void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }
    public void OnEventTriggered()
    {
        onEventTriggered.Invoke();
        onCharacterEventTriggered.Invoke(GetComponent<Character>());
    }
}
