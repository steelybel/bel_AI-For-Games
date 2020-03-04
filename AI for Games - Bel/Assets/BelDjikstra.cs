using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BelTilemap))]
public class BelDjikstra : MonoBehaviour
{
    private BelTilemap map;
    public Material blank;
    public Material mark;
    public Material path;
    public Vector2Int start = new Vector2Int(0, 0);
    public Vector2Int goal = new Vector2Int(1, 1);
    public List<BelTile> openList = new List<BelTile>();
    public List<BelTile> closedList = new List<BelTile>();
    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<BelTilemap>();
        foreach (BelTile node in map.nodes)
        {
            node.GetComponent<MeshRenderer>().material = blank;
        }
        openList.Add(map.nodes[start.x,start.y]);
    }

    // Update is called once per frame
    void Update()
    {
        while (!closedList.Contains(map.nodes[goal.x,goal.y]))
        {
            while (openList.Count > 0)
            {
                Djikstra(openList[0]);
            }
        }
        

    }
    bool Found()
    {
        return false;
        //BelTile g = map.nodes[(int)goal.x, (int)goal.y];
        //if (current.up == g || current.down == g || current.left == g || current.right == g) { return true; }
        //else { return false; }
    }
    void Djikstra(BelTile current)
    {
        openList.Remove(current);
        MarkNode(current);
        closedList.Add(current);
        List<BelTile> connected = new List<BelTile>();
        if (current.up != null && !closedList.Contains(current.up) && !openList.Contains(current.up)) { current.up.prevNode = current; connected.Add(current.up); }
        if (current.down != null && !closedList.Contains(current.down) && !openList.Contains(current.down)) { current.down.prevNode = current; connected.Add(current.down); }
        if (current.left != null && !closedList.Contains(current.left) && !openList.Contains(current.left)) { current.left.prevNode = current; connected.Add(current.left); }
        if (current.right != null && !closedList.Contains(current.right) && !openList.Contains(current.right)) { current.right.prevNode = current; connected.Add(current.right); }
        TileComp tc = new TileComp();
        if (connected.Count > 0) { connected.Sort(0, connected.Count - 1, tc); }
        connected.Reverse();
        openList.AddRange(connected);
        if (current == map.nodes[goal.x,goal.y])
        {
            TraceBack(current);
        }
    }
    void AStar(BelTile current)
    {
        openList.Remove(current);
        MarkNode(current);
        closedList.Add(current);
        List<BelTile> connected = new List<BelTile>();
        if (current.up != null && !closedList.Contains(current.up) && !openList.Contains(current.up)) { current.up.prevNode = current; connected.Add(current.up); }
        if (current.down != null && !closedList.Contains(current.down) && !openList.Contains(current.down)) { current.down.prevNode = current; connected.Add(current.down); }
        if (current.left != null && !closedList.Contains(current.left) && !openList.Contains(current.left)) { current.left.prevNode = current; connected.Add(current.left); }
        if (current.right != null && !closedList.Contains(current.right) && !openList.Contains(current.right)) { current.right.prevNode = current; connected.Add(current.right); }
        TileComp tc = new TileComp();
        if (connected.Count > 0) { connected.Sort(0, connected.Count - 1, tc); }
        connected.Reverse();
        openList.AddRange(connected);
        if (current == map.nodes[goal.x, goal.y])
        {
            TraceBack(current);
        }
    }
    void MarkNode(BelTile t)
    {
        t.GetComponent<MeshRenderer>().material = mark;
    }
    void MarkNodeAlt(BelTile t)
    {
        t.GetComponent<MeshRenderer>().material = path;
    }
    void TraceBack(BelTile a)
    {
        BelTile track = a;
        while (track.prevNode != null)
        {
            MarkNodeAlt(track);
            track = track.prevNode;
        }
    }
}

public class TileComp : IComparer<BelTile>
{
    // Compares by Height, Length, and Width.
    public int Compare(BelTile x, BelTile y)
    {
        if (x.gScore().CompareTo(y.gScore()) != 0)
        {
            return x.gScore().CompareTo(y.gScore());
        }
        else
        {
            return 0;
        }
    }
}