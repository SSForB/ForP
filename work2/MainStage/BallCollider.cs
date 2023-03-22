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

    
    void OnCollisionStay(Collision collision) //�{�[���������ɐG�ꂽ�Ƃ�
    {
        NowGround = true; //���n�ʂ��ǂ�������ON
        if(collision.gameObject.tag == "Untagged") //���������I�u�W�F�N�g��Untagged(��Q��)�̏ꍇ
        {
            GameOver = true; //�Q�[���I�[�o�[�t���OON
        }
        /*
        if (collision.gameObject.tag == "goal")//���������I�u�W�F�N�g���S�[���p�̕ǂ̏ꍇ
        {
            GameClear = true; //�Q�[���N���A����ON
        }
        */
    }
    

    
    void OnCollisionExit(Collision other) //�{�[���������I�u�W�F�N�g�ƐG��ĂȂ��Ƃ�
    {
        Debug.Log("���ꂽ�I");
        NowGround = false; //���n�ʂ��ǂ�������OFF
    }
    
}
