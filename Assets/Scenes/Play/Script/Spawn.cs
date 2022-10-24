using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    // 시간
    float timer;
    float timerDelayEnemy;

    // 적 변수
    public GameObject prefabEnemy;
    public Transform[] pointEnemy;
    public int cntEnemy;

    public int maxEnemy;
    public float timeDelayEnemy = 10;

    // 동물 변수
    public GameObject[] prefabAnimal;
    public int[] maxAnimal;
    public Transform[] pointAnimal;
    public int[] inlangePointAnimal;
    public int[] numPointAnimal;

    // 아이템 변수
    public GameObject prefabItem;
    public Transform[] pointItem;
    public int cntItem;
    public int maxItem = 15;
    public float timeDelayItem = 1;

    void Create()
    {
        if (cntEnemy >= maxEnemy)
        {
            return;
        }
        cntEnemy++;
        int i = Random.Range(0, pointEnemy.Length);
        Instantiate(prefabEnemy, pointEnemy[i]);
    }

    void CreateItem()
    {
        if (cntItem >= maxItem)
        {
            return;
        }
        cntItem++;
        int i = Random.Range(0, pointItem.Length);
        Instantiate(prefabItem, pointItem[i]);
    }

    void CreateAnimal()
    {
        int min;
        int max = 0;
        for(int i=0; i<prefabAnimal.Length; i++)
        {
            min = (i <= 0) ? 0 : numPointAnimal[i - 1];
            max += numPointAnimal[i];
            for (int j=0; j<= maxAnimal[i]; j++)
            {
                int k = Random.Range(min, max);
                Vector3 posAnimal = pointAnimal[k].position;
                posAnimal.x += Random.Range(inlangePointAnimal[i] * -1, inlangePointAnimal[i]);
                posAnimal.z += Random.Range(inlangePointAnimal[i] * -1, inlangePointAnimal[i]);
                Instantiate(prefabAnimal[i], posAnimal, Quaternion.identity);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxEnemy = 10;
        timeDelayEnemy = 5;

        InvokeRepeating("Create", 0, timeDelayEnemy);
        CreateAnimal();
        InvokeRepeating("CreateItem", 0, timeDelayItem);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerDelayEnemy += Time.deltaTime;
        if (timer >= 7)
        {
            timer = 0;
            maxEnemy += 1;
        }
        if (timerDelayEnemy >= 30)
        {
            timerDelayEnemy = 0;
            timeDelayEnemy -= timeDelayEnemy * 0.1f;
        }
    }
}
