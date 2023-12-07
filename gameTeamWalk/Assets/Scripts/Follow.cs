using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    [SerializeField] public Transform targetTransform;
    NavMeshAgent navMeshAgent;

    public List<Follow> _boids;
    public float separationWeight = 1.0f;
    public float cohesionWeight = 1.0f;
    public float alignmentWeight = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        float separationRadius = 5;
        float cohesionRadius = 20;
        //default vars

        var steering = Vector3.zero;

        var separationDirection = Vector3.zero;
        var separationCount = 0;
        var alignmentDirection = Vector3.zero;
        var alignmentCount = 0;
        var cohesionDirection = Vector3.zero;
        var cohesionCount = 0;

        foreach (Follow boid in _boids)
        {
            //skip self
            if (boid == this)
                continue;
            var distance = Vector3.Distance(boid.transform.position, this.transform.position);

            if (distance < separationRadius)
            {
                separationDirection += (transform.position - boid.transform.position) / distance;
                separationCount++;
            }

            //identify local neighbour
            if (distance < cohesionRadius)
            {
                alignmentDirection += boid.transform.forward;
                alignmentCount++;

                cohesionDirection += boid.transform.position;
                cohesionCount++;
            }

        }

        if (separationCount > 0)
            separationDirection /= separationCount;

        if (alignmentCount > 0)
            alignmentDirection /= alignmentCount;

        if (cohesionCount > 0)
            cohesionDirection /= cohesionCount;

        alignmentDirection -= transform.forward;

        cohesionDirection = cohesionDirection - transform.position;

        steering += separationDirection * separationWeight;
        steering += alignmentDirection * alignmentWeight;
        steering += cohesionDirection * cohesionWeight;
        steering.Normalize();

        navMeshAgent.Move(steering * Time.deltaTime);
        navMeshAgent.destination = targetTransform.position;

        transform.LookAt(Camera.main.transform.position);
        //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}