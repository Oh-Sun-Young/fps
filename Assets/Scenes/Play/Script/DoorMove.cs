using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    /*
        OnCollisionEnter(Collision collision)
        OnCollisionStay(Collision collision)
        OnCollisionExit(Collision collision)

        OnTriggerEnter(Collider other)
        OnTriggerStay(Collider other)
        OnTriggerExit(Collider other)
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            transform.GetComponent<Animator>().SetBool("bMove", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            transform.GetComponent<Animator>().SetBool("bMove", false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
