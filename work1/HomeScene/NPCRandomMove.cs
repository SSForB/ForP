using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カプセルコライダーの距離分を足す

public class NPCRandomMove : MonoBehaviour
{
    [Header("NPCの移動速度")]
    public float speed;

    [Header("最大・最小待ち時間")]
    public float maxWaitTime;
    public float minWaitTime;

    [Header("配置した位置からどれほど動くか")]
    public float moveDistanceX;
    public float moveDistanceY;

    Vector2 oriPos; //キャラを配置したポジションを保存するための変数
    Vector2 nowPos; //動いてるキャラの現在地を取得するための変数
    Vector2 nowDestinationPos; //目的地を保存するための変数

    float waitTime; //キャラが動かない時間を取得するための変数

    float moveX; //どれほどX軸に動くか決める変数
    float moveY; //どれほどY軸に動くか決める変数

    bool isMoving = false; //移動中かのフラグ trueで動いてるとき falseで動いてないとき
　  bool canMove = false; //移動できるかどうか判定する変数 trueで移動できる状態 falseで移動できない状態
    

    void Start()
    {
        oriPos = transform.position; //oriPosに現在地を代入
        nowPos = transform.position; //nowPosに現在地を代入
        nowDestinationPos = transform.position; //nowDestinationPosに現在地を代入
        waitTime = Random.Range(minWaitTime, maxWaitTime); //最小、最大の幅でランダムな数値を代入
    }
     

    void Update()
    {
        if(waitTime < 0 && canMove) //waitTimeが0秒以下かつcanMoveがtrueの時
        {
            nowPos = transform.position; //常にnowPosに現在地を代入
            Move(); //常に動く
            
            if (nowPos == nowDestinationPos) //目的地と現在地が同じとき
            {
                waitTime = Random.Range(minWaitTime, maxWaitTime); //最小、最大の幅でランダムな数値を再代入
                isMoving = false; //動いていないことにする処理
            }
        }
        
        if (!isMoving) //動いていないとき
        {
            waitTime -= Time.deltaTime; //秒数を減らしてく
            if (nowPos == nowDestinationPos) //現在地と目的地が同じだった時
            {
                canMove = false; //動けないようにする処理
                SetDestination(); //目的地設定
                //Debug.Log($"目的地は{nowDestinationPos}");
            }
        }
               
        
    }
    private void Move()
    {
        isMoving = true; //動いてることにする処理
        transform.position = Vector2.MoveTowards(nowPos, nowDestinationPos, speed * Time.deltaTime); //nowPosからnowDestinationPosに等速直線運動をする
    }
    
    //次の目的地の設定
    void SetDestination() //目的地設定
    {
        while (true)
        {
            //Debug.Log("抽選するよ！");
            moveX = Random.Range(-moveDistanceX, moveDistanceX); //X座標に指定した幅でランダムな値を代入
            moveY = Random.Range(-moveDistanceY, moveDistanceY); //Y座標に指定した幅でランダムな値を代入

            nowDestinationPos.x = oriPos.x + moveX; //目的地のX座標にキャラを配置した場所＋上記で指定した数値を代入
            nowDestinationPos.y = oriPos.y + moveY; //目的地のY座標にキャラを配置した場所＋上記で指定した数値を代入

            var maxDistance = Vector2.Distance(nowPos, nowDestinationPos) + 0.4f; //現在地から目的地までの距離を代入

            float dx = nowDestinationPos.x - nowPos.x; //目的地-現在地のX座標差分を代入
            float dy = nowDestinationPos.y - nowPos.y; //目的地-現在地のY座標差分を代入

            Vector2 directon = new Vector2(dx, dy); //差分から向きを指定
        
            RaycastHit2D hit = Physics2D.Raycast(nowPos, directon, maxDistance); //現在地から目的地までRaycastを飛ばす

            Debug.Log(hit.collider);

            if (hit.collider == null) //Raycastで何もオブジェクトに当たらなかった時
            {
                //Debug.Log("動けるよ！");
                canMove = true; //動けるようにする処理
                break;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) //何かに当たった時
    {
        //Debug.Log("壁に当たったよ");
        nowDestinationPos = nowPos; //現在地と目的地を同じ値にする
    }
}
        


                

            
            

        




    





