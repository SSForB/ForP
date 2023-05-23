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

    int targetSystem = 0; //0=���g�����ԋ߂� 1=

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
        //�v���C���[�X�e�[�^�X�擾
        playerParam = AssetDatabase.LoadAssetAtPath<PlayerParam>(dbPath);
        rnd = UnityEngine.Random.Range(0, playerParam.PlayerDBList.Count);
        playerName = playerParam.PlayerDBList[rnd].playerName;
        playerRange = playerParam.PlayerDBList[rnd].playerRange;
        playerExtentKind = playerParam.PlayerDBList[rnd].playerExtentKind;
        playerExtentRad = playerParam.PlayerDBList[rnd].playerExtentRad;
    �@�@playerAttackSpeed = playerParam.PlayerDBList[rnd].playerAttackSpeed;
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
        progressTime += Time.deltaTime; //�O�̍U������̎��Ԏ擾

        if (playerExtentKind == 0) //�ʏ�P���U��
        {
            //�^�[�Q�b�g�V�X�e���Ŏw�肳�ꂽ�G�ɑ΂��čU������
            if (progressTime >= playerAttackSpeed && enemyObject[arrayNum] != null)
            {
                commonAIBehaviour = enemyObject[arrayNum].GetComponent<CommonAIBehaviour>();
                commonAIBehaviour.DamageDisplay(playerAttackDamage);
                commonAIBehaviour.monsterHP -= playerAttackDamage;
                progressTime = 0;
            }
        }

        if(playerExtentKind == 1) //���g�̎���͈͍U��
        {
            //�U���͈͓��ɂ���G�S���ɍU������
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

        if(playerExtentKind == 2) //���e�n�_�~�`
        {
            //�^�[�Q�b�g�V�X�e���Ŏw�肳�ꂽ�G����~�`�͈͓̔��ɂ���G���擾���A�S���ɍU��
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

        if(playerExtentKind == 3) //��`�͈͍U��
        {
            //�^�[�Q�b�g�V�X�e���Ŏw�肳�ꂽ�G���܂݁A����45�x�ȓ��ɂ��Ă��A�U���͈͓��ɂ���G�S���ɍU���@���p�x�͗v����
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

        if(playerExtentKind == 4) //�꒼���r�[���U��
        {
            //�^�[�Q�b�g�V�X�e���Ŏw�肳�ꂽ�G�����ɖ����ɂq�������΂��A���������G�S���ɍU��
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
        //�U���͈͓��̓G�S�����擾
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
        //�͈͂���o���烊�X�g����폜
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

    void NearTarget()�@//��ԋ߂��G���擾
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

    public void TargetChangeButton(int i) //�^�[�Q�b�g�V�X�e���p
    {

    }
    
}
