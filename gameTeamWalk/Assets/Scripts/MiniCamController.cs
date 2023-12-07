using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCamController : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    [SerializeField]
    Vector3 _delta=new Vector3(0,210,37.3f);

    void Start()
    {
        transform.position = _player.transform.position + _delta;
    }

    void Update()
    {
     //   transform.position = _player.transform.position+_delta;
    }
}
