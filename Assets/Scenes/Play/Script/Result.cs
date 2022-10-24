using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    bool bResultCheck;
    float timer;
    float timerTotal; // 총 게임 시간
    int pinPlayer;
    public static int pinEnemy;
    public static int cntDeathEnemy;

    public GameObject[] Area; // 구역
    public GameObject Player; // Player
    public GameObject GameManager; // 게임매니저
    public GameObject GameOverPopup; // 게임오버창
    public GameObject ResultPopup; // 결과창
    public GameObject WarningPopup; // 경고창
    public GameObject BlurPopup; // 
    public GameObject BtnHub; // 조작 버튼
    public GameObject EmptBtnHub; // 임시 조작 버튼()
    public GameObject BgSound; // 배경음악
    public AudioClip ClipWarning; // 경고음
    public AudioClip ClipGameOver; // 게임오버음
    public TextMeshProUGUI cntPinPlayer; // 실시간 Player 구역 갯수
    public TextMeshProUGUI cntPinEnemy; // 실시간 Enemy 구역 갯수
    public TextMeshProUGUI totalTime; // 시간
    public TextMeshProUGUI infoResult; // 시간 외 정보

    void GameStop()
    {
        Player.GetComponent<PlayerMove>().enabled = false;
        Player.GetComponent<PlayerAttack>().enabled = false;
        Player.GetComponent<PlayerHealth>().enabled = false;
        Player.GetComponent<Item>().enabled = false;
        Player.GetComponent<Level>().enabled = false;
        GameManager.GetComponent<Spawn>().enabled = false;
    }

    void GameOver()
    {
        GameOverPopup.SetActive(true);
        BlurPopup.SetActive(true);
        ResultPopup.SetActive(false);
        WarningPopup.SetActive(false);
        BtnHub.SetActive(false);
        EmptBtnHub.SetActive(true);
        GameStop();
        GameObject.Find("PopupHub").GetComponent<Canvas>().sortingOrder = 9999;
        GetComponent<AudioSource>().Pause();
        GetComponent<AudioSource>().PlayOneShot(ClipGameOver);
    }

    void GameResult()
    {
        ResultPopup.SetActive(true);
        BlurPopup.SetActive(true);
        GameOverPopup.SetActive(false);
        WarningPopup.SetActive(false);
        BtnHub.SetActive(false);
        EmptBtnHub.SetActive(true);
        GameStop();
        GameObject.Find("PopupHub").GetComponent<Canvas>().sortingOrder = 9999;

        // 시간
        int fh = 0, fm = 0, fs = (int)timerTotal;
        if(fs >= 60)
        {
            fm = (int)(timerTotal / 60);
            fs = (int)(timerTotal % 60);
            if (fm >= 60)
            {
                fh = (int)(fm / 60);
                fm %= 60;
            }
        }
        totalTime.text = "<b>" + (fh < 10 ? "0" + fh : fh) + "</b>:<b>";
        totalTime.text = totalTime.text + (fm < 10 ? "0" + fm : fm) + "</b>:<b>";
        totalTime.text = totalTime.text + (fs < 10 ? "0" + fs : fs) + "</b>";

        // 시간 외 정보
        infoResult.text = "Level : <b>" + Level.lv + "</b>   Score : <b>" + Score.score + "</b>   kill : <b>" + cntDeathEnemy + "</b>";
    }

    void GameWarning()
    {
        if (WarningPopup.activeSelf)
        {
            WarningPopup.SetActive(false);
        }
        else
        {
            WarningPopup.SetActive(true);
        }
        GetComponent<AudioSource>().Pause();
        GetComponent<AudioSource>().PlayOneShot(ClipWarning);
    }

    // Start is called before the first frame update
    void Start()
    {
        bResultCheck = false;
        GameOverPopup.SetActive(false);
        ResultPopup.SetActive(false);
        BlurPopup.SetActive(false);
        WarningPopup.SetActive(false);
        BtnHub.SetActive(true);
        EmptBtnHub.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pinPlayer = 0;
        pinEnemy = 0;
        for (int i = 0; i < Area.Length; i++)
        {
            if (Area[i].GetComponent<AreaSteal>().areaOwner == true) pinPlayer++;
            else pinEnemy++;
        }
        cntPinPlayer.text = "0" + pinPlayer;
        cntPinEnemy.text = "0" + pinEnemy;
        if (pinEnemy == Area.Length)
        {
            /* 적이 기지 전체를 장악했을 경우 */
            timer += Time.deltaTime;
            if (!bResultCheck)
            {
                bResultCheck = true;
                GameOver();
            }
            else if (timer > 5)
            {
                GameResult();
            }
        }
        else
        {
            /* 적이 기지 전체를 장악하지 못한 경우 */
            timerTotal += Time.deltaTime;
            if (pinEnemy == Area.Length - 1)
            {
                /* 적이 1 영역만 제외하고 징악한 경우 */
                timer += Time.deltaTime;
                if (timer > 0.5f)
                {
                    timer = 0;
                    GameWarning();
                }
            }
            else
            {
                /* 적이 0 ~ 3구역을 장악한 경우 */
                WarningPopup.SetActive(false);
                GetComponent<AudioSource>().Pause();
            }
        }
    }
}