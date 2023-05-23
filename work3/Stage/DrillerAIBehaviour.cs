using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillerAIBehaviour : MonoBehaviour
{
    public float enemySpeed;
    GameObject enemyGameObject;
    Transform enemyTransform;
    Vector3 enemyPos;
    Vector3 nowPos;
    RaycastHit hit;

    DrillerAIBehaviour drillerAIBehaviour;

    //ドリラーは壁に行き止まりになった雑魚敵がいるときに呼ばれる。真下方向に全部壊していく敵。準備ができたらわざと壁を作ってこいつを呼び、倒すのが目的

    void Start()
    {
        drillerAIBehaviour = this.gameObject.GetComponent<DrillerAIBehaviour>();
        enemyGameObject = this.gameObject;
        enemyTransform = enemyGameObject.transform;
        enemyPos = enemyTransform.position;
    }

    void Update()
    {
        nowPos = enemyPos;

        enemyPos.z -= enemySpeed * Time.deltaTime;
        enemyTransform.position = enemyPos;

        //下にあるobjectを壊していく
        if (Physics.Raycast(new Vector3(nowPos.x, -98.5f,nowPos.z), new Vector3(0, 0, -1), out hit, 3.5f) && hit.collider.tag == "Wall")
        {
            GameObject deleteObject = hit.collider.gameObject;
            Destroy(deleteObject);
        }
    }
}
