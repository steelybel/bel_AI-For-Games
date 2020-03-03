using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelTile : MonoBehaviour
{
    public int gScore;
    public Vector2 pos;
    public BelTile prevNode = null;
    public BelTile up, down, right, left = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Report()
    {
        Debug.Log("The node above costs " + up.gScore);
        Debug.Log("The node below costs " + down.gScore);
        Debug.Log("The node left costs " + left.gScore);
        Debug.Log("The node right costs " + right.gScore);
    }
}
