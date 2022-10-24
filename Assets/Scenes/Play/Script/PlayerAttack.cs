using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    float timer;
    Transform shootPoint;

    public static int AttackPower; // 공격력
    public static int AttackRange; // 공격 범위
    public static int maxBullet; // 최대 총알 갯수
    public static int cntBullet; // 현재 총알 갯수

    public GameObject bulletPrefab; // 총알 원본
    public GameObject bulletPos; // 총알 위치

    public TextMeshProUGUI numBullet; // 총알 갯수 표기
    public TextMeshProUGUI emptBullet; // 총알 갯수 표기 (Blur 효과 줄 버튼)

    public AudioClip ClipShoot; // 총 쏜 소리2

    // Start is called before the first frame update
    void Start()
    {
        shootPoint = transform.Find("Shootpoint");
        AttackPower = 50;
        AttackRange = 15;
        maxBullet = 30;
        cntBullet = maxBullet;
    }

    // Update is called once per frame
    void Update()
    {
        /* 공격 */
        timer += Time.deltaTime;
        if(Input.GetButtonDown("Attack") || CrossPlatformInputManager.GetButtonUp("Attack"))
        {
            if (timer >= 0.5f && 0 < cntBullet)
            {
                timer = 0;
                Shoot();
            }
        }
        /* 총알 갯수 정보 */
        numBullet.text = cntBullet +" / " + maxBullet;
        emptBullet.text = cntBullet + " / " + maxBullet;
    }

    void Shoot()
    {
        //Ray ray = new Ray(shootPoint.position, shootPoint.forward);
        //RaycastHit hit;
        Instantiate(bulletPrefab, bulletPos.transform.position, transform.rotation);
        cntBullet--;
        GetComponent<AudioSource>().PlayOneShot(ClipShoot);
        GetComponent<Animator>().SetTrigger("Shoot");
    }
}
