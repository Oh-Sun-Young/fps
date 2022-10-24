using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    int num;

    // 레벨
    public static int lv;
    public static float exp;
    public static float maxExp;
    public AudioClip ClipLevelUp;

    // 경험치
    public int numHp;

    // 경험치
    GameObject player;
    public TextMeshProUGUI LvText;
    public TextMeshProUGUI noticeLvText;

    public Slider sliderHpHUD;
    public Slider sliderHpPlayer;
    public Slider sliderLv;

    float temp;
    void Start()
    {
        player = GameObject.Find("Player");
        lv = 1;
        exp = 0;
        maxExp = 1;
    }
    void Update()
    {
        if(exp >= maxExp)
        {
            LevelUp();
        }
        sliderLv.value = exp / maxExp * 100;
    }
    void ResetNotice()
    {
        noticeLvText.text = " ";
    }
    void NoticeLevel(int n)
    {
        noticeLvText.text = "Level Up!<br>";
        if (n == 0) noticeLvText.text = noticeLvText.text + "HP가 증가했습니다. (" + (int)temp + " > " + (int)PlayerHealth.maxHp + ")";
        if (n == 1) noticeLvText.text = noticeLvText.text + "속도가 증가했습니다. (" + (int)temp + " > " + (int)PlayerMove.speed + ")";
        if (n == 2) noticeLvText.text = noticeLvText.text + "공격력이 증가했습니다. (" + (int)temp + " > " + (int)PlayerAttack.AttackPower + ")";
        if (n == 3) noticeLvText.text = noticeLvText.text + "공격 범위가 증가했습니다. (" + (int)temp + " > " + (int)PlayerAttack.AttackRange + ")";
        if (n == 4) noticeLvText.text = noticeLvText.text + "총알 최대 갯수가 증가했습니다. (" + (int)temp + " > " + (int)PlayerAttack.maxBullet + ")";
        Invoke("ResetNotice", 5);
    }
    void LevelUp()
    {
        num = Random.Range(0, 5);
        /* 레벨 */
        lv++;
        LvText.text = (lv < 10 ? "0" : "") + lv;
        /* 경험치 */
        exp = 0;
        for (int i = 0; i < lv; i++)
        {
            maxExp = 1;
            maxExp += lv;
        }
        /* 능력치 > HP */
        if (num == 0)
        {
            temp = PlayerHealth.maxHp;
            PlayerHealth.maxHp += Random.Range(50, PlayerHealth.maxHp);
        }
        player.GetComponent<PlayerHealth>().SetHP();
        /* 능력치 > Speed */
        if (num == 1)
        {
            temp = PlayerMove.speed;
            PlayerMove.speed += PlayerMove.speed * Random.Range(0.01f, 0.25f);
        }
        /* 능력치 > Attack Power */
        if (num == 2)
        {
            temp = PlayerAttack.AttackPower;
            PlayerAttack.AttackPower += Random.Range(5, PlayerAttack.AttackPower);
        }
        /* 능력치 > Attack Range */
        if (num == 3)
        {
            temp = PlayerAttack.AttackRange;
            PlayerAttack.AttackRange += Random.Range(10, PlayerAttack.AttackRange);
        }
        /* 능력치 > Bullet Max Num */
        if (num == 4)
        {
            temp = PlayerAttack.maxBullet;
            PlayerAttack.maxBullet += Random.Range(1, PlayerAttack.maxBullet);
        }
        /* 시스템 HP */
        NoticeLevel(num);
        GetComponent<AudioSource>().PlayOneShot(ClipLevelUp);
    }
}
