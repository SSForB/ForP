using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontserSummonDark : MonoBehaviour
{

    GameObject enemyGameObject;
    CommonAIBehaviour commonAIBehaviour;

    GameObject monsterObjectStore;

    int monsterCount;
    int monsterListNum;
    int monsterLimit;
    float monsterInterval;

    Vector3 enemyScale;
    void Start()
    {
        enemyGameObject = GameObject.Find("Monster");
        monsterObjectStore = GameObject.Find("ForMonsterObject");
        enemyScale = enemyGameObject.transform.localScale;
        StartCoroutine("wave1");
    }


    void Update()
    {
        
    }

    IEnumerator wave1()
    {
        //wave1
        monsterListNum = 1; //モンスターID
        monsterCount = 5; //召喚する数
        monsterInterval = 3.0f; //召喚する間隔
        yield return StartCoroutine("summon"); //指定したモンスターの召喚用メソッド

        //次の召喚までのインターバル
        yield return new WaitForSeconds(100.0f);

        //wave2
        monsterListNum = 2;
        monsterCount = 1;
        monsterInterval = 2.0f;
        yield return StartCoroutine("summon");

        //wave2に移行
        yield return StartCoroutine("wave2");
        yield break;
    }

    IEnumerator wave2()
    {
        yield break;
    }

    IEnumerator summon()
    {
        monsterLimit = monsterCount;
        //モンスターを召喚し、位置、向き等を整える
        for (monsterCount = 0; monsterCount < monsterLimit; monsterCount++)
        {
            commonAIBehaviour = new CommonAIBehaviour();
            enemyGameObject = Instantiate(enemyGameObject);
            commonAIBehaviour = enemyGameObject.GetComponent<CommonAIBehaviour>();
            commonAIBehaviour.monsterListNum = monsterListNum;
            enemyGameObject.transform.parent = monsterObjectStore.transform;
            enemyGameObject.transform.localScale = enemyScale;
            enemyGameObject.transform.Rotate(90, 0, 0);
            enemyGameObject.transform.localPosition = new Vector3(0, 900, -60);
            yield return new WaitForSeconds(monsterInterval);
        }
    }
}
