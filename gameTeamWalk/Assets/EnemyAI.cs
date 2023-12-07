using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    //�þ߰�
    [SerializeField] float e_angle = 0f;
    //�󸶳� �������ִ���
    [SerializeField] float e_distance = 0f;
    //player���̾� ����� ���̾��ũ player�� �����س����մϴ�.
    [SerializeField] LayerMask e_layerMask = 0;
    Camera m_Camera; //LookAt ���� �� ���� �� ���� �߰��߽��ϴ�

    AudioSource _source;
    [SerializeField]
    AudioClip _clip;
    [SerializeField]
    AudioClip _clip2;

    bool isInSight()    //�þ߿� �����ִ��� üũ by �þ߰�, ����Ʈ���̽�(���̿� ���ִ���), �þ߰Ÿ�
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, e_distance, e_layerMask);

        if (t_cols.Length > 0)
        {
            //�÷��̾� transform
            Transform t_tfPlayer = t_cols[0].transform;

            Vector3 t_direction = (t_tfPlayer.position - transform.position).normalized;

            //transform.forward�� ���� z���� �ǹ�. z��������� �þ߰��̶� �þ߰Ÿ�¥�����.
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
    int wp_count = 0; // ���° wayPoint����.

    [SerializeField] Transform player; //�÷��̾� ��ġ
    [SerializeField] float chaseDistance; //�ִ� �����Ÿ�.. �̰� �Ѿ�� ���������� ���ư�.
    int isChasing = 0; // ���� ���������� ���� 0 = �ƴ�, 1 = ������, 2 = �����ϴٰ� ��ħ(�ֺ��� �ִٰ� ����)

    [SerializeField] float patrolSpeed = 3.5f; // ���� �ӵ�
    [SerializeField] float chaseSpeed = 5f; // ���� �ӵ�

    

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0) // Waypoint�� ������ ����
            return;
        // ���� Waypoint�� �̵�
        enemy.destination = waypoints[wp_count].position;

        // ���� Waypoint �ε����� ����
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
        
        //�÷��̾���� �Ÿ�
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

        if (isInSight()) // �þ߳��� ������ �÷��̾ ����
        {
            enemy.destination = player.position;
            isChasing = 1;
            enemy.speed = chaseSpeed;

            StartCoroutine(ShakeDelay(1f));
        }
        else if (isChasing == 1 || isChasing == 2) //�þ߿��� �������� �÷��̾ ������ �ϰ��ִ»���
        {
            isChasing = 2;
            enemy.destination = player.position;
            enemy.speed = patrolSpeed;
            StartCoroutine(ShakeDelay(0.25f));
        }
        else if ((isChasing == 2 && distanceToPlayer > chaseDistance)) // ���� ���ε� ���� �Ÿ� �̻� �������ų� �þ߿��� ����� ������ ���ƿ�
        {
            isChasing = 0;
            enemy.speed = patrolSpeed;
            StartCoroutine(ShakeDelay(0.01f));  
            GoToNextWaypoint();
        }

        if (!enemy.pathPending && enemy.remainingDistance < 0.5f) // Waypoint�� �����ϸ� ���� Waypoint�� �̵�
        {
            GoToNextWaypoint();
        }

        if (distanceToPlayer < 6) 
        {
            SceneManager.LoadScene("End");
        }
    }
}