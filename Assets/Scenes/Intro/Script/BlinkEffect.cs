using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    public GameObject target;

    public void Blink()
    {
        if (target.activeSelf)
        {
            target.SetActive(false);
        }
        else
        {
            target.SetActive(true);
        }
    }
    void Start()
    {
        InvokeRepeating("Blink", 0, 0.5f);
    }
}
