using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowMove : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;
    Animator ani;
    public float hp;
    public float distance;
    public float time;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        hp = GetComponent<EnemyHealth>().hp;
        distance = Vector3.Distance(player.position, transform.position);
        if (distance <= 30 && hp > 0)
        {
            ani.SetBool("bMove", true);
            Invoke("enemyMoveOn", 0);
        }
        else
        {
            nav.enabled = false;
            ani.SetBool("bMove", false);
        }
    }
    void enemyMoveOn()
    {
        nav.enabled = true;
        if (nav.isOnNavMesh)
        {
            nav.SetDestination(player.position);
        }
    }
}
