using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleMovement : MonoBehaviour
{
    protected Character unit;
    protected Board board;
    public BattleCameraMovement cameraController;
    // protected Transform jumper;
    protected virtual void Awake() {
        unit = GetComponent<Character>();
        board = GameObject.FindObjectOfType<Board>();
        cameraController = GameObject.FindObjectOfType<BattleCameraMovement>();
        // jumper = transform.FindChild("Jumper");
    }
    public virtual List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = board.Search(unit.currentTile, ExpandSearch);
        Filter(retValue);
        return retValue;    
    }
    protected virtual bool ExpandSearch (Tile from, Tile to)
    {
        return (from.distance + 1 <= unit.range);
    }
    protected virtual void Filter(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
           if(tiles[i].occupied){
            tiles.RemoveAt(i);
           }
        }

    }
    public abstract IEnumerator Traverse(Tile tile);
    public abstract void Turn(Vector3 target);
    public virtual IEnumerator ITurn(Vector3 target)
    {
        Vector3 direction = (target - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        while(Quaternion.Angle(unit.transform.rotation, lookRotation) >= 0.01f)
        {
            unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, lookRotation, Time.deltaTime * 5f);
            yield return null;
        }
        unit.SetDir();
    }
    public virtual void CameraFollow()
    {
        cameraController.transform.position = unit.transform.position;
    }
    // protected virtual IEnumerator Turn(Directions dir){
    //     TransformLocalEulerTweener t = (TransformLocalEulerTweener) transform.RotateToLocal(dir.ToEuler(), 0.25f, EasingEquations.EaseInOutQuad);
    //     if (Mathf.Approximately(t.startValue.y, 0f) && Mathf.Approximately(t.endValue.y, 270f))
    //         t.startValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
    //     else if (Mathf.Approximately(t.startValue.y, 270) && Mathf.Approximately(t.endValue.y, 0))
    //         t.endValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
    //     unit.dir = dir;
    //     while(t != null)
    //         yield return null;
    // }
}