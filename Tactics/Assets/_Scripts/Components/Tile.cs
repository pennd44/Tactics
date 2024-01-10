using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material originalMaterial;
    public Point pos;
    public float height;
    public int distance = int.MaxValue;
    public Tile prev;
    public GameObject content;

    //^ turn content to array of game objects or occupiers
    // have walls in content?
    //wall [] _walls
    //function to create wall from one tile to another
    //climbing state
    //bool diggable 
    //give tiles a thickness
    // for get cieling do tile height - thickness
    // for floors do infinite thickness
    //for diggable check thickness
    //if wall is over half unit height, knockback smacks them into the wall, otherwise they trip over the wall

    public bool occupied = false;
    public bool selected = false;
    public bool selectable = false;
    public bool visited = false;
    public int cost = 1;

    private void Awake() {
        pos = new Point(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        height = transform.position.y;
        originalMaterial = gameObject.GetComponent<Renderer>().material;
    }
    public void changeHighlight(Material material){
        Material [] newMaterials = new Material [] {originalMaterial, material};
        GetComponent<Renderer>().materials = newMaterials;
    }
    public void removeHighlight(){
        Material [] newMaterials = new Material [] {originalMaterial};
        GetComponent<Renderer>().materials = newMaterials;
    }

}
