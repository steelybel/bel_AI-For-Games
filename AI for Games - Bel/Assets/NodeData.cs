using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeData
{
    public NodeData() { }
    public NodeData(BelTile tile)
    {
        weight = tile.weight;
        pos = tile.pos;
        original = tile;
        realPos = tile.transform.position;
    }
    //public NodeData(BelTile tile, Direction dir, NodeData original)
    //{
    //    weight = tile.weight;
    //    pos = tile.pos;
    //    if (tile.up != null)
    //    {
    //        if (dir != Direction.North)
    //        {
    //            up = new NodeData(tile.up, Direction.South, this);
    //        }
    //        else { up = original; }
    //    }
    //    if (tile.down != null)
    //    {
    //        if (dir != Direction.South)
    //        {
    //            down = new NodeData(tile.down, Direction.North, this);
    //        }
    //        else { down = original; } }
    //    if (tile.left != null)
    //    {
    //        if (dir != Direction.West)
    //        {
    //            left = new NodeData(tile.left, Direction.East, this);
    //        }
    //        else { left = original; } }
    //    if (tile.right != null)
    //    {
    //        if (dir != Direction.East)
    //        {
    //            right = new NodeData(tile.right, Direction.West, this);
    //        }
    //        else { right = original; } }
    //    realPos = tile.transform.position;
    //}
    ~NodeData()
    {
        prevNode = null;
    }
    public enum Direction
    {
        North,
        South,
        East,
        West,
    }
    public BelTile original;
    public int weight = 1;
    public Vector2 pos;
    public NodeData prevNode = null;
    public Vector3 realPos;
    public float gScore = 0f;
}
