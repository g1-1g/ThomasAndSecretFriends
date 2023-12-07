using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInMapController : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    [SerializeField]
    GameObject _cam;

    Vector3 dir = new Vector3(0, 0, 0);
    void Start()
    {
        transform.position = _player.transform.position + new Vector3(0, 68, 0);
    }

    void Update()
    {
        dir = _cam.transform.position - _player.transform.position; //플레이어 -> 미니맵 카메라 
        transform.position =_player.transform.position + dir * 0.4f;
    }
}
