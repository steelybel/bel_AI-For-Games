using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(BelTilemap))]
public class BelDjikstra : MonoBehaviour
{
    public BelTilemap map;
    private Dictionary<BelTile, NodeData> nodes = new Dictionary<BelTile, NodeData>();
    public Vector2Int start = new Vector2Int(0, 0);
    public Vector2Int goal = new Vector2Int(1, 1);
    public List<BelTile> openList = new List<BelTile>();
    public List<BelTile> closedList = new List<BelTile>();
    public List<BelTile> pathWay = new List<BelTile>();
    public bool aStar = false;
    public bool going = false;
    public List<Vector2Int> routine = new List<Vector2Int>();
    private int routeIter = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void Djikstra(BelTile current)
    {
        if (!nodes.ContainsKey(current)) { nodes.Add(current, new NodeData(current)); }
        openList.Remove(current);
        //MarkNode(current);
        closedList.Add(current);
        List<NodeData> connected = new List<NodeData>();
        if (current.up != null && !closedList.Contains(current.up) && !openList.Contains(current.up))
        {
            NodeData upNode = new NodeData(current.up);
            upNode.prevNode = nodes[current]; connected.Add(upNode);
            upNode.gScore = nodes[current].gScore + upNode.weight;
        }
        if (current.down != null && !closedList.Contains(current.down) && !openList.Contains(current.down))
        {
            NodeData downNode = new NodeData(current.down);
            downNode.prevNode = nodes[current]; connected.Add(downNode);
            downNode.gScore = nodes[current].gScore + downNode.weight;
        }
        if (current.left != null && !closedList.Contains(current.left) && !openList.Contains(current.left))
        {
            NodeData leftNode = new NodeData(current.left);
            leftNode.prevNode = nodes[current]; connected.Add(leftNode);
            leftNode.gScore = nodes[current].gScore + leftNode.weight;
        }
        if (current.right != null && !closedList.Contains(current.right) && !openList.Contains(current.right))
        {
            NodeData rightNode = new NodeData(current.right);
            rightNode.prevNode = nodes[current]; connected.Add(rightNode);
            rightNode.gScore = nodes[current].gScore + rightNode.weight;
        }
        TileComp tc = new TileComp();
        if (connected.Count > 0) { connected.Sort(0, connected.Count - 1, tc); }
        connected.Reverse();
        foreach(NodeData data in connected)
        {
            openList.Add(data.original);
        }
        if (current.pos == goal)
        {
            TraceBack(current);
        }
        Debug.Log("Finished");
    }
    void TraceBack(BelTile a)
    {
        NodeData track = nodes[a];
        while (track.prevNode != null)
        {
            pathWay.Add(track.original);
            //MarkNodeAlt(track);
            track = track.prevNode;
        }
        pathWay.Add(map.nodes[start.x, start.y]);
        pathWay.Reverse();
    }
    public void Search()
    {
        closedList.Clear();
        openList.Clear();
        nodes.Clear();
        foreach (BelTile node in map.nodes)
        {
            //node.GetComponent<MeshRenderer>().material = blank;
            //node.prevNode = null;
        }
        if (routine.Count > 0)
        {
            if (routeIter >= routine.Count)
            {
                routeIter = 0;
            }
            start = routine[routeIter];
            goal = routine[(routeIter == routine.Count-1) ? 0 : routeIter + 1]; //on the final coordinate of the routine, go back to the first coordinate
        }
        openList.Add(map.nodes[start.x, start.y]);

        Debug.Log("searching on route no. " + routeIter);
        while (!gotGoal())
        {
            while (openList.Count > 0)
            {
                Djikstra(openList[0]);
            }
        }
        if (routine.Count > 0) { routeIter++; }
        return;
    }
    public void Initialize()
    {
        routeIter = 0;
        closedList.Clear();
        openList.Clear();
    }
    private bool gotGoal()
    {
        foreach (BelTile node in closedList)
        {
            if (node.pos == goal) return true;
        }
        return false;
    }
}

public class TileComp : IComparer<NodeData>
{
    // Compares by Height, Length, and Width.
    public int Compare(NodeData x, NodeData y)
    {
        if (x.gScore.CompareTo(y.gScore) != 0)
        {
            return x.gScore.CompareTo(y.gScore);
        }
        else
        {
            return 0;
        }
    }
}