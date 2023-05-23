using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject[] enemyObject = new GameObject[1];

    public string playerName;
    public float playerRange;
    public float playerExtentKind;
    public float playerExtentRad;
    public float playerAttackSpeed;
    public float playerAttackDamage;

    public float[] distance = new float[1];

    int targetSystem = 0; //0=自身から一番近い 1=

    float progressTime = 0;

    public List<GameObject> listEnemy = new List<GameObject>();
    public List<float> listDistance = new List<float>();

    string dbPath = "Assets/Scripts/DataBase/Player.asset";

    Vector3 targetVec;
    Vector3 checkVec;

    PlayerParam playerParam;

    SphereCollider attackRangeCollider;
    CommonAIBehaviour commonAIBehaviour;

    int rnd;
    int arrayNum;

    void Start()
    {
        //プレイヤーステータス取得
        playerParam = AssetDatabase.LoadAssetAtPath<PlayerParam>(dbPath);
        rnd = UnityEngine.Random.Range(0, playerParam.PlayerDBList.Count);
        playerName = playerParam.PlayerDBList[rnd].playerName;
        playerRange = playerParam.PlayerDBList[rnd].playerRange;
        playerExtentKind = playerParam.PlayerDBList[rnd].playerExtentKind;
        playerExtentRad = playerParam.PlayerDBList[rnd].playerExtentRad;
    　　playerAttackSpeed = playerParam.PlayerDBList[rnd].playerAttackSpeed;
        playerAttackDamage = playerParam.PlayerDBList[rnd].playerAttackDamage;

        attackRangeCollider = transform.GetChild(0).gameObject.GetComponent<SphereCollider>();
        attackRangeCollider.radius = playerRange;

        listEnemy = enemyObject.ToList();
        listDistance = distance.ToList();
    }

    void Update()
    {
        if(targetSystem == 0)
        {
            NearTarget();
        }
        progressTime += Time.deltaTime; //前の攻撃からの時間取得

        if (playerExtentKind == 0) //通常単発攻撃
        {
            //ターゲットシステムで指定された敵に対して攻撃する
            if (progressTime >= playerAttackSpeed && enemyObject[arrayNum] != null)
            {
                commonAIBehaviour = enemyObject[arrayNum].GetComponent<CommonAIBehaviour>();
                commonAIBehaviour.DamageDisplay(playerAttackDamage);
                commonAIBehaviour.monsterHP -= playerAttackDamage;
                progressTime = 0;
            }
        }

        if(playerExtentKind == 1) //自身の周り範囲攻撃
        {
            //攻撃範囲内にいる敵全員に攻撃する
            if(progressTime >= playerAttackSpeed)
            {
                for(int i = 0; ;i++)
                {
                    if(enemyObject[i] == null)
                    {
                        break;
                    }
                    commonAIBehaviour = enemyObject[i].GetComponent<CommonAIBehaviour>();
                    commonAIBehaviour.DamageDisplay(playerAttackDamage);
                    commonAIBehaviour.monsterHP -= playerAttackDamage;
                }
                progressTime = 0;
            }
        }

        if(playerExtentKind == 2) //着弾地点円形
        {
            //ターゲットシステムで指定された敵から円形の範囲内にいる敵を取得し、全員に攻撃
            if (progressTime >= playerAttackSpeed && enemyObject[arrayNum] != null)
            {
                Collider[] inExtentRad = Physics.OverlapSphere(enemyObject[arrayNum].transform.position, playerExtentRad);
                foreach (Collider collider in inExtentRad)
                {
                    if(collider == null)
                    {
                        break;
                    }
                    if(collider.gameObject.tag == "Enemy")
                    {
                        commonAIBehaviour = collider.gameObject.transform.parent.GetComponent<CommonAIBehaviour>();
                        commonAIBehaviour.DamageDisplay(playerAttackDamage);
                        commonAIBehaviour.monsterHP -= playerAttackDamage;
                    }
                }
                Array.Clear(inExtentRad, 0, inExtentRad.Length);
                progressTime = 0;
            }
        }

        if(playerExtentKind == 3) //扇形範囲攻撃
        {
            //ターゲットシステムで指定された敵を含み、その45度以内にいてかつ、攻撃範囲内にいる敵全員に攻撃　※角度は要調整
            if (progressTime >= playerAttackSpeed)
            {
                for (int i = 0; ; i++)
                {
                    if (enemyObject[i] == null)
                    {
                        break;
                    }
                    targetVec = enemyObject[arrayNum].transform.position - this.gameObject.transform.position;
                    checkVec = enemyObject[i].transform.position - this.gameObject.transform.position;

                    Vector3 forDebugVec = Quaternion.Euler(0, 45, 0) * targetVec;
                    Vector3 forDebugVec_2 = Quaternion.Euler(0, -45, 0) * targetVec;
                    Debug.DrawRay(this.gameObject.transform.position, forDebugVec,Color.blue ,1.0f);
                    Debug.DrawRay(this.gameObject.transform.position, forDebugVec_2, Color.blue, 1.0f);

                    float checkAngle = Vector3.Angle(targetVec, checkVec);
                    if(checkAngle <= 45.0f)
                    {
                        commonAIBehaviour = enemyObject[i].GetComponent<CommonAIBehaviour>();
                        commonAIBehaviour.DamageDisplay(playerAttackDamage);
                        commonAIBehaviour.monsterHP -= playerAttackDamage;
                    }
                }
                progressTime = 0;
            }
        }

        if(playerExtentKind == 4) //一直線ビーム攻撃
        {
            //ターゲットシステムで指定された敵方向に無限にＲａｙを飛ばし、当たった敵全員に攻撃
            if (progressTime >= playerAttackSpeed && enemyObject[arrayNum] != null)
            {
                Vector3 targetDirection = enemyObject[arrayNum].transform.position - this.gameObject.transform.position;
                Ray ray = new Ray(this.gameObject.transform.position, new Vector3(targetDirection.x, 0, targetDirection.z));
                RaycastHit[] hitsAll = Physics.RaycastAll(ray, Mathf.Infinity, int.MaxValue);
                Debug.DrawRay(this.gameObject.transform.position, new Vector3(targetDirection.x,0,targetDirection.z) * 1000,Color.blue,0.2f);

                foreach (RaycastHit hit in hitsAll)
                {
                    if(hit.collider.gameObject.tag == "Enemy")
                    {
                        commonAIBehaviour = hit.collider.gameObject.transform.parent.GetComponent<CommonAIBehaviour>();
                        commonAIBehaviour.DamageDisplay(playerAttackDamage);
                        commonAIBehaviour.monsterHP -= playerAttackDamage;
                    }
                }
                progressTime = 0;
            }
        }
    }

    private void OnTriggerStay(Collider otherEnter)
    {
        //攻撃範囲内の敵全員を取得
        if (otherEnter.gameObject.tag == "Enemy")
        {
            for (int cnt = 0; ; cnt++)
            {
                if(otherEnter.transform.parent.gameObject == enemyObject[cnt])
                {
                    listDistance[cnt] = Vector3.Distance(enemyObject[cnt].transform.position, this.gameObject.transform.position);
                    distance = listDistance.ToArray();
                    break;
                }

                if (enemyObject[cnt] == null)
                {
                    listEnemy.Insert(cnt, otherEnter.transform.parent.gameObject);
                    enemyObject = listEnemy.ToArray();

                    listDistance.Insert(cnt, Vector3.Distance(enemyObject[cnt].transform.position, this.gameObject.transform.position));
                    distance = listDistance.ToArray();
                    break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider otherExit)
    {
        //範囲から出たらリストから削除
        if (otherExit.gameObject.tag == "Enemy")
        {
            for (int cnt = 0; ; cnt++)
            {
                if (enemyObject[cnt] == otherExit.transform.parent.gameObject)
                {
                    listEnemy.RemoveAt(cnt);
                    listDistance.RemoveAt(cnt);
                    enemyObject = listEnemy.ToArray();
                    distance = listDistance.ToArray();
                    break;
                }
            }
        }
    }

    void NearTarget()　//一番近い敵を取得
    {
        float min = 100.0f;
        for (int i = 0 ; i < distance.Length ; i++)
        {

            if(distance[i] == 0.0f)
            {
                break;
            }

            if (min > distance[i])
            {
                min = distance[i];
                arrayNum = i;
            }
        }
    }

    public void TargetChangeButton(int i) //ターゲットシステム用
    {

    }
    
}
