using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    Vector3 ClickStartPosition;
    Vector3 ClickGoalPosition;
    Vector3 ClickPositionDif;
    Vector3 SlidePos;
    Vector3 OriginPos;
    Vector3 RightPos;
    Vector3 LeftPos;
    Vector3 NowPos;
    public Vector3 JumpForce;

    Transform TransformSlideBall;

    public float FlickJudgeDistance;
    public float SlideSpeed;
    public float JumpSpeed;
    float FlickDistanceX;
    float FlickDistanceY;
    public float SlideBallDistanceX;
    public float Gravity;
    int NowField;

    public GameObject SlideBall;

    public bool NowMove;
    public bool isTouch;
    public bool CanPlay;

    Rigidbody BallRigid;
    Collider BallCollider;

    BallCollider BallColliderScript;

    StageAudioManager stageAudioManager;

    IEnumerator CoroutineMoveLeft;
    IEnumerator CoroutineMoveRight;

    void Start()
    {
        stageAudioManager = GameObject.Find("StageAudioManager").GetComponent<StageAudioManager>();
        BallRigid = GameObject.Find("Sphere").GetComponent<Rigidbody>();
        BallCollider = GameObject.Find("Sphere").GetComponent<SphereCollider>();
        BallColliderScript = GameObject.Find("Sphere").GetComponent<BallCollider>();
        TransformSlideBall = SlideBall.transform;
        SlidePos = TransformSlideBall.position;
        OriginPos = TransformSlideBall.position;
        RightPos = OriginPos;
        LeftPos = OriginPos;
        RightPos.x = TransformSlideBall.position.x + SlideBallDistanceX;
        LeftPos.x = TransformSlideBall.position.x - SlideBallDistanceX;
        NowField = 1;
        NowMove = true;
        Physics.gravity = new Vector3(0, Gravity, 0);
        isTouch = false;
        CanPlay = false;
        CoroutineMoveRight = BallMoveRight();
        CoroutineMoveLeft = BallMoveLeft();
    }

    void Update()
    {
        if(!CanPlay)
        {
            return;
        }

        if(TransformSlideBall.position.x == OriginPos.x)
        {
            NowField = 1;
        }

        if (TransformSlideBall.position.x == LeftPos.x)
        {
            NowField = 0;
        }

        if (TransformSlideBall.position.x == RightPos.x)
        {
            NowField = 2;
        }


        if (Input.GetMouseButtonDown(0))
        {
            ClickStartPosition = Input.mousePosition;
            if (BallColliderScript.NowGround)
            {
                isTouch = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ClickGoalPosition = Input.mousePosition;
            ClickPositionDif = ClickGoalPosition - ClickStartPosition;
            FlickDistanceX = Mathf.Abs(ClickPositionDif.x);
            FlickDistanceY = Mathf.Abs(ClickPositionDif.y);
        }

        if (FlickDistanceX >= FlickJudgeDistance)
        {
            if (ClickPositionDif.x > 0) //右方向
            {
                stageAudioManager.SE(2);
                NowMove = true;
                TransformSlideBall = SlideBall.transform;
                //SlidePos = TransformSlideBall.position;
                NowPos = TransformSlideBall.position;
                //SlidePos.x += SlideBallDistanceX;
                ClickPositionDif.x = 0;

                StopAllCoroutines();
                CoroutineMoveLeft = null;
                CoroutineMoveLeft = BallMoveLeft();
                StartCoroutine(CoroutineMoveLeft);
            }

            if (ClickPositionDif.x < 0) //左方向
            {
                NowMove = true;
                TransformSlideBall = SlideBall.transform;
                //SlidePos = TransformSlideBall.position;
                NowPos = TransformSlideBall.position;
                //SlidePos.x -= SlideBallDistanceX;
                ClickPositionDif.x = 0;

                StopAllCoroutines();
                CoroutineMoveRight = null;
                CoroutineMoveRight = BallMoveRight();
                StartCoroutine(CoroutineMoveRight);
            }
        }

        if (FlickDistanceX < FlickJudgeDistance && FlickDistanceY < FlickJudgeDistance && BallColliderScript.NowGround)
        {
            stageAudioManager.SE(1);
            //BallColliderScript.NowGround = false;
            BallRigid.AddForce(JumpForce, ForceMode.Acceleration);
        }
    }

    IEnumerator BallMoveLeft()
    {

        stageAudioManager.SE(2);

        if (NowField == 2)
        {
            NowMove = false;
            yield break;
        }

        if (OriginPos.x <= NowPos.x && NowPos.x <= RightPos.x) //ボールが真ん中から右の間だったら
        {
            while (NowPos.x <= RightPos.x)
            {
                NowPos = TransformSlideBall.position;
                NowPos.x += SlideSpeed * Time.deltaTime;
                TransformSlideBall.position = NowPos;
                yield return null;

                if (NowPos.x > RightPos.x)
                {
                    TransformSlideBall.position = new Vector3(RightPos.x , NowPos.y , NowPos.z);
                    NowMove = false;
                    yield break;
                }
            }
        }
        else if(LeftPos.x <= NowPos.x && NowPos.x <= OriginPos.x) //ボールが左から真ん中の間だったら
        {
            while (NowPos.x <= OriginPos.x)
            {
                NowPos = TransformSlideBall.position;
                NowPos.x += SlideSpeed * Time.deltaTime;
                TransformSlideBall.position = NowPos;
                yield return null;

                if (NowPos.x > OriginPos.x)
                {
                    TransformSlideBall.position = new Vector3(OriginPos.x, NowPos.y, NowPos.z);
                    NowMove = false;
                    yield break;
                }
            }
        }            
    }

    IEnumerator BallMoveRight()
    {

        stageAudioManager.SE(2);

        if (NowField == 0)
        {
            NowMove = false;
            yield break;
        }

        if (LeftPos.x <= NowPos.x && NowPos.x <= OriginPos.x) //ボールが左から真ん中の間だったら
        {
            while (NowPos.x >= LeftPos.x)
            {
                NowPos = TransformSlideBall.position;
                NowPos.x -= SlideSpeed * Time.deltaTime;
                TransformSlideBall.position = NowPos;
                yield return null;

                if (NowPos.x < LeftPos.x)
                {
                    TransformSlideBall.position = new Vector3(LeftPos.x, NowPos.y, NowPos.z);
                    NowMove = false;
                    yield break;
                }
            }
        }
        else if(OriginPos.x <= NowPos.x && NowPos.x <= RightPos.x) //ボールが真ん中から右の間だったら
        {
            while (NowPos.x >= OriginPos.x)
            {
                NowPos = TransformSlideBall.position;
                NowPos.x -= SlideSpeed * Time.deltaTime;
                TransformSlideBall.position = NowPos;
                yield return null;

                if (NowPos.x < OriginPos.x)
                {
                    TransformSlideBall.position = new Vector3(OriginPos.x, NowPos.y, NowPos.z);
                    NowMove = false;
                    yield break;
                }
            }
        }

    }
}
