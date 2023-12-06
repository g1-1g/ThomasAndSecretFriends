using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;
    void OnTriggerEnter(Collider other)
    {

        prefab = Resources.Load("Prefabs/ClearUI") as GameObject;
        parent = GameObject.Find("Canvas").transform;
        StartCoroutine(WaitForIt()); 
    }
    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject myInstance = Instantiate(prefab, parent);

    }
}
