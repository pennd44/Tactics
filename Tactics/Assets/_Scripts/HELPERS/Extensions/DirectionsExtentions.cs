using UnityEngine;
using System.Collections;

public static class DirectionsExtentions
{
    public static Directions GetDirections(this Tile t1, Tile t2){
        if(t1.pos.y < t2.pos.y)
        return Directions.North;
        if(t1.pos.x < t2.pos.x)
        return Directions.East;
        if(t1.pos.y > t2.pos.y)
        return Directions.South;
        return Directions.West;
    }
    public static Vector3 ToEuler (this Directions d)
    {
        return new Vector3(0, (int)d * 90, 0);
    }
    public static Point ToPoint (this Directions d)
    {
        switch (d)
        {
            case Directions.North:
                return new Point(0, 1);
            case Directions.South:
                return new Point(0, -1);
            case Directions.East:
                return new Point(1, 0);
            case Directions.West:
                return new Point(-1, 0);
            default:
                return new Point(0,0);
        }
    }
}