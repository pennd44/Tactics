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

    // if jump reaches wall you can climb
    public bool occupied = false;
    public bool selected = false;
    public bool selectable = false;
    public bool visited = false;
    public int cost = 1;

    Board board;

    private void Awake()
    {
        pos = new Point(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        height = transform.position.y;
        originalMaterial = gameObject.GetComponent<Renderer>().material;
        board = GetComponentInParent<Board>();
    }
    public void changeHighlight(Material material)
    {
        Material[] newMaterials = new Material[] { originalMaterial, material };
        GetComponent<Renderer>().materials = newMaterials;
    }
    public void removeHighlight()
    {
        Material[] newMaterials = new Material[] { originalMaterial };
        GetComponent<Renderer>().materials = newMaterials;
    }
    public event System.Action<Character> OnEntityEnterTile;
    public event System.Action<Character> OnEntityExitTile;

    private Character currentCharacter;

    public List<Tile> GetNeighbors()
    {
        List<Tile> neighbors = new List<Tile>();
        for (int i = 0; i < 4; i++)
        {
            Tile neighbor = board.getTile(pos + board.dirs[i]);
            if (neighbor != null)
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }
    public void EnterTile(Character character)
    {
        currentCharacter = character;
        occupied = true;
        OnEntityEnterTile?.Invoke(character);
    }

    public void ExitTile(Character character)
    {
        if (currentCharacter != null)
        {
            occupied = false;
            OnEntityExitTile?.Invoke(currentCharacter);
            currentCharacter = null;
        }
    }
    public event System.Action<Character> OnEntityPassThroughTile;
    public void PassthroughTile(Character character)
    {
        OnEntityPassThroughTile?.Invoke(character);
    }


}
