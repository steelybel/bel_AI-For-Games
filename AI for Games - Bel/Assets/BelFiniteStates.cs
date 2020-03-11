using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BelFiniteStates : MonoBehaviour
{
	public enum States
	{
		Patrol,
		Go
	}

	public Transform target;
    public float radius = 1f;

    private bool found = false;

    public UnityEvent patrolMethods;
    public UnityEvent goMethods;
    public UnityEvent resetMethods;

    private States currentState;
    public States defaultState;
    private void Awake()
    {
        currentState = defaultState;
    }

    void Update()
	{
        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            resetMethods.Invoke();
            currentState = States.Patrol;
        }
        if (currentState != States.Go && Vector3.Distance(transform.position, target.position) <= radius) { ChangeState(States.Go); }
        if (currentState == States.Go && Vector3.Distance(transform.position, target.position) > radius) { ChangeState(States.Patrol); }

        switch (currentState)
		{
			case States.Patrol:
			Patrol();
			break;
			case States.Go:
            Go();
			break;
			default:
			Debug.LogError("Invalid state!");
			break;
		}
	}
	void Patrol()
	{
        patrolMethods.Invoke();
        
	}
	void Go()
	{
        goMethods.Invoke();
    }
    void ChangeState(States newState)
    {
        currentState = newState;
    }

}
