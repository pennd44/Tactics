using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkMovement : BattleMovement
{
    protected override bool ExpandSearch (Tile from, Tile to)
    {
        if((Mathf.Abs(from.height - to.height) > unit.jumpHeight))
            return false;
        if ( to.occupied)
            return false;
        return base.ExpandSearch(from, to);
    }

    public override IEnumerator Traverse(Tile end)
    {

        List<Tile> targets = new List<Tile>();
        while (end != null)
        {
            targets.Insert(0, end);
            end = end.prev;
        }
        for (int i = 1; i < targets.Count; i++)
        {
            Tile to = targets[i];
            yield return StartCoroutine(Walk(to));
        }
    }
    public IEnumerator Walk(Tile target)
    {

        Vector3 playerPosition = unit.gameObject.transform.position;
        Vector3 tilePosition = target.gameObject.transform.position;
        while (unit.gameObject.transform.position != tilePosition)
        {
            Turn(tilePosition); 
            unit.transform.position = Vector3.MoveTowards(unit.gameObject.transform.position, tilePosition, 3.0f * Time.deltaTime);
            yield return null;
        }
    }

    public void 
    Turn(Vector3 target)
    {
        Vector3 direction = (target - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, lookRotation, Time.deltaTime * 5f);

    }
}