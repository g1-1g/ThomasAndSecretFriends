using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    public Follow boidPrefab;
    public Transform targetTransform;
    public int boidsNum = 3;
    public int boidDistance = 2;
    private List<Follow> _boids;


    public float separationWeight = 1.0f;
    public float cohesionWeight = 1.0f;
    public float alignmentWeight = 1.0f;
    // Start is called before the first frame update

    void Start()
    {
        _boids = new List<Follow>();
        for (int i = 0; i < boidsNum; i++)
        {
            for (int j = 0; j < boidsNum; j++)
                SpawnBoid(boidPrefab, transform.position + new Vector3((i - boidsNum / 2) * boidDistance, 0.0f, (j - boidsNum / 2) * boidDistance));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnBoid(Follow boidPrefab, Vector3 position)
    {
        var boidInstance = Instantiate(boidPrefab, position, new Quaternion());
        boidInstance.GetComponent<Follow>().targetTransform = targetTransform;
        boidInstance.GetComponent<Follow>()._boids = _boids;
        boidInstance.GetComponent<Follow>().separationWeight = separationWeight;
        boidInstance.GetComponent<Follow>().cohesionWeight = cohesionWeight;
        boidInstance.GetComponent<Follow>().alignmentWeight = alignmentWeight;
        _boids.Add(boidInstance);
    }
}