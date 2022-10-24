using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    public static float speed = 10;
    public float angle = 1;
    public float hAxis;
    public float vAxis;
    public float hAxisCross;
    public float vAxisCross;
    public int mode;

    bool bBorder; // 벽이나 적과 충돌했는 지 확인
    public bool bNotice; // 벽이나 적과 충돌했는 지 확인

    GameObject mainCamera;
    public GameObject bgHub;
    Rigidbody rigid;
    public TextMeshProUGUI notice;
    public AudioClip ClipNotice;

    void Move(int mode)
    {
        if (mode == 0)
        {
            /* 
             * 3인칭 시점
             */
            bgHub.SetActive(false);
            if (hAxis != 0 || vAxis != 0 || hAxisCross != 0 || vAxisCross != 0)
            {
                Vector3 dir;
                if (hAxis != 0 || vAxis != 0)
                {
                    dir = hAxis * Vector3.right + vAxis * Vector3.forward;
                    transform.rotation = Quaternion.LookRotation(dir);
                }
                if (hAxisCross != 0 || vAxisCross != 0)
                {
                    dir = hAxisCross * Vector3.right + vAxisCross * Vector3.forward;
                    transform.rotation = Quaternion.LookRotation(dir);
                }
                if (!bBorder ||
                    bBorder && (hAxis < 0 ||vAxis < 0|| hAxisCross < 0 || vAxisCross < 0))
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                }
                transform.GetComponent<Animator>().SetBool("bMove", true);
            }
            else
            {
                transform.GetComponent<Animator>().SetBool("bMove", false);
            }
        }
        else
        {
            /* 
             * 1인칭 시점
             */
            bgHub.SetActive(true);
            if (hAxis != 0 || vAxis != 0)
            {
                transform.Rotate(hAxis * Vector3.up * Time.deltaTime * angle);
                if (!bBorder ||
                    bBorder && vAxis < 0)
                {
                    transform.Translate(vAxis * Vector3.forward * Time.deltaTime * speed);
                }
            }
            if (hAxisCross != 0 ||vAxisCross != 0)
            {
                transform.Rotate(hAxisCross * Vector3.up * Time.deltaTime * angle);
                if (!bBorder ||
                    bBorder && vAxisCross < 0)
                {
                    transform.Translate(vAxisCross * Vector3.forward * Time.deltaTime * speed);
                }
            }
            if (vAxis != 0 || vAxisCross != 0) transform.GetComponent<Animator>().SetBool("bMove", true);
            else transform.GetComponent<Animator>().SetBool("bMove", false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        rigid = transform.GetComponent<Rigidbody>();
        bNotice = false;
    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        hAxisCross = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        vAxisCross = CrossPlatformInputManager.GetAxisRaw("Vertical");
        mode = mainCamera.GetComponent<FollowCameraMove>().cameraMode;
        Move(mode);
    }

    void ResetWall()
    {
        notice.text = "";
        bNotice = false;
    }
    // 물리 충돌 있어났을 경우 자동으로 회전하는 것을 방지
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }
    // 벽이나 적을 뚫는 것을 방지
    void StopToObject()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 2, Color.green);// DrawRay : Scene 내에서 Ray를 보여주는 함수
        bBorder = Physics.Raycast(ray, out hit, 2, LayerMask.GetMask("Shootable"));
        if (bBorder)
        {
            if (!bNotice && hit.collider.CompareTag("Wall"))
            {
                bNotice = true;
                notice.text = "더 이상 앞으로 나갈 수 없습니다.";
                notice.GetComponent<AudioSource>().PlayOneShot(ClipNotice);
                Invoke("ResetWall", 5);
            }
        }
    }
    void FixedUpdate()
    {
        FreezeRotation();
        StopToObject();
    }
}
