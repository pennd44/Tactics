using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName =" Local Game Event")]
public class LocalGameEvent : GameEvent
{
    // private List<GameEventListener> listeners = new List<GameEventListener>();
    public override void TriggerEvent(GameObject gO)
    {
        for (int i = listeners.Count -1; i >= 0; i--)
        {
            if (listeners[i].gameObject == gO) listeners[i].OnEventTriggered();
        }
    }
    // public void AddListener(GameEventListener listener)
    // {
    //     listeners.Add(listener);
    // }
    // public void RemoveListener(GameEventListener listener)
    // {
    //     listeners.Remove(listener);
    // }
}