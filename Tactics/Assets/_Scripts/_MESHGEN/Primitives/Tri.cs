using System.Runtime.ExceptionServices;
using UnityEngine;
[System.Serializable]

public class Tri
{
    public int Id;
    public Vertex First;
    public int FirstIndex;
    public Vertex Second;
    public int SecondIndex;
    public Vertex Third;
    public int ThirdIndex;

    public Vertex [] longestEdgePoints = new Vertex[2];
    public Vertex cornerPoint;
    
    public Tri(int Id, Vertex first, Vertex second, Vertex third){
        this.Id = Id;
        this.First = first;
        this.Second = second;
        this.Third = third;
        SetUp();
    }

    private void SetUp(){
        float distance0 = Vector3.Distance(First.pos, Second.pos);
        float distance1 = Vector3.Distance(Second.pos, Third.pos);
        float distance2 = Vector3.Distance(Third.pos, First.pos);
        if (distance0 > distance1 && distance0 > distance2)
        {
            longestEdgePoints[0] = First;
            longestEdgePoints[1] = Second;
            cornerPoint = Third;
        }
        else if (distance1 > distance2){
            longestEdgePoints[0] = Second;
            longestEdgePoints[1] = Third;
            cornerPoint = First;
        }
        else{
            longestEdgePoints[0] = Third;
            longestEdgePoints[1] = First;
            cornerPoint = Second;
        }
    }
    public override string ToString()
    {
        return "1 : " + First + " 2: " + Second + " 3: " + Third;
    }
}