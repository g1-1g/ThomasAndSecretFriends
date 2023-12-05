using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pick : MonoBehaviour
{
    public int count = 0;
    void Start()
    {
        
    }

    void Update()
    {
        

    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;  
        }
    }
}
