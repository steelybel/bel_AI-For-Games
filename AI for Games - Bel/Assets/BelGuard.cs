using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BelGuard : MonoBehaviour
{
    private BelDjikstra djikstra;
    private NavMeshAgent nav;
    private bool chasing = false;
    private List<BelTile> pathGoin;
    private BelFiniteStates finiteStates;
    private Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        djikstra = GetComponent<BelDjikstra>();
        nav = GetComponent<NavMeshAgent>();
        finiteStates = GetComponent<BelFiniteStates>();
        initialPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 eyes = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        Debug.DrawRay(eyes, Vector3.forward, Color.red);
    }
    public void DjikstraChase()
    {
        if (djikstra.pathWay.Count > 0)
        {
            for (int a = 0; a < djikstra.pathWay.Count - 1; a++)
            {
                Debug.DrawLine(djikstra.pathWay[a].transform.position, djikstra.pathWay[a + 1].transform.position, Color.cyan);
            }
        }
        
        if (!chasing)
        {
            Debug.Log("Initiating search");
            djikstra.Search();
            chasing = true;
            Debug.Log("Phase 1 - setting to start destination");
            Vector3 nodePos = djikstra.pathWay[0].transform.position;
            Vector3 betterPos = new Vector3(nodePos.x, transform.position.y, nodePos.z);
            nav.SetDestination(betterPos);
        }
        else
        {
            if (nav.remainingDistance <= 0.5f)
            {
                Debug.Log("Yee haw!");
                if (djikstra.pathWay.Count > 0)
                {
                    djikstra.pathWay.RemoveAt(0);
                    if (djikstra.pathWay.Count > 0) // if it's STILL greater than 0 after removal
                    {
                        Vector3 nodePos = djikstra.pathWay[0].transform.position;
                        Vector3 betterPos = new Vector3(nodePos.x, transform.position.y, nodePos.z);
                        nav.SetDestination(betterPos);
                        Debug.Log("Step taken, there are now " + djikstra.pathWay.Count + "steps left.");
                    }
                }
                else
                {
                    Debug.Log("Broke out of the loop!");
                    chasing = false;
                }
            }
        }
    }
    public void NavSeek(Transform seek)
    {
        chasing = false;
        nav.SetDestination(seek.position);
    }
    public void NavFlee(Transform flee)
    {
        chasing = false;
        Vector3 v = (transform.position - flee.position).normalized;
        if (nav.path == null) nav.SetDestination(v);
    }
    public void Clear()
    {
        chasing = false;
        nav.isStopped = true;
        nav.ResetPath();
    }
    public void ResetPos()
    {
        transform.position = initialPos;
    }
}
