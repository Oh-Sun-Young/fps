using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public AudioClip ClipBreak; // 맞은 소리
    void Update()
    {
        Vector3 bulletDest = Vector3.forward * PlayerAttack.AttackRange;
        if (transform.position.x == bulletDest.x && transform.position.z == bulletDest.z)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 100);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && other.gameObject.tag != "Player")
        {
            if (other.gameObject.tag == "Enemy")
            {
                EnemyHealth e = other.gameObject.GetComponent<EnemyHealth>();
                Vector3 reactVec = other.transform.position - transform.position;
                if (e.bHp && PlayerHealth.bHp)
                {
                    e.Damage(PlayerAttack.AttackPower, reactVec);
                    Score.score += 10;
                }
            }
            GetComponent<AudioSource>().PlayOneShot(ClipBreak);
            GetComponent<BoxCollider>().enabled = false;
            Destroy(gameObject, 0.1f);
        }
    }
}
