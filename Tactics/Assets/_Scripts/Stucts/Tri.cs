using UnityEngine;
[System.Serializable]

public class Tri
{
    public Vector3 First;
    public Vector3 Second;
    public Vector3 Third;
    public Tri(Vector3 first, Vector3 second, Vector3 third){
        this.First = first;
        this.Second = second;
        this.Third = third;
    }
    public override string ToString()
    {
        return "1 : " + First + " 2: " + Second + " 3: " + Third;
    }
}