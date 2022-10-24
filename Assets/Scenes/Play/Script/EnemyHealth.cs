using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public bool bHp;
    public static float maxHp;

    public float hp;
    public Slider imgBar;

    GameObject player;

    Rigidbody rigid;
    BoxCollider boxCollider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }
    void Start()
    {
        maxHp = 100;
        hp = maxHp;
        KinematicAbled();
    }
    void Update()
    {
        if (hp <= 0)
        {
            bHp = false;
        }
        else
        {
            bHp = true;
        }
    }
    public void EnemyDisabled()
    {
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<EnemyHealth>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyFollowMove>().enabled = false;
        GetComponent<LineRenderer>().enabled = false;
        transform.Find("PointLight").gameObject.SetActive(false);
    }
    void KinematicAbled()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
    void HurtEffectDisabled()
    {
        transform.Find("HurtEffect").gameObject.SetActive(false);
        transform.Find("HurtEffect").GetComponent<EffectSettings>().enabled = false;
    }
    public void Damage(int amount, Vector3 reactVec)
    {
        /* 
         * hp가 없는 Enemy가 공격을 받았을 경우
         */
        if (hp <= 0) return;
        /* 
         * hp가 있는 Enemy가 공격을 받았을 경우
         */
        hp -= amount;
        imgBar.value = hp / maxHp * 100;
        /* 폭발 효과 */
        transform.Find("HurtEffect").gameObject.SetActive(true);
        Invoke("HurtEffectDisabled", 0.5f);
        /* 물리적인 반응 효과 */
        GetComponent<Rigidbody>().isKinematic = false;
        reactVec = reactVec.normalized;
        reactVec += Vector3.up;
        rigid.AddForce(reactVec * -10, ForceMode.Impulse);
        Invoke("KinematicAbled", 1);
        /* 
         * Enemy가 사망에 이를 경우
         */
        if (hp <= 0)
        {
            /* Enemy Object 파괴
             * 자식 Object 중 DroneGuard가 뼈대를 구성하고 있어서 Constraints 설정 해제로만 적용 X */
            Destroy(transform.Find("DroneGuard").gameObject);
            transform.Find("Down").gameObject.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)0;
            transform.Find("Left_Arm").gameObject.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)0;
            transform.Find("Right_Arm").gameObject.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)0;
            transform.Find("Up").gameObject.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)0;
            /* 애니메이션 */
            GetComponent<Animator>().SetTrigger("Death");
            /* HPBar 비활성화 */
            transform.Find("HPBar").gameObject.SetActive(false);
            /* 미니맵에 보이는 핀 비활성화 */
            transform.Find("PosEnemy").gameObject.SetActive(false);
            /* 미니맵에 보이는 핀 비활성화 */
            gameObject.layer = 9;
            /* Enemy 안의 Component 비활성화 */
            EnemyDisabled();
            /* Enemy Object 삭제 */
            Destroy(gameObject, 1.5f);
            /* 
             * 기록
             */
            /* 경험치 */
            Level.exp += 1;
            /* Enemy 죽인 수 */
            Result.cntDeathEnemy++;
            /* 현재 Enemy 갯수 */
            GameObject.Find("GameManager").GetComponent<Spawn>().cntEnemy--;
        }
    }
}
