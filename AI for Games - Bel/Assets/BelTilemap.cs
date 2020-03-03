using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelTilemap : MonoBehaviour
{
    public BelTile tileT;
    public int width = 10;
    public int height = 10;
    public BelTile[,] belTiles;
    private Vector2 look = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        belTiles = new BelTile[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                belTiles[x, y] = Instantiate(tileT, transform.position + new Vector3(x, y, 0) - new Vector3(width / 2, height / 2, 0), Quaternion.identity, transform);
                belTiles[x, y].pos = new Vector2(x, y);
                belTiles[x, y].gScore = 1;
            }
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (y > 0) { belTiles[x, y].down = belTiles[x, y - 1]; }
                if (y < height) { belTiles[x, y].up = belTiles[x, y + 1]; }
                if (x > 0) { belTiles[x, y].left = belTiles[x - 1, y]; }
                if (x < width) { belTiles[x, y].right = belTiles[x + 1, y]; }
            }
        }
        belTiles[0, 0].gScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
