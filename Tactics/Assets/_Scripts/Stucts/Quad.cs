using UnityEngine;
[System.Serializable]

public struct Quad
{
    public Vector3 First;
    public Vector3 Second;
    public Vector3 Third;
    public Vector3 Fourth;
    public Quad(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth){
        this.First = first;
        this.Second = second;
        this.Third = third;
        this.Fourth = fourth;
    }
}