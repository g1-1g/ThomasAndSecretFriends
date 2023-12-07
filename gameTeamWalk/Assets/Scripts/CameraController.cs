using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject _player; // 직렬화, 유니티 리플렉션 기술의 일종으로, 런타임 중에 객체에 대한 정보를 얻을 수 있는 기술, 언리얼에선 이런거 못한다.
                      // private을 유지해 객체 지향 은닉성을 유지한 상태에서 에디터에서 접근할 수는 있도록 만듦 -> 개발의 용이성을 위해 
    
    void Start()
    {
        transform.rotation = Quaternion.Euler(30, 24, 0); // 다음주에 배울 회전 성분에 대한 내용, 카메라의 고개 각도 초기화 위해 사용  
    }

    void Update()
    {
        transform.position = _player.transform.position+new Vector3(-4.0f,5.0f,-5.0f); 
        // 이 부분 쉬운데 아까 오류난건 transform 타입을 position과 매칭하려 해서 오류남  

    }
}
