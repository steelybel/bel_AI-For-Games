using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelTilemap : MonoBehaviour
{
    public BelTile tileT;
    public int width = 10;
    public int height = 10;
    public BelTile[,] nodes;
    private Vector2 look = Vector2.zero;
    public bool sideways = false;
    // Start is called before the first frame update
    void Start()
    {
        Init(sideways);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Init(bool zAxis)
    {
        nodes = new BelTile[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (zAxis)
                {
                    nodes[x, y] = Instantiate(tileT, transform.position + (new Vector3(x, 0, y) - new Vector3(width / 2, 0, height / 2)), Quaternion.identity, transform);
                }
                else
                {
                    nodes[x, y] = Instantiate(tileT, transform.position + (new Vector3(x, y, 0) - new Vector3(width / 2, height / 2, 0)), Quaternion.identity, transform);
                }
                nodes[x, y].tilemap = this;
                nodes[x, y].pos = new Vector2(x, y);
                nodes[x, y].weight = 1;
            }
        }
        //for (int y = 0; y < height; y++)
        //{
        //    for (int x = 0; x < width; x++)
        //    {
        //        if (y > 0) { nodes[x, y].down = nodes[x, y - 1]; }
        //        if (y < (height - 1)) { nodes[x, y].up = nodes[x, y + 1]; }
        //        if (x > 0) { nodes[x, y].left = nodes[x - 1, y]; }
        //        if (x < (width - 1)) { nodes[x, y].right = nodes[x + 1, y]; }
        //    }
        //}
        nodes[0, 0].weight = 0;
    }
}
