using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    string StageName;
    GameObject StagePlane;
    public GameObject[] StagePlaneAttach;
    public float[] SlideSpeed;
    GameObject LastStagePlaneAttach;
    GameObject StageStart;
    BallManager ballManager;

    [System.NonSerialized]
    public int ListNumber;

    Transform TransformPlane;
    Vector3 NowPos;
    StageAudioManager stageAudioManager;

    BallCollider ballCollider;

    Text StageStartText;
    Text GameFinishText;
    GameObject GameFinishTextObj;
    GameObject GameFinishBackImageObj;
    GameObject GameFinishDialogImage;
    GameObject TopUIManagerObj;
    TopUIManager topUIManager;
    public GameObject NextOrRetryButtonText;

    int count2 = 3;

    private void Awake()
    {
        TopUIManagerObj = GameObject.Find("TopUIManager");
        topUIManager = TopUIManagerObj.GetComponent<TopUIManager>();
        GameFinishDialogImage = GameObject.Find("GameFinishDialogImage");
        GameFinishBackImageObj = GameObject.Find("GameFinishBackImage");
        GameFinishTextObj = GameObject.Find("GameFinishText");
        GameFinishBackImageObj.SetActive(false);
        GameFinishDialogImage.SetActive(false);
        TopUIManagerObj.SetActive(false);
        ballManager = GameObject.Find("BallManager").GetComponent<BallManager>();
        StageStart = GameObject.Find("StageStartText");
        StageStartText = StageStart.GetComponent<Text>();
        ballCollider = GameObject.Find("Sphere").GetComponent<BallCollider>();
        stageAudioManager = GameObject.Find("StageAudioManager").GetComponent<StageAudioManager>();
        StageName = PlayerPrefs.GetString("StageNameSave");
        StagePlane = GameObject.Find(StageName);
        LastStagePlaneAttach = StagePlaneAttach.Last();
        TransformPlane = StagePlane.transform;
        NowPos = TransformPlane.position;
        int count = 0;

        while (true)
        {

            if (StagePlane == null) //選択したステージがなかったら抜ける
            {
                break;
            }

            if (StagePlane.name == StagePlaneAttach[count].name) //選択したステージのオブジェクト名がアタッチしたオブジェクトの名前を一致している場合
            {
                ListNumber = count; //何番目か記録
                count++; //次のオブジェクトに
                continue;
            }
            StagePlaneAttach[count].SetActive(false); //選択したステージじゃない場合これが選択され、非表示になる
            count++; //次のオブジェクトに

            if (StagePlaneAttach[count] == LastStagePlaneAttach) //最後までオブジェクトの表示非表示判定が来たら
            {
                StagePlaneAttach[count].SetActive(false); //最後のオブジェクトを消し
                break; //処理を終える
            }
        }
    }
    void Start()
    {
        StartCoroutine("PlaneMove"); //ステージ開始時の演出とパネルを動かす用のコルーチン再生
    }

    // Update is called once per frame
    void Update()
    {
        if (ballCollider.GameOver) //ゲームオーバーになったら
        {
            GameFinishTextObj.GetComponent<Text>().text = "Game Over"; //テキストを設定
            GameFinishBackImageObj.SetActive(true); //画面にゲームオーバー表示
            stageAudioManager.StopBGM(); //BGM止める
            StartCoroutine(DisplayDialog(0)); //リトライor戻るダイアログを表示
            StopCoroutine("PlaneMove");
            ballCollider.GameOver = false;
        }

        if(ballCollider.GameClear) //ゲームクリア時
        {
            GameFinishTextObj.GetComponent<Text>().text = "Game Clear"; //テキストを設定
            GameFinishBackImageObj.SetActive(true); //画面にゲームオーバー表示
            stageAudioManager.StopBGM(); //BGM止める
            StartCoroutine(DisplayDialog(1)); //次のステージor戻るダイアログを表示
            StopCoroutine("PlaneMove");
            ballCollider.GameClear = false;
        }
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
                stageAudioManager.StageBGMStart(); //BGMスタート
                ballManager.isTouch = false; //ボールを動かせるように
                ballManager.NowMove = false; //ボールを動かせるように
                ballManager.CanPlay = true;
            }
        }
        if (ballCollider.GameOver || ballCollider.GameClear) //ゲームオーバーかクリアしたとき
        {
            yield break; //パネルを動かすのを止める
        }

        while (!ballCollider.GameOver && !ballCollider.GameClear) //ゲームオーバーかクリア状態ではないとき
        {
            NowPos.z -= SlideSpeed[ListNumber] * Time.deltaTime; //パネルを動かす
            TransformPlane.position = NowPos; //現在の位置取得
            yield return null;
        }
    }

    IEnumerator DisplayDialog(int i) //0 = ゲームオーバー    1 = クリア
    {
        if(i == 0)
        {
            stageAudioManager.SE(4);
        }
        else
        {
            stageAudioManager.SE(5);
        }
        yield return new WaitForSeconds(1.5f); //ゲームオーバー判定から指定秒数待つ
        GameFinishDialogImage.SetActive(true); //ダイアログ表示
        if (i == 0) //GameOver
        {
            NextOrRetryButtonText.GetComponent<Text>().text = "リトライ"; //ボタンのテキストをリトライに
        }
        else //GameClear
        {
            NextOrRetryButtonText.GetComponent<Text>().text = "次のステージへ"; //ボタンのテキストを次のステージへに
        }
        TopUIManagerObj.SetActive(true); //TopUI表示
        yield break;
    }

    public void PushDialog(Button ButtonName) //ゲームフィニッシュ後のボタン選択時のメソッド(ボタンアタッチ用)
    {
        if (ButtonName.name == "BackToStageSelectButton") //ステージ選択画面へ戻る
        {
            stageAudioManager.SE(7);
            PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name); //現在のシーン名をセーブし
            SceneManager.LoadScene("Title Scene"); //タイトル画面に戻る(タイトル画面の処理でステージから遷移されたらステージセレクトに遷移する処理を入れている)
        }
        else if (ballCollider.GameOver)//GameOverかつリトライ
        {
            stageAudioManager.SE(6);
            topUIManager.StaminaNumber--; //スタミナ1減少
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //ステージ更新
        }
        else //GameClearかつ次のステージへ
        {
            stageAudioManager.SE(6);
            ListNumber++; //ステージナンバーを次のへ
            StageName = StagePlaneAttach[ListNumber].name; //ステージ名を保存
            PlayerPrefs.SetString("StageNameSave", StageName); //ステージ名を保存
            topUIManager.StaminaNumber--; //スタミナ1減少
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //ステージ更新
        }
    }
}
