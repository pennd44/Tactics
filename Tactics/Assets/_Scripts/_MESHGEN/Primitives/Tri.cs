using System.Runtime.ExceptionServices;
using UnityEngine;
[System.Serializable]

public class Tri
{
    static int idCounter = 0;
    public int id;
    public int index;
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
    public Tri compliment;

    public Tri(Vertex first, Vertex second, Vertex third)
    {
        this.id = idCounter++;
        this.First = first;
        this.Second = second;
        this.Third = third;
        SetUp();
    }

    public void SetIndex(int index)
    {
        this.index = index; 
        First.index = index;
        Second.index = index + 1;
        Third.index = index + 2;
        longestEdgePoints1Index += index;
        longestEdgePoints2Index += index;
        cornerPointIndex += index;
    }

    private void SetUp()
    {
        float distance0 = Vector3.Distance(First.pos, Second.pos);
        float distance1 = Vector3.Distance(Second.pos, Third.pos);
        float distance2 = Vector3.Distance(Third.pos, First.pos);
        if (distance0 > distance1 && distance0 > distance2)
        {
            longestEdgePoints[0] = First;
            longestEdgePoints1Index = 0;
            longestEdgePoints[1] = Second;
            longestEdgePoints2Index = 1;
            cornerPoint = Third;
            cornerPointIndex = 2;
        }
        else if (distance1 > distance2)
        {
            longestEdgePoints[0] = Second;
            longestEdgePoints1Index = 1;
            longestEdgePoints[1] = Third;
            longestEdgePoints2Index = 2;
            cornerPoint = First;
            cornerPointIndex = 0;
        }
        else
        {
            longestEdgePoints[0] = Third;
            longestEdgePoints1Index = 2;
            longestEdgePoints[1] = First;
            longestEdgePoints2Index = 0;
            cornerPoint = Second;
            cornerPointIndex = 1;
        }
    }
    public override string ToString()
    {
        return "1 : " + First + " 2: " + Second + " 3: " + Third;
    }
}