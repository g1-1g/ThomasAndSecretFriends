using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pick : MonoBehaviour
{
    public int count = 0;
    [SerializeField]
    AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        

    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            audioSource.Play();
            other.gameObject.SetActive(false);
            count = count + 1;  
        }
    }
}
