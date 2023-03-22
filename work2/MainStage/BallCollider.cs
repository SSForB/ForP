using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollider : MonoBehaviour
{
    public bool NowGround;
    public bool GameOver;
    public bool GameClear;

    void Start()
    {
        NowGround = true;
        GameOver = false;
        GameClear = false;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnCollisionStay(Collision collision) //ボールが何かに触れたとき
    {
        NowGround = true; //今地面かどうか判定ON
        if(collision.gameObject.tag == "Untagged") //当たったオブジェクトがUntagged(障害物)の場合
        {
            GameOver = true; //ゲームオーバーフラグON
        }
        /*
        if (collision.gameObject.tag == "goal")//当たったオブジェクトがゴール用の壁の場合
        {
            GameClear = true; //ゲームクリア判定ON
        }
        */
    }
    

    
    void OnCollisionExit(Collision other) //ボールが何もオブジェクトと触れてないとき
    {
        Debug.Log("離れた！");
        NowGround = false; //今地面かどうか判定OFF
    }
    
}
