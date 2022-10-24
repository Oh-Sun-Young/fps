using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaSteal : MonoBehaviour
{
    public bool areaOwner; // true인 경우 Player 땅, false인 경우 Enemy 땅
    public string areaName;
    public GameObject areaPlayer;
    public GameObject areaEnemy;
    public TextMeshProUGUI notice;


    public AudioClip ClipStillArea;
    public AudioClip ClipFindArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && areaOwner)
        {
            areaOwner = false;
            notice.text = (notice.text != "" ? "" : notice.text + "<br>") + "기지의 " + areaName + " 구역을 뺏겼습니다.";
            notice.GetComponent<AudioSource>().PlayOneShot(ClipStillArea);
        }
        if (other.tag == "Player" && !areaOwner)
        {
            areaOwner = true;
            notice.text = (notice.text != "" ? "" : notice.text + "<br>") + "기지의 " + areaName + " 구역을 되찾았습니다.";
            notice.GetComponent<AudioSource>().PlayOneShot(ClipFindArea);
        }
        Invoke("NoticeReset", 5);
    }

    void NoticeReset()
    {
        notice.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        areaOwner = true; // 초반에는 Player 땅으로 세팅
    }

    // Update is called once per frame
    void Update()
    {
        if (areaOwner)
        {
            areaPlayer.SetActive(true);
            areaEnemy.SetActive(false);
        }
        else
        {
            areaPlayer.SetActive(false);
            areaEnemy.SetActive(true);
        }
    }
}
