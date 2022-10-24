using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    GameObject player;
    float timer;
    float maxTime;
    public bool bInRange;
    LineRenderer line;
    Transform shootPoint;
    public AudioClip ClipShoot;
    GameObject pointLight; // 광선 효과
    float fDistance = 10;
    void Start()
    {
        player = GameObject.Find("Player");
        pointLight = transform.Find("PointLight").gameObject;
        line = GetComponent<LineRenderer>();
        shootPoint = transform.Find("EnemyShootPoint");

        maxTime = 1.5f;

        pointLight.SetActive(false);
    }
    void Update()
    {
        
        timer += Time.deltaTime;
        if (timer >= maxTime && Vector3.Distance(transform.position, player.transform.position) < fDistance)
        {

            timer = 0;
            Shoot();
            Invoke("lineEnabledFalse", 0.5f);
            Invoke("lightEnabledFalse", 0.01f);
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player && transform.GetComponent<EnemyHealth>().hp > 0 && player.GetComponent<PlayerHealth>().hp > 0)
        {
            collision.gameObject.GetComponent<PlayerHealth>().Damage(10);
        }

    }
    void lineEnabledFalse()
    {
        line.enabled = false;
    }
    void lightEnabledFalse()
    {
        pointLight.SetActive(false);
    }
    void Shoot()
    {
        Ray ray = new Ray(shootPoint.position, shootPoint.forward);
        RaycastHit hit;
        GetComponent<AudioSource>().PlayOneShot(ClipShoot);
        if (Physics.Raycast(ray, out hit, fDistance, LayerMask.GetMask("Shootable")))
        {
            PlayerHealth e = hit.collider.GetComponent<PlayerHealth>();
            if (e != null)
            {
                if(player.GetComponent<PlayerHealth>().hp > 0){
                    e.Damage(50);
                }
                pointLight.SetActive(true);
                line.enabled = true;
                line.SetPosition(0, shootPoint.position);
                line.SetPosition(1, hit.point);
            }
        }
        else
        {
            pointLight.SetActive(true);
            line.enabled = true;
            line.SetPosition(0, shootPoint.position);
            line.SetPosition(1, shootPoint.position + ray.direction * fDistance);
        }
    }
}
