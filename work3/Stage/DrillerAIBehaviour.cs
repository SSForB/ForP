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

    //�h�����[�͕ǂɍs���~�܂�ɂȂ����G���G������Ƃ��ɌĂ΂��B�^�������ɑS���󂵂Ă����G�B�������ł�����킴�ƕǂ�����Ă������ĂсA�|���̂��ړI

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

        //���ɂ���object���󂵂Ă���
        if (Physics.Raycast(new Vector3(nowPos.x, -98.5f,nowPos.z), new Vector3(0, 0, -1), out hit, 3.5f) && hit.collider.tag == "Wall")
        {
            GameObject deleteObject = hit.collider.gameObject;
            Destroy(deleteObject);
        }
    }
}
