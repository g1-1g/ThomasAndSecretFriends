using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateKey : MonoBehaviour
{
    private float y;
    void Start()
    {
        
    }

    void Update()
    {
        y += 0.02f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, y, 0);
    }
}
