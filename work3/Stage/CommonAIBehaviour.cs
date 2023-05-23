using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class CommonAIBehaviour : MonoBehaviour
{

    float enemySpeed;
    Transform enemyTransform;
    Vector3 enemyPos;
    Vector3 nowPos;
    Vector3 rayPos;
    GameObject enemyGameObject;
    Vector3 direction;
    float distance = 3.5f;
    RaycastHit hit;
    bool canUnder;
    bool stopUpdate = false;

    public string monsterName;
    public float monsterSpeed;
    public float monsterHP;
    public int dropGold;
    public int monsterListNum;

    float totalDamage = 0;

    MonsterParam monsterParam;

    TextMeshProUGUI damageText;

    string dbPath = "Assets/Scripts/DataBase/Monster.asset";

    int directionMove; //0=�E 1=�� 2=��

    int deadEndCounter = 0;

    void Start()
    {
        //�����X�^�[�X�e�[�^�X�擾
        monsterParam = AssetDatabase.LoadAssetAtPath<MonsterParam>(dbPath);
        monsterName = monsterParam.monsterDBList[monsterListNum].monsterName;
        monsterSpeed = monsterParam.monsterDBList[monsterListNum].monsterSpeed;
        enemySpeed = monsterSpeed;
        monsterHP = monsterParam.monsterDBList[monsterListNum].monsterHP;
        dropGold = monsterParam.monsterDBList[monsterListNum].dropGold;

        //���g��object�ƈʒu���擾
        enemyGameObject = this.gameObject;
        enemyTransform = enemyGameObject.transform;
        enemyPos = enemyTransform.position;

        directionMove = 2;
        rayPos = enemyPos;
        rayPos.y = -98.5f;

        //damage�\���p�̃R���|�[�l���g�擾
        damageText = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        damageText.text = "";
    }

    void Update()
    {

        //�s���ꂪ�Ȃ��Ƃ��ɓ������~�߂邽�߂ɃX�g�b�v
        if(stopUpdate)
        {
            return;
        }

        //���݈ʒu�擾
        nowPos = enemyPos;
        rayPos.z = nowPos.z;
        rayPos.x = nowPos.x;

        //HP���Ȃ��Ȃ����������
        if(monsterHP <= 0)
        {
            enemyPos.z += 100;
            Invoke("destroyOwn", 2.0f);
            //Destroy(this.gameObject);
        }

        //�E�����ɐi�ދ���
        if(directionMove == 0)
        {
            enemyPos.x += enemySpeed * Time.deltaTime;
            enemyTransform.position = enemyPos;
            direction = new Vector3(3.5f, 0, 0);
        }

        //�������ɐi�ދ���
        if (directionMove == 1)
        {
            enemyPos.x -= enemySpeed * Time.deltaTime;
            enemyTransform.position = enemyPos;
            direction = new Vector3(-3.5f, 0, 0);
        }

        //�������ɐi�ދ���
        if (directionMove == 2)
        {
            enemyPos.z -= enemySpeed * Time.deltaTime;
            enemyTransform.position = enemyPos;
            direction = new Vector3(0, 0, -3.5f);
        }

        //���E�ɍs���ꂪ�Ȃ������ɍs����Ƃ����ɍs��
        if (!Physics.Raycast(rayPos, new Vector3(1, 0, 0), out hit, 6) && !Physics.Raycast(rayPos, new Vector3(-1, 0, 0), out hit, 6) && !Physics.Raycast(rayPos, new Vector3(0, 0, -1), out hit, 6) && canUnder)
        {
            deadEndCounter = 0;
            directionMove = 2;
        }

        //�i�s������Ray������
        Ray ray = new Ray(rayPos, direction);

        //�i�s�����ɕǂ�����ΐi�߂�����m�F
        if (Physics.Raycast(ray, out hit, distance) && hit.collider.tag == "Wall" )
        {
            checkDirection();
        }
        //�v���C���[�����Ă������m�F
        else if (Physics.Raycast(ray, out hit, distance) && hit.collider.tag == "Player")
        {
            checkDirection();
        }

        Debug.DrawRay(rayPos,new Vector3(1, 0, 0) * 5, Color.red,0.1f);
        Debug.DrawRay(rayPos, new Vector3(-1, 0, 0) * 5, Color.blue, 0.1f);
        Debug.DrawRay(rayPos, new Vector3(0, 0, -1) * 5, Color.yellow, 0.1f);
    }

    private void OnTriggerStay(Collider otherStay)
    {
        //�I�u�W�F�N�g�̐^���ɃR���W�����pobject�p�Ӂ@�������ɍs���邩�ǂ����̔��������
        if(otherStay.gameObject.tag == "Wall" || otherStay.gameObject.tag == "Player")
        {
            canUnder = false;
        }
    }
    
    //�������ɂȂɂ��Ȃ���Ή��ɍs�����T�C��
    private void OnTriggerExit(Collider otherExit)
    {
        canUnder = true;
    }


    void checkDirection()
    {
        RaycastHit hitCheck;

        //���E�����ǂ���ɂ��s���Ȃ���΃X�g�b�v
        if (Physics.Raycast(rayPos, new Vector3(1, 0, 0), out hitCheck, 5) && Physics.Raycast(rayPos, new Vector3(-1, 0, 0), out hitCheck, 5) && Physics.Raycast(rayPos, new Vector3(0, 0, -1), out hitCheck, 5))
        {
            stopUpdate = true;
        }

        //�������ɂȂɂ��Ȃ���Ή������ɍs��
        if (Physics.Raycast(rayPos, new Vector3(0, 0, -1), out hitCheck, 5))
        {

        }
        else
        {
            deadEndCounter = 0;
            directionMove = 2;
        }

        //���E�ǂ���ɂ��s����Ƃ���50%�̊m���Œ��I
        int rnd = Random.Range(1, 3);

        if (rnd == 1)
        {
            //�������m�F
            if (Physics.Raycast(rayPos, new Vector3(1, 0, 0), out hitCheck, 5))
            {
            }
            //�Ȃɂ��Ȃ������݉E�����ɐi��łȂ���΍��ɍs���A���݉������ɐi��łȂ���΍��E�J�E���^�[+1 �����E�J�E���^�[�͂����ƉE�ɍs�����荶�ɍs�����肵�Ȃ��悤�A��U���E�ǂ��炩�̕����ɐi�݁A�ǂɓ��������ۂɊm�F���邽�߂̂���
            else if(deadEndCounter != 1)
            {
                if(directionMove != 2)
                {
                    deadEndCounter = 1;
                }
                directionMove = 0;
                return;
            }
            else if (Physics.Raycast(rayPos, new Vector3(0, 0, -1), out hitCheck, 5))
            {
                stopUpdate = true;
                return;
            }

            //�E�����m�F
            if (Physics.Raycast(rayPos, new Vector3(-1, 0, 0), out hitCheck, 5))
            {
            }
            else if(deadEndCounter != 1)
            {
                if (directionMove != 2)
                {
                    deadEndCounter = 1;
                }
                directionMove = 1;
            }
            else if (Physics.Raycast(rayPos, new Vector3(0, 0, -1), out hitCheck, 5))
            {
                stopUpdate = true;
            }
        }
        else
        {
            //�E�����m�F
            if (Physics.Raycast(rayPos, new Vector3(-1, 0, 0), out hitCheck, 5))
            {
            }
            else if (deadEndCounter != 1)
            {
                if (directionMove != 2)
                {
                    deadEndCounter = 1;
                }
                directionMove = 1;
                return;
            }
            else if(Physics.Raycast(rayPos, new Vector3(0, 0, -1), out hitCheck, 5))
            {
                stopUpdate = true;
                return;
            }

            //�������m�F
            if (Physics.Raycast(rayPos, new Vector3(1, 0, 0), out hitCheck, 5))
            {
            }
            else if (deadEndCounter != 1)
            {
                if (directionMove != 2)
                {
                    deadEndCounter = 1;
                }
                directionMove = 0;
            }
            else if (Physics.Raycast(rayPos, new Vector3(0, 0, -1), out hitCheck, 5))
            {
                stopUpdate = true;
            }
        }
    }

    //�_���[�W���󂯂��ۂɕ\�����邽�߂̃��\�b�h
    public void DamageDisplay(float i)
    {
        totalDamage += i;
        damageText.text = totalDamage.ToString();
    }

    //���ꂽ�Ƃ��̃��\�b�h
    void destroyOwn()
    {
        Destroy(this.gameObject);
    }
}
