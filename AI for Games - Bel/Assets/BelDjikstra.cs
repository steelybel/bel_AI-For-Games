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
        NodeData temp = new NodeData();
        if (!nodes.ContainsKey(current)) {
            if (openList.Contains(current)) { temp = new NodeData(current); }
            nodes.Add(current, temp);
        }
        else { temp = nodes[current]; }
        openList.Remove(current);
        //MarkNode(current);
        closedList.Add(current);
        List<NodeData> connected = new List<NodeData>();
        if (current.up != null && !closedList.Contains(current.up))
        {
            if (!nodes.ContainsKey(current.up))
            {
                NodeData _node = new NodeData(current.up)
                {
                    prevNode = temp
                };
                _node.gScore = temp.gScore + _node.weight;
                nodes.Add(current.up, _node);
                connected.Add(_node);
            }
            else
            {
                if (openList.Contains(current.up))
                {
                    float tempScore = temp.gScore + current.up.weight;
                    if (nodes[current.up].gScore < tempScore)
                    {
                        nodes[current.up].prevNode = temp;
                        nodes[current.up].gScore = tempScore;
                    }
                    
                }
                connected.Add(nodes[current.up]);
            }
            
        }
        if (current.down != null && !closedList.Contains(current.down))
        {
            if (!nodes.ContainsKey(current.down))
            {
                NodeData _node = new NodeData(current.down)
                {
                    prevNode = temp
                };
                _node.gScore = temp.gScore + _node.weight;
                nodes.Add(current.down, _node);
                connected.Add(_node);
            }
            else
            {
                if (openList.Contains(current.down))
                {
                    float tempScore = temp.gScore + current.down.weight;
                    if (nodes[current.down].gScore < tempScore)
                    {
                        nodes[current.down].prevNode = temp;
                        nodes[current.down].gScore = tempScore;
                    }
                    
                }
                connected.Add(nodes[current.down]);
            }
        }
        if (current.left != null && !closedList.Contains(current.left) && !openList.Contains(current.left))
        {
            if (!nodes.ContainsKey(current.left))
            {
                NodeData _node = new NodeData(current.left)
                {
                    prevNode = temp
                };
                _node.gScore = temp.gScore + _node.weight;
                nodes.Add(current.left, _node);
                connected.Add(_node);
            }
            else
            {
                if (openList.Contains(current.left))
                {
                    float tempScore = temp.gScore + current.left.weight;
                    if (nodes[current.left].gScore < tempScore)
                    {
                        nodes[current.left].prevNode = temp;
                        nodes[current.left].gScore = tempScore;
                    }
                    
                }
                connected.Add(nodes[current.left]);
            }
        }
        if (current.right != null && !closedList.Contains(current.right) && !openList.Contains(current.right))
        {
            if (!nodes.ContainsKey(current.right))
            {
                NodeData _node = new NodeData(current.right)
                {
                    prevNode = temp
                };
                _node.gScore = temp.gScore + _node.weight;
                nodes.Add(current.right, _node);
                connected.Add(_node);
            }
            else
            {
                if (openList.Contains(current.right))
                {
                    float tempScore = temp.gScore + current.right.weight;
                    if (nodes[current.right].gScore < tempScore)
                    {
                        nodes[current.right].prevNode = temp;
                        nodes[current.right].gScore = tempScore;
                    }

                }
                connected.Add(nodes[current.right]);
            }
        }
        TileComp tc = new TileComp();
        if (connected.Count > 0) { connected.Sort(0, connected.Count - 1, tc); }
        connected.Reverse();
        foreach (NodeData data in connected)
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
        pathWay = new List<BelTile>();
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
        if (gotGoal() && routine.Count > 0) { routeIter++; }
    }
    public void StartSearch()
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
            goal = routine[(routeIter == routine.Count - 1) ? 0 : routeIter + 1]; //on the final coordinate of the routine, go back to the first coordinate
        }
        openList.Add(map.nodes[start.x, start.y]);
    }
    public void ContinueSearch()
    {
        if (!gotGoal() && openList.Count > 0)
        {
            Djikstra(openList[0]);
            if (gotGoal() && routine.Count > 0) { routeIter++; }
            return;
        }
    }
    public void FinishSearch()
    {

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