using UnityEngine;
[System.Serializable]
public struct Vertex
{
    public int id;
    public Vector3 pos;
    public Vertex(int id, Vector3 pos)
    {
        this.id = id;
        this.pos = pos;
    }
     public static bool operator ==(Vertex a, Vertex b)
    {
        return a.pos == b.pos;
    }
    public static bool operator !=(Vertex a, Vertex b)
    {
        return !(a == b);
    }
    //get vertex
}
