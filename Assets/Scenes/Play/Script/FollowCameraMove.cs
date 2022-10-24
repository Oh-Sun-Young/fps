using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class FollowCameraMove : MonoBehaviour
{
    Transform player;
    public int cameraMode;
    public RawImage Crosshairs;
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        if (Input.GetButtonDown("Mode") || CrossPlatformInputManager.GetButtonUp("Mode"))
        {
            cameraMode++;
            if (cameraMode == 2) cameraMode = 0;
        }
        CamerMode();
    }

    void CamerMode()
    {
        if (cameraMode == 0)
        {
            /* 
             * 3인칭 시점
             */
            transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.position = player.position + new Vector3(0, 2f, -4.5f);
            Crosshairs.GetComponent<RawImage>().enabled = false;
        }
        else
        {
            /* 
             * 1인칭 시점
             */
            transform.rotation = player.rotation;
            transform.position = player.position;
            transform.Translate(Vector3.up * 2f + Vector3.forward * 0.7f);
            Crosshairs.GetComponent<RawImage>().enabled = true;
        }
    }
}
