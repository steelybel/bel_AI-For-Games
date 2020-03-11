using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelDumbAI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform chasing;
    [SerializeField] private float maxVelocity = 3.0f;
    private Vector3 currentVelocity;
    [SerializeField] private float wanderRadius = 1f;
    [SerializeField] private float wanderDist = 1f;
    [SerializeField] private float wanderJitter = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void AddForce(Vector3 f)
    {
        currentVelocity += f * Time.deltaTime;
        currentVelocity = new Vector3(Mathf.Clamp(currentVelocity.x, -maxVelocity, maxVelocity), Mathf.Clamp(currentVelocity.y, -maxVelocity, maxVelocity), Mathf.Clamp(currentVelocity.z, -maxVelocity, maxVelocity));
    }
    void Seek(Transform chase)
    {
        Vector3 v = (chase.position - transform.position).normalized * maxVelocity;
        Vector3 force = v - currentVelocity;
        AddForce(force);
        transform.position += (currentVelocity * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(v);
    }
    void Flee(Transform run)
    {
        Vector3 v = (transform.position - run.position).normalized * maxVelocity;
        Vector3 force = v - currentVelocity;
        AddForce(force);
        transform.position += (currentVelocity * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(v);
    }
    void Wander()
    {
        Vector3 w = new Vector3(Random.Range(-wanderJitter, wanderJitter),0, Random.Range(-wanderJitter, wanderJitter));
        w = w.normalized * wanderDist;
        w += transform.position;
        Vector3 force = w - currentVelocity;
        AddForce(force);
        transform.position += (currentVelocity * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(w);
    }
    void Pursue()
    {
        Vector3 v = (chasing.transform.position - transform.position).normalized * maxVelocity;
        Vector3 force = v - currentVelocity;
        currentVelocity += force * Time.deltaTime;
        transform.Translate(currentVelocity * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(v);
    }
}
