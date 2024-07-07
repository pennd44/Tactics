using System.Runtime.ExceptionServices;
using UnityEngine;
[System.Serializable]

public class Tri
{
    static int idCounter = 0;
    public int id;
    public Vertex First;
    // public int FirstIndex;
    public Vertex Second;
    // public int SecondIndex;
    public Vertex Third;
    // public int ThirdIndex;

    public int longestEdgePoints1Index;
    public int longestEdgePoints2Index;
    public int cornerPointIndex;

    public Vertex[] longestEdgePoints = new Vertex[2];
    public Vertex cornerPoint;

    public Tri(Vertex first, Vertex second, Vertex third)
    {
        this.id = idCounter++;
        this.First = first;
        this.Second = second;
        this.Third = third;
        SetUp();
    }


    private void SetUp()
    {
        float distance0 = Vector3.Distance(First.pos, Second.pos);
        float distance1 = Vector3.Distance(Second.pos, Third.pos);
        float distance2 = Vector3.Distance(Third.pos, First.pos);
        if (distance0 > distance1 && distance0 > distance2)
        {
            longestEdgePoints[0] = First;
            longestEdgePoints1Index = First.id;
            longestEdgePoints[1] = Second;
            longestEdgePoints2Index = Second.id;
            cornerPoint = Third;
            cornerPointIndex = Third.id;
        }
        else if (distance1 > distance2)
        {
            longestEdgePoints[0] = Second;
            longestEdgePoints1Index = Second.id;
            longestEdgePoints[1] = Third;
            longestEdgePoints2Index = Third.id;
            cornerPoint = First;
            cornerPointIndex = First.id;
        }
        else
        {
            longestEdgePoints[0] = Third;
            longestEdgePoints1Index = Third.id;
            longestEdgePoints[1] = First;
            longestEdgePoints2Index = First.id;
            cornerPoint = Second;
            cornerPointIndex = Second.id;
        }
    }
    public override string ToString()
    {
        return "1 : " + First + " 2: " + Second + " 3: " + Third;
    }
}