using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceStat : MonoBehaviour
{
    [SerializeField] GameEvent onCharReduceCurrent;
    [SerializeField] GameEvent onCharRaiseCurrent;
    [SerializeField] GameEvent onCharReduceMax;
    [SerializeField] GameEvent onCharRaiseMax;
    [SerializeField] GameEvent onCharReachZero;
    [SerializeField] GameEvent onCharReachMax;
    [SerializeField] LocalGameEvent onReduceCurrent;
    [SerializeField] LocalGameEvent onRaiseCurrent;
    [SerializeField] LocalGameEvent onReduceMax;
    [SerializeField] LocalGameEvent onRaiseMax;
    [SerializeField] LocalGameEvent onReachZero;
    [SerializeField] LocalGameEvent onReachMax;
    [SerializeField] public int current;
    [SerializeField] public int max;
    public virtual void ReduceCurrent(int ammount){
        current = Mathf.Max(current - ammount, 0);
        onReduceCurrent.TriggerEvent(gameObject);
        onCharReduceCurrent.TriggerEvent(gameObject);
        if(current == 0){
            onReachZero.TriggerEvent(gameObject);    
            onCharReachZero.TriggerEvent(gameObject);    
        } 
    }
    public void ReduceMax(int ammount){
        max = Mathf.Max(max - ammount, 0);
        onReduceMax.TriggerEvent(gameObject);
    }
    public void RaiseCurrent(int ammount){
        current = Mathf.Min(current + ammount, max);
        onRaiseCurrent.TriggerEvent(gameObject);
        if(current == max) onReachMax.TriggerEvent(gameObject);
    }
    public void RaiseMax(int ammount){
        max = max + ammount;
        onRaiseMax.TriggerEvent(gameObject);
    }
}
