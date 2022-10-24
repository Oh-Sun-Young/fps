using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogMove : MonoBehaviour
{
    public float disMax;
    public float disMin;

    Vector3 posReturn;
    Transform target;
    NavMeshAgent nav;
    Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        posReturn = transform.position;
        target = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(transform.position, target.position);
        if (nav.isOnNavMesh)
        {
            if(dis > disMax)
            {
                nav.SetDestination(posReturn);
                if(Vector3.Distance(transform.position, posReturn) < 3f)
                {
                    ani.SetBool("bMove", false);
                    ani.SetInteger("idleType", Random.Range(0, 3));
                }
                else
                {
                    ani.SetBool("bMove", true);
                }
            }
            else if (dis > disMin)
            {
                nav.SetDestination(target.position);
                ani.SetBool("bMove", true);
            }
            else
            {
                nav.SetDestination(transform.position);
                ani.SetBool("bMove", false);
                ani.SetInteger("idleType", Random.Range(0, 3));
            }
                
        }
    }
}
