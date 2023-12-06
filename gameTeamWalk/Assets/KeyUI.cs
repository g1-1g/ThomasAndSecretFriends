using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    [SerializeField]
    private Text countText;

    private Open _open;
    private Pick _pick;
    void Start()
    {
        countText = GetComponent<Text>();
        _open = FindObjectOfType<Open>();
        _pick = FindObjectOfType<Pick>();
        countText.text = "KEY :" + _pick.count + "/" + _open.KeyClear;
    }

    void Update()
    {
        countText.text = "KEY : " + _pick.count +" / " + _open.KeyClear;
    }
}
