using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Vertex
{
    static int id_counter = 0;
    public int id;
    public int index;
    public Vector3 pos;
    public Vertex( Vector3 pos)
    {
        this.id = id_counter++;
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
