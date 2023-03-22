using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class MainStageManager : MonoBehaviour
{

    public GameObject TopUIManagerObj;
    TopUIManager topUIManager;

    MainStageAudioManager mainStageAudioManager;

    GameObject GameFinishTextObj;
    GameObject GameFinishBackImageObj;
    GameObject GameFinishDialogImage;

    MainStageBallManager mainStageBallManager;
    GameObject StageStart;
    Text StageStartText;
    BallCollider ballCollider;

    int count2 = 3;
    int PanelNum;

    GameObject Panel1;
    GameObject Panel2;
    GameObject Panel3;

    GameObject[] WhichLevel;

    Vector3 Panel1Position = new Vector3(0, -90, 1620);
    Vector3 Panel2Position = new Vector3(0, -90, 4620);
    Vector3 Panel3Position = new Vector3(0, -90, 7620);
    Vector3 PanelStoreroom = new Vector3(-8000, 90, 5000);
    Vector3 NowPos1;
    Vector3 NowPos2;
    Vector3 NowPos3;

    float SpeedMulti;

    [Header("基本値スピード")]
    public float SlideSpeed;

    [Header("難易度2スピード係数")]
    public float Level2Speed;

    [Header("難易度3スピード係数")]
    public float Level3Speed;

    [Header("難易度4スピード係数")]
    public float Level4Speed;

    [Header("難易度5スピード係数")]
    public float Level5Speed;

    [Header("第1難易度までのパネル数")]
    public int DifficultyLevel1;

    [Header("第2難易度までのパネル数")]
    public int DifficultyLevel2;

    [Header("第3難易度までのパネル数")]
    public int DifficultyLevel3;

    [Header("第4難易度までのパネル数")]
    public int DifficultyLevel4;

    [Header("第1難易度のパネル")]
    public GameObject[] Level1Panel;

    [Header("第2難易度のパネル")]
    public GameObject[] Level2Panel;

    [Header("第3難易度のパネル")]
    public GameObject[] Level3Panel;

    [Header("第4難易度のパネル")]
    public GameObject[] Level4Panel;

    [Header("第5難易度のパネル")]
    public GameObject[] Level5Panel;

    private void Awake()
    {

    }
    void Start()
    {
        PanelNum = 0;

        topUIManager = TopUIManagerObj.GetComponent<TopUIManager>();
        TopUIManagerObj.SetActive(false);

        mainStageAudioManager = GameObject.Find("MainStageAudioManager").GetComponent<MainStageAudioManager>();

        GameFinishDialogImage = GameObject.Find("GameFinishDialogImage");
        GameFinishBackImageObj = GameObject.Find("GameFinishBackImage");
        GameFinishTextObj = GameObject.Find("GameFinishText");
        GameFinishBackImageObj.SetActive(false);
        GameFinishDialogImage.SetActive(false);

        mainStageBallManager = GameObject.Find("MainStageBallManager").GetComponent<MainStageBallManager>();
        StageStart = GameObject.Find("StageStartText");
        StageStartText = StageStart.GetComponent<Text>();
        ballCollider = GameObject.Find("Sphere").GetComponent<BallCollider>();

        int rnd = Random.Range(0, Level1Panel.Length);
        Panel1 = Level1Panel[rnd];
        Panel1.transform.position = Panel1Position;
        NowPos1 = Panel1Position;

        while (true)
        {
            rnd = Random.Range(0, Level1Panel.Length);
            if (Panel1 != Level1Panel[rnd] && Panel3 != Level1Panel[rnd])
            {
                Panel2 = Level1Panel[rnd];
                Panel2.transform.position = Panel2Position;
                NowPos2 = Panel2Position;
            }

            if (Panel2 != null)
            {
                break;
            }
        }

        while (true)
        {
            rnd = Random.Range(0, Level1Panel.Length);
            if (Panel2 != Level1Panel[rnd] && Panel1 != Level1Panel[rnd])
            {
                Panel3 = Level1Panel[rnd];
                Panel3.transform.position = Panel3Position;
                NowPos3 = Panel3Position;
            }

            if (Panel3 != null)
            {
                break;
            }
        }

        StartCoroutine("PlaneMove");
    }

    void Update()
    {
        if (PanelNum < DifficultyLevel1 && 0 <= PanelNum)
        {
            WhichLevel = Level1Panel;
            SpeedMulti = 1.0f;
            Debug.Log("今はレベル1だよ");
        }

        if (PanelNum < DifficultyLevel2 && DifficultyLevel1 <= PanelNum)
        {
            WhichLevel = Level2Panel;
            SpeedMulti = Level2Speed;
            Debug.Log("今はレベル2だよ");
        }

        if (PanelNum < DifficultyLevel3 && DifficultyLevel2 <= PanelNum)
        {
            WhichLevel = Level3Panel;
            SpeedMulti = Level3Speed;
            Debug.Log("今はレベル3だよ");
        }

        if (PanelNum < DifficultyLevel4 && DifficultyLevel3 <= PanelNum)
        {
            WhichLevel = Level4Panel;
            SpeedMulti = Level4Speed;
            Debug.Log("今はレベル4だよ");
        }
        if (PanelNum >= DifficultyLevel4)
        {
            WhichLevel = Level5Panel;
            SpeedMulti = Level5Speed;
            Debug.Log("今はレベル5だよ");
        }

        if (Panel1.transform.position.z <= -1600)
        {
            PanelNum++;
            Panel1.transform.position = PanelStoreroom;
            Panel1 = null;
            StopCoroutine("PanelMove");
            Panel1Change();
        }

        if (Panel2.transform.position.z <= -1600)
        {
            PanelNum++;
            Panel2.transform.position = PanelStoreroom;
            Panel2 = null;
            StopCoroutine("PanelMove");
            Panel2Change();

            /*
            while (true)
            {
                int rnd = Random.Range(0, Level1Panel.Length - 1);
                if (Panel1 != Level1Panel[rnd] && Panel3 != Level1Panel[rnd])
                {
                    Panel2 = Level1Panel[rnd];
                    NowPos2.z = NowPos1.z + 3000;
                    Panel2.transform.position = NowPos2;
                    //NowPos2 = Panel2.transform.position;
                    StartCoroutine("PanelMove");
                }

                if (Panel2 != null)
                {
                    break;
                }
            }
            */
        }

        if (Panel3.transform.position.z <= -1600)
        {
            PanelNum++;
            Panel3.transform.position = PanelStoreroom;
            Panel3 = null;
            StopCoroutine("PanelMove");
            Panel3Change();
        }

        //Debug.Log(PanelNum);
    }
    IEnumerator PlaneMove()
    {
        while (count2 > 0) //カウント変数が0より大きい場合繰り返し
        {
            StageStartText.text = count2.ToString(); //現在のカウント数表示
            yield return new WaitForSeconds(0.9f); //0.9秒待機
            count2--; //カウント1個減らす
            if (count2 == 0) //カウントが0まで来たら
            {
                StageStartText.text = "Start"; //Startと表示
                yield return new WaitForSeconds(0.9f); //0.9秒待機
                StageStart.SetActive(false); //ゲーム開始時のカウント非表示
                //mainStageAudioManager.StageBGMStart(); //BGMスタート
                mainStageBallManager.isTouch = false; //ボールを動かせるように
                mainStageBallManager.NowMove = false; //ボールを動かせるように
                mainStageBallManager.CanPlay = true;
                StartCoroutine("PanelMove");
            }
        }
        if (ballCollider.GameOver || ballCollider.GameClear) //ゲームオーバーかクリアしたとき
        {
            yield break; //パネルを動かすのを止める
        }

        /*
        while (!ballCollider.GameOver && !ballCollider.GameClear) //ゲームオーバーかクリア状態ではないとき
        {
            NowPos1.z -= SlideSpeed * Time.deltaTime; //パネルを動かす
            NowPos2.z -= SlideSpeed * Time.deltaTime; //パネルを動かす
            NowPos3.z -= SlideSpeed * Time.deltaTime; //パネルを動かす
            Panel1.transform.position = NowPos1;
            Panel2.transform.position = NowPos2;
            Panel3.transform.position = NowPos3;
            yield return null;
        }
        */
    }

    IEnumerator PanelMove()
    {
        while (!ballCollider.GameOver && !ballCollider.GameClear) //ゲームオーバーかクリア状態ではないとき
        {
            NowPos1.z -= SlideSpeed * SpeedMulti * Time.deltaTime; //パネルを動かす
            NowPos2.z -= SlideSpeed * SpeedMulti * Time.deltaTime; //パネルを動かす
            NowPos3.z -= SlideSpeed * SpeedMulti * Time.deltaTime; //パネルを動かす
            Panel1.transform.position = NowPos1;
            Panel2.transform.position = NowPos2;
            Panel3.transform.position = NowPos3;
            yield return null;
        }
    }

    void Panel1Change()
    {
        while (true)
        {
            int rnd = Random.Range(0, WhichLevel.Length);
            if (Panel2 != WhichLevel[rnd] && Panel3 != WhichLevel[rnd])
            {
                Panel1 = WhichLevel[rnd];
                NowPos1.z = NowPos3.z + 2998;
                Panel1.transform.position = NowPos1;
                //NowPos1 = Panel1.transform.position;
                StartCoroutine("PanelMove");
            }

            if (Panel1 != null)
            {
                break;
            }
        }
    }
    void Panel2Change()
    {
        while (true)
        {
            int rnd = Random.Range(0, WhichLevel.Length);
            if (Panel1 != WhichLevel[rnd] && Panel3 != WhichLevel[rnd])
            {
                Panel2 = WhichLevel[rnd];
                NowPos2.z = NowPos1.z + 2998;
                Panel2.transform.position = NowPos2;
                //NowPos2 = Panel2.transform.position;
                StartCoroutine("PanelMove");
            }

            if (Panel2 != null)
            {
                break;
            }
        }
    }

    void Panel3Change()
    {
        while (true)
        {
            int rnd = Random.Range(0, WhichLevel.Length);
            if (Panel2 != WhichLevel[rnd] && Panel1 != WhichLevel[rnd])
            {
                Panel3 = WhichLevel[rnd];
                NowPos3.z = NowPos2.z + 2998;
                Panel3.transform.position = NowPos3;
                //NowPos3 = Panel3.transform.position;
                StartCoroutine("PanelMove");
            }

            if (Panel3 != null)
            {
                break;
            }
        }
    }
}
