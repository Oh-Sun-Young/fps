using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalMove : MonoBehaviour
{
    public int strong;
    public int disMin;
    public int disMax;
    public float time;
    public float minInRange = -5;
    public float maxInRange = 5;
    public float minTimeInRange = 3;
    public float maxTimeInRange;

    public float dis;
    public float vInRange;
    public float hInRange;
    public float timeInRange = 10;


    public int numIdleType;

    bool bMove;


    Transform target;
    Vector3 posReturn;
    Vector3 posDis;
    public Vector3 posTemp;

    NavMeshAgent nav;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        posReturn = transform.position;
        posDis = transform.position;
        posTemp = transform.position;
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        ani.SetInteger("idleType", Random.Range(0, numIdleType));
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector3.Distance(transform.position, target.position);
        /* 이동 */
        if (nav.isOnNavMesh)
        {
            bMove = true;
            if (dis < disMin && strong > 0)
            {
                nav.SetDestination(transform.position);
                if(transform.position == posTemp) bMove = false;

            }
            else if (dis < disMax && strong > 0)
            {
                nav.SetDestination(target.position);
            }
            else if (dis < disMax && strong < 0)
            {
                hInRange = transform.position.x + (transform.position.x - target.position.x);
                vInRange = transform.position.z + (transform.position.z - target.position.z);
                posDis = new Vector3(hInRange, 0, vInRange);
                nav.SetDestination(posDis);
            }
            else
            {
                if (transform.position.x == posDis.x && transform.position.z == posDis.z || time >= timeInRange || transform.position == posTemp)
                {
                    timeInRange = Random.Range(minTimeInRange, maxTimeInRange);
                    hInRange = Random.Range(posReturn.x + minInRange, posReturn.x + maxInRange);
                    vInRange = Random.Range(posReturn.z + minInRange, posReturn.z + maxInRange);
                }
                time += Time.deltaTime;
                if (time >= timeInRange)
                {
                    time = 0;
                    posDis = new Vector3(hInRange, 0, vInRange);
                }
                nav.SetDestination(posDis);
            }
            posTemp = transform.position;
        }
        /* 애니메이션 */
        if (transform.position.x == posDis.x && transform.position.z == posDis.z || !bMove)
        {
            ani.SetBool("bMove", false);
            ani.SetInteger("idleType", Random.Range(0, numIdleType));
        }
        else
        {
            ani.SetBool("bMove", true);
        }
    }
}
