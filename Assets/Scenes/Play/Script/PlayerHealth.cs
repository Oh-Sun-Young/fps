using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public static float maxHp;

    public float hp;
    public static bool bHp;

    bool bDamage;

    public RawImage imgDamage;
    public RawImage imgBlood;

    public Slider sliderHpHUD;
    public Slider sliderHpPlayer;

    public TextMeshProUGUI txtHp;

    Vector3 posRespawn;

    public AudioClip ClipPlayerHurt; // 맞는 소리
    public AudioClip[] ClipPlayerDeath; // 신음 소리
    void Start()
    {
        maxHp = 250;
        SetHP();
        posRespawn = transform.position;
        imgBlood.GetComponent<RawImage>().enabled = false;
        imgBlood.transform.Find("BgBlood").gameObject.GetComponent<RawImage>().enabled = false;
    }
    void Update()
    {
        txtHp.text = (int)hp + " / " + (int)maxHp;

        if (hp <= 0)
        {
            hp = 0;
            bHp = false;
        }
        else
        {
            bHp = true;
        }
        if (bDamage)
        {
            imgDamage.color = new Color(1, 0, 0, 1);
        }
        else
        {
            imgDamage.color = Color.Lerp(imgDamage.color, Color.clear, 5 * Time.deltaTime);
        }
        bDamage = false;
    }
    public void SetHP()
    {
        /* hp 초기화 */
        hp = maxHp;
        /* hp slider 초기화 */
        sliderHpHUD.value = 100;
        sliderHpPlayer.value = 100;
    }
    public void Damage(float amount)
    {
        if (!bDamage)
        {
            /* 
             * hp가 없는 Player가 공격을 받았을 경우
             */
            if (hp <= 0) return;
            /* 
             * hp가 있는 Player가 공격을 받았을 경우
             */
            hp -= amount;
            bDamage = true;
            sliderHpHUD.value = hp / maxHp * 100;
            sliderHpPlayer.value = hp / maxHp * 100;
            GetComponent<AudioSource>().PlayOneShot(ClipPlayerHurt);
            /* 
             * Player가 사망에 이를 경우
             */
            if (hp <= 0)
            {
                int num = Random.Range(0, ClipPlayerDeath.Length);
                GetComponent<AudioSource>().PlayOneShot(ClipPlayerDeath[num]);
                imgBlood.GetComponent<RawImage>().enabled = true;
                imgBlood.transform.Find("BgBlood").gameObject.GetComponent<RawImage>().enabled = true;
                GetComponent<Animator>().SetTrigger("Death");
                GetComponent<PlayerMove>().enabled = false;
                GetComponent<PlayerAttack>().enabled = false;
                Invoke("Respawn", 3);
            }
        }
    }
    public void Respawn()
    {
        SetHP();
        transform.position = posRespawn;
        transform.rotation = Quaternion.LookRotation(Vector3.zero);
        GetComponent<Animator>().SetTrigger("Respawn");
        GetComponent<PlayerMove>().enabled = true;
        GetComponent<PlayerAttack>().enabled = true;
        imgBlood.GetComponent<RawImage>().enabled = false;
        imgBlood.transform.Find("BgBlood").gameObject.GetComponent<RawImage>().enabled = false;
    }
}
