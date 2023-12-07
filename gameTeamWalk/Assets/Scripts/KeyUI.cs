using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    [SerializeField]
    private Text countText;

    [SerializeField]
    private TextMeshProUGUI countTexPro;
  

    private Open _open;
    private Pick _pick;
    void Start()
    {
       // countText = GetComponent<Text>();
        countTexPro = GetComponent<TextMeshProUGUI>();
        _open = FindObjectOfType<Open>();
        _pick = FindObjectOfType<Pick>();
        countTexPro.text = "KEY :" + _pick.count + "/" + _open.KeyClear;
    }

    void Update()
    {
        countTexPro.text = "KEY : " + _pick.count +" / " + _open.KeyClear;
    }
}
