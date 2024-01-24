using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Vertex
{
    public int id;
    public Vector3 pos;
    public Vertex( int id, Vector3 pos)
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
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        return pos == ((Vertex)obj).pos;
    }
}
