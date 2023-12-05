using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.VersionControl;
using UnityEngine;

public class Open : MonoBehaviour
{
    
    [SerializeField]
    private int KeyClear = 3;
    [SerializeField]
    AudioClip doorS;
    AudioSource doorSSource;

    bool isOn = false;
    bool isOpen = false;
    private Pick pick;
    private float x;
    private Camera MainC;

    void Start()
    {
        pick = FindObjectOfType<Pick>();
        MainC = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        doorSSource = gameObject.GetComponent<AudioSource>();

    }

    void Update()
    {
        if (pick.count == KeyClear)
        {
            if (!isOn) { doorSSource.clip = doorS; doorSSource.Play(); isOn = true; }
            if (!isOpen) {StartCoroutine(WaitForIt());}
            else if (isOpen && transform.eulerAngles.x >= 270)
            {
                x += 1 * Time.deltaTime;
                transform.eulerAngles += new Vector3(x, 0, 0);
            }
            else if (isOpen)
            {
                StartCoroutine(WaitForOpen());
                
            }       
        }

    }

    IEnumerator WaitForIt()
    {
        MainC.enabled = false;
        yield return new WaitForSeconds(1.0f);
        isOpen = true;
    }
    IEnumerator WaitForOpen() { 
        yield return new WaitForSeconds(1.0f);
        MainC.enabled = true;
        KeyClear = -1;
    }
}


