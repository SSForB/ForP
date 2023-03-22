using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveManager : MonoBehaviour
{
    Vector2 oriPos; //キャラを配置したポジションを記録
    Vector2 nowPos; //移動後のポジションを記録
    Vector2 goPos; //目的地のポジションを記録

    [Header("NPCキャラの移動速度")] 
    public float speed = 7.0f; //歩くスピード

    [Header("NPCキャラの移動幅")]
    public float moveDisX = 1.0f; //X軸の幅
    public float moveDisY = 1.0f; //Y軸の幅

    [Header("NPCが再度動くまでの時間の最小値/最大値")]
    public float waitMin = 1.0f; //止まっている最小時間
    public float waitMax = 10.0f; //止まっている最大時間

    float waitTime; //止まっている時間を上記の幅でランダム指定し格納するための変数

    float MoveX; //X座標用の宣言
    float MoveY; //Y座標用の宣言

    float goPosX; //目的地のX座標を丸めるための変数
    float nowPosX; //現在地のX座標を丸めるための変数
    float goPosY; //目的地のY座標を丸めるための変数
    float nowPosY; //現在地のY座標を丸めるための変数

    //もとの位置を保存nowPos→ランダム関数でtransform値に代入して移動goPos→何かしらに当たったら動きストップ・・・これによって最初に配置した位置を中心に周りに動くようにする※自由自在に動き回れるものではなくする

    void Start()
    {
        oriPos = transform.position; //初期地点を取得
        goPos = oriPos; //初期地点を取得
        nowPos = oriPos; //初期地点を取得

        goPosX = Mathf.Ceil(goPos.x); //目的地のX軸を繰り上げで整数に
        nowPosX = Mathf.Ceil(nowPos.x); //現在地のX軸を繰り上げで整数に
        goPosY = Mathf.Ceil(goPos.y); //目的地のY軸を繰り上げで整数に
        nowPosY = Mathf.Ceil(nowPos.y); //現在地のX軸を繰り上げで整数に

        StartCoroutine("WaitMove"); //コルーチンスタート
    }

    void OnCollisionEnter2D(Collision2D collision) //何かしらに当たった時
    {
        UnityEngine.Debug.Log("何かしらの壁にあたりました");
        goPos = oriPos;
        StartCoroutine("WaitMove2");
    }

    void Update()
    {
        nowPos = transform.position; //常に自分のポジション値を記録

        goPosX = Mathf.Ceil(goPos.x); //常に目的地のX軸を繰り上げで整数に
        nowPosX = Mathf.Ceil(nowPos.x); //常に現在地のX軸を繰り上げで整数に
        goPosY = Mathf.Ceil(goPos.y); //常に目的地のY軸を繰り上げで整数に
        nowPosY = Mathf.Ceil(nowPos.y); //常に現在地のX軸を繰り上げで整数に

        Move(); //常に動く
    }

    void Move() //移動処理
    {
        transform.position = Vector2.MoveTowards(nowPos, goPos, speed * Time.deltaTime); //nowPosからgoPosに、指定したスピードの等速直線運動で移動
        //Debug.Log("現在の移動先はX軸" + goPos.x + "Y軸" + goPos.y);
    }

    private IEnumerator WaitMove() //実質Updateのような処理にし、キャラを停止させる時間を作る
    {
        while (true)
        {
            if (goPosX == nowPosX || goPosY == nowPosY) //目的地と自分の位置が同じだった場合=止まっている場合
            {
                waitTime = Random.Range(waitMin, waitMax); //指定した幅でランダムな時間を指定
                //Debug.Log(waitTime + "秒待ちます");
                yield return new WaitForSeconds(waitTime); //上記で指定した時間分停止
                MoveX = Random.Range(-moveDisX, moveDisX); //指定した幅でランダムな数値を取得
                MoveY = Random.Range(-moveDisY, moveDisY); //指定した幅でランダムな数値を取得
                goPos.x = oriPos.x + MoveX; //X座標に上記で決定したランダムな数値をキャラを配置した地点のX座標の値に加算
                goPos.y = oriPos.y + MoveY; //Y座標に上記で決定したランダムな数値をキャラを配置した地点のY座標の値に加算
            }
        }
    }

    private IEnumerator WaitMove2()
    {
        yield return new WaitForSeconds(0.1f);
        goPos = nowPos;
    }
}
