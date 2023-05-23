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

    int directionMove; //0=右 1=左 2=下

    int deadEndCounter = 0;

    void Start()
    {
        //モンスターステータス取得
        monsterParam = AssetDatabase.LoadAssetAtPath<MonsterParam>(dbPath);
        monsterName = monsterParam.monsterDBList[monsterListNum].monsterName;
        monsterSpeed = monsterParam.monsterDBList[monsterListNum].monsterSpeed;
        enemySpeed = monsterSpeed;
        monsterHP = monsterParam.monsterDBList[monsterListNum].monsterHP;
        dropGold = monsterParam.monsterDBList[monsterListNum].dropGold;

        //自身のobjectと位置を取得
        enemyGameObject = this.gameObject;
        enemyTransform = enemyGameObject.transform;
        enemyPos = enemyTransform.position;

        directionMove = 2;
        rayPos = enemyPos;
        rayPos.y = -98.5f;

        //damage表示用のコンポーネント取得
        damageText = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        damageText.text = "";
    }

    void Update()
    {

        //行き場がないときに動きを止めるためにストップ
        if(stopUpdate)
        {
            return;
        }

        //現在位置取得
        nowPos = enemyPos;
        rayPos.z = nowPos.z;
        rayPos.x = nowPos.x;

        //HPがなくなったら消える
        if(monsterHP <= 0)
        {
            enemyPos.z += 100;
            Invoke("destroyOwn", 2.0f);
            //Destroy(this.gameObject);
        }

        //右方向に進む挙動
        if(directionMove == 0)
        {
            enemyPos.x += enemySpeed * Time.deltaTime;
            enemyTransform.position = enemyPos;
            direction = new Vector3(3.5f, 0, 0);
        }

        //左方向に進む挙動
        if (directionMove == 1)
        {
            enemyPos.x -= enemySpeed * Time.deltaTime;
            enemyTransform.position = enemyPos;
            direction = new Vector3(-3.5f, 0, 0);
        }

        //下方向に進む挙動
        if (directionMove == 2)
        {
            enemyPos.z -= enemySpeed * Time.deltaTime;
            enemyTransform.position = enemyPos;
            direction = new Vector3(0, 0, -3.5f);
        }

        //左右に行き場がなくかつ下に行けるとき下に行く
        if (!Physics.Raycast(rayPos, new Vector3(1, 0, 0), out hit, 6) && !Physics.Raycast(rayPos, new Vector3(-1, 0, 0), out hit, 6) && !Physics.Raycast(rayPos, new Vector3(0, 0, -1), out hit, 6) && canUnder)
        {
            deadEndCounter = 0;
            directionMove = 2;
        }

        //進行方向にRayをうつ
        Ray ray = new Ray(rayPos, direction);

        //進行方向に壁があれば進める方向確認
        if (Physics.Raycast(ray, out hit, distance) && hit.collider.tag == "Wall" )
        {
            checkDirection();
        }
        //プレイヤーがいても方向確認
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
        //オブジェクトの真下にコリジョン用object用意　下方向に行けるかどうかの判定をする
        if(otherStay.gameObject.tag == "Wall" || otherStay.gameObject.tag == "Player")
        {
            canUnder = false;
        }
    }
    
    //下方向になにもなければ下に行けるよサイン
    private void OnTriggerExit(Collider otherExit)
    {
        canUnder = true;
    }


    void checkDirection()
    {
        RaycastHit hitCheck;

        //左右したどちらにも行けなければストップ
        if (Physics.Raycast(rayPos, new Vector3(1, 0, 0), out hitCheck, 5) && Physics.Raycast(rayPos, new Vector3(-1, 0, 0), out hitCheck, 5) && Physics.Raycast(rayPos, new Vector3(0, 0, -1), out hitCheck, 5))
        {
            stopUpdate = true;
        }

        //下方向になにもなければ下方向に行く
        if (Physics.Raycast(rayPos, new Vector3(0, 0, -1), out hitCheck, 5))
        {

        }
        else
        {
            deadEndCounter = 0;
            directionMove = 2;
        }

        //左右どちらにも行けるときに50%の確率で抽選
        int rnd = Random.Range(1, 3);

        if (rnd == 1)
        {
            //左方向確認
            if (Physics.Raycast(rayPos, new Vector3(1, 0, 0), out hitCheck, 5))
            {
            }
            //なにもないかつ現在右方向に進んでなければ左に行く、現在下方向に進んでなければ左右カウンター+1 ※左右カウンターはずっと右に行ったり左に行ったりしないよう、一旦左右どちらかの方向に進み、壁に当たった際に確認するためのもの
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

            //右方向確認
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
            //右方向確認
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

            //左方向確認
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

    //ダメージを受けた際に表示するためのメソッド
    public void DamageDisplay(float i)
    {
        totalDamage += i;
        damageText.text = totalDamage.ToString();
    }

    //やられたときのメソッド
    void destroyOwn()
    {
        Destroy(this.gameObject);
    }
}
