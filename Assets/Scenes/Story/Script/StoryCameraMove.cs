using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCameraMove : MonoBehaviour
{
    public Transform target;
    public float orbitSpeed;
    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
