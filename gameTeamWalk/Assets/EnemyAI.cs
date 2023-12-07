using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    //시야각
    [SerializeField] float e_angle = 0f;
    //얼마나 떨어져있는지
    [SerializeField] float e_distance = 0f;
    //player레이어 만들고 레이어마스크 player로 지정해놔야합니다.
    [SerializeField] LayerMask e_layerMask = 0;
    Camera m_Camera; //LookAt 쓰는 게 나을 거 같아 추가했습니당

    AudioSource _source;
    [SerializeField]
    AudioClip _clip;
    [SerializeField]
    AudioClip _clip2;

    bool isInSight()    //시야에 들어와있는지 체크 by 시야각, 레이트레이싱(사이에 벽있는지), 시야거리
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, e_distance, e_layerMask);

        if (t_cols.Length > 0)
        {
            //플레이어 transform
            Transform t_tfPlayer = t_cols[0].transform;

            Vector3 t_direction = (t_tfPlayer.position - transform.position).normalized;

            //transform.forward는 모델의 z축을 의미. z축기준으로 시야각이랑 시야거리짜놨어요.
            float t_angle = Vector3.Angle(t_direction, transform.forward);


            if (t_angle < e_angle * 0.5f)
            {
                if (Physics.Raycast(transform.position, t_direction, out RaycastHit t_hit, e_distance))
                {
                    if (t_hit.transform.name == "Player")
                    {
                        return true;
                    } else return false;
                } else return false;
            } else return false;
        } else return false;
    }



    NavMeshAgent enemy = null; // NavMesh Agent
    [SerializeField] Transform[] waypoints = null; // Waypoints
    int wp_count = 0; // 몇번째 wayPoint인지.

    [SerializeField] Transform player; //플레이어 위치
    [SerializeField] float chaseDistance; //최대 추적거리.. 이거 넘어가면 기존순찰로 돌아감.
    int isChasing = 0; // 지금 추적중인지 여부 0 = 아님, 1 = 추적중, 2 = 추적하다가 놓침(주변에 있다고 인지)

    [SerializeField] float patrolSpeed = 3.5f; // 순찰 속도
    [SerializeField] float chaseSpeed = 5f; // 추적 속도

    

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0) // Waypoint가 없으면 리턴
            return;
        // 다음 Waypoint로 이동
        enemy.destination = waypoints[wp_count].position;

        // 다음 Waypoint 인덱스로 변경
        wp_count = (wp_count + 1) % waypoints.Length;
    }

    void Start()
    {

        _source = GetComponent<AudioSource>();
        enemy = GetComponent<NavMeshAgent>();
        enemy.speed = patrolSpeed;
        m_Camera = player.GetComponentInChildren<Camera>();
    }

    IEnumerator ShakeDelay(float volume)
    {
        float shakeRange = 0.1f * volume;
        Vector3 shakeAmount = new Vector3(Random.Range(-shakeRange, shakeRange)
                                           ,Random.Range(-shakeRange, shakeRange)
                                           ,Random.Range(-shakeRange, shakeRange));
        yield return new WaitForSeconds(0.1f);
    }



    void Update()
    {
        
        //플레이어와의 거리
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        transform.LookAt(m_Camera.transform.position);

        if (isChasing==0) 
        {
            if (_source.clip != _clip)
            {
                _source.clip = _clip;
                _source.Play();
            }
            
        }
        else 
        {
            if (_source.clip != _clip2)
            {
                _source.clip = _clip2;
                _source.Play();
            }
        }

        if (isInSight()) // 시야내에 있으면 플레이어를 추적
        {
            enemy.destination = player.position;
            isChasing = 1;
            enemy.speed = chaseSpeed;

            StartCoroutine(ShakeDelay(1f));
        }
        else if (isChasing == 1 || isChasing == 2) //시야에서 놓쳤으나 플레이어를 인지는 하고있는상태
        {
            isChasing = 2;
            enemy.destination = player.position;
            enemy.speed = patrolSpeed;
            StartCoroutine(ShakeDelay(0.25f));
        }
        else if ((isChasing == 2 && distanceToPlayer > chaseDistance)) // 추적 중인데 일정 거리 이상 도망가거나 시야에서 벗어나면 순찰로 돌아옴
        {
            isChasing = 0;
            enemy.speed = patrolSpeed;
            StartCoroutine(ShakeDelay(0.01f));  
            GoToNextWaypoint();
        }

        if (!enemy.pathPending && enemy.remainingDistance < 0.5f) // Waypoint에 도착하면 다음 Waypoint로 이동
        {
            GoToNextWaypoint();
        }

        if (distanceToPlayer < 6) 
        {
            SceneManager.LoadScene("End");
        }
    }
}