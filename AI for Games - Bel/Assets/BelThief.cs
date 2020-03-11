using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BelThief : MonoBehaviour
{
    public float lineOfSight = 1f;
    public float fleeSpeed = 1f;
    public Transform enemy;
    private NavMeshAgent nav;
    bool closeToGuard = false;
    private BelFiniteStates states;
    private void Start()
    {
        states = GetComponent<BelFiniteStates>();
    }

    public void GoldChase (Transform gold)
    {
        nav.SetDestination(gold.position);
    }
    public void Fleeing (Transform scary)
    {
        Vector3 v = (transform.position - scary.position).normalized * fleeSpeed;
        if (nav.path == null) nav.SetDestination(v);
    }
    public void Clear()
    {
        nav.isStopped = true;
        nav.ResetPath();
    }
}
