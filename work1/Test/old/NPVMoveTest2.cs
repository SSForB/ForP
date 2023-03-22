using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPVMoveTest2 : MonoBehaviour
{
    float moveX;
    float moveY;
    float goingPosX;
    float goingPosY;

    Vector2 oriPos;
    Vector2 goPos;
    Vector2 nowPos;

    Rigidbody2D rbNPC;

    void Start()
    {
        oriPos = transform.position;
        nowPos = transform.position;
        goPos = transform.position;
        rbNPC = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rbNPC.velocity == (new Vector2(0, 0)))
        {
            nowPos = transform.position;
            moveX = Random.Range(-1.0f, 1.0f); //-1～1までのランダムな数値を取得
            moveY = Random.Range(-1.0f, 1.0f); //-1～1までのランダムな数値を取得
            goPos.x = oriPos.x + moveX; //X座標にランダムな数値を加算
            goPos.y = oriPos.y + moveY; //Y座標にランダムな数値を加算
            //goingPosX = goPos.x - nowPos.x;
            //goingPosY = goPos.y - nowPos.y;
            Debug.Log("動きが0の時の動作");
            Move();
        }
        else
        {
            Move();
        }
    }

    void Move() //移動
    {
        transform.position = Vector2.MoveTowards(nowPos, goPos, 2.0f * Time.deltaTime); //nowPosからgoPosに移動
        Debug.Log("動いてる時の動作");
    }
}
