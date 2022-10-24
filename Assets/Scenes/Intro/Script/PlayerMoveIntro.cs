using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveIntro : MonoBehaviour
{
    bool bMove;
    // Start is called before the first frame update
    void Start()
    {
        bMove = true;
        GameObject.Find("Player").GetComponent<Animator>().SetBool("bMove", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "door")
        {
            GameObject.Find("doorForward").GetComponent<Animator>().SetBool("bMove",true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "stop")
        {
            GameObject.Find("Player").GetComponent<Animator>().SetBool("bMove", false);
            bMove = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "door")
        {
            GameObject.Find("doorForward").GetComponent<Animator>().SetBool("bMove", false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (bMove)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 1);
        }
    }
}
