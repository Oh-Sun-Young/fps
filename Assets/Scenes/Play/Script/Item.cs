using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item : MonoBehaviour
{
    public AudioClip ClipReload;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            if (PlayerAttack.cntBullet < PlayerAttack.maxBullet)
            {
                GetComponent<AudioSource>().PlayOneShot(ClipReload);
                PlayerAttack.cntBullet = PlayerAttack.cntBullet + Random.Range(1, PlayerAttack.maxBullet - PlayerAttack.cntBullet);
                GameObject.Find("GameManager").GetComponent<Spawn>().cntItem--;
                Destroy(other.gameObject);
            }
        }
    }
}
