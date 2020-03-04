using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BelTile : MonoBehaviour
{
    public int weight = 1;
    public Vector2 pos;
    public BelTile prevNode = null;
    public BelTile up, down, right, left = null;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray rayUp = new Ray(transform.position, Vector3.up);
        Ray rayDown = new Ray(transform.position, Vector3.down);
        Ray rayLeft = new Ray(transform.position, Vector3.left);
        Ray rayRight = new Ray(transform.position, Vector3.right);
        RaycastHit hit;
        if (Physics.Raycast(rayUp, out hit))
        {
            if (hit.collider.tag.Equals("Node"))
            {
                up = hit.collider.gameObject.GetComponent<BelTile>();
            }
        }
        if (Physics.Raycast(rayDown, out hit))
        {
            if (hit.collider.tag.Equals("Node"))
            {
                down = hit.collider.gameObject.GetComponent<BelTile>();
            }
        }
        if (Physics.Raycast(rayLeft, out hit))
        {
            if (hit.collider.tag.Equals("Node"))
            {
                left = hit.collider.gameObject.GetComponent<BelTile>();
            }
        }
        if (Physics.Raycast(rayRight, out hit))
        {
            if (hit.collider.tag.Equals("Node"))
            {
                right = hit.collider.gameObject.GetComponent<BelTile>();
            }
        }
    }
    public void Report()
    {
        Debug.Log("The node above costs " + up.gScore());
        Debug.Log("The node below costs " + down.gScore());
        Debug.Log("The node left costs " + left.gScore());
        Debug.Log("The node right costs " + right.gScore());
    }
    public float gScore()
    {
        if (prevNode != null)
        {
            float butt = prevNode.gScore() + weight;
            return butt;
        }
        else
        {
            return 0;
        }
        
    }
    public float hScore()
    {
        return 0;
    }
    public float fScore()
    {
        return (gScore()+hScore());
    }
}
