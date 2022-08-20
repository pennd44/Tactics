using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceStat : MonoBehaviour
{
    [SerializeField] public int current;
    [SerializeField] public int max;
    public void ReduceCurrent(int ammount){
        Debug.Log(current);
        current = Mathf.Max(current - ammount, 0);
        Debug.Log(current);
    }
    public void ReduceMax(int ammount){
        max = Mathf.Max(max - ammount, 0);
    }
    public void RaiseCurrent(int ammount){
        current = Mathf.Min(current + ammount, max);
    }
    public void RaiseMax(int ammount){
        max = max + ammount;
    }
}
