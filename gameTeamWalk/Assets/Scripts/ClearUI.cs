using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    private Image black;
    private Text Text;
    private float S;


    void Start()
    {
        GameObject[] toma = GameObject.FindGameObjectsWithTag("Toma"); 
        
        foreach (GameObject go in toma)
        {
            go.SetActive(false);
        }
        black = GetComponent<Image>();
        Text = GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (black.color.a < 255)
        {
            S += 0.05f * Time.deltaTime;

            Color colorB = black.color;
            colorB.a += S;
            black.color = colorB;

            Color colorT = Text.color;
            colorT.a += S;
            Text.color = colorT;
        }

    }
}
