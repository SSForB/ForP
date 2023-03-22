using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectSceneManager : MonoBehaviour
{

    Vector3 ClickStartPosition;
    Vector3 ClickGoalPosition;
    Vector3 ClickPositionDif;
    Vector3 SlidePos;
    Vector3 NowPos;

    Transform TransformSlidePanel;

    public float FlickJudgeDistance;
    public float SlideSpeed;
    float FlickDistanceX;
    int NowStage;
    Text DialogText;

    public GameObject SlidePanelCanvas;
    public GameObject StageSelectDialog;
    public AudioManager audioManager;
    public TopUIManager topUIManager;
    GameObject _audioManager;

    bool NowMove;

    void Start()
    {
        DialogText = GameObject.Find("DialogText").GetComponent<Text>(); //ゲームフィニッシュ後のダイアログ用テキスト取得
        topUIManager = GameObject.Find("TopUIManager").GetComponent<TopUIManager>(); //TopUIのscript取得
        _audioManager = GameObject.Find("AudioManager"); //BGM用ゲームオブジェクト取得
        audioManager = _audioManager.GetComponent<AudioManager>(); //script取得

        TransformSlidePanel = SlidePanelCanvas.transform; //canvasの現在位置を取得
        NowStage = 1; //今はステージ1を表示中
        NowMove = false; //今はスライド中ではない
        StageSelectDialog.SetActive(false); //ステージ選択後のダイアログを非表示
    }

    void Update()
    {
        if(NowMove) //もしスライド中だったら
        {
            return; //スライドが終わるまで動かす処理を実行させない
        }

        if (Input.GetMouseButtonDown(0)) //画面をタップしたとき
        {
            ClickStartPosition = Input.mousePosition; //押した位置を記録
        }

        if (Input.GetMouseButtonUp(0)) //画面タップを離したとき
        {
            ClickGoalPosition = Input.mousePosition; //離した位置を記録
            ClickPositionDif = ClickGoalPosition - ClickStartPosition; //押した位置と離した位置の差を出す
            FlickDistanceX = Mathf.Abs(ClickPositionDif.x); //絶対値にしマイナス値を排除
        }

        if(FlickDistanceX >= FlickJudgeDistance) //フリック距離が、設定したフリック判定距離より大きかったら
        {
            if(ClickPositionDif.x > 0 && NowStage >= 2)//左方向にフリックし、ステージ2かステージ3表示中だったら
            {
                NowMove = true; //動いてる判定ON
                TransformSlidePanel = SlidePanelCanvas.transform; //canvasの現在の位置取得
                SlidePos = TransformSlidePanel.position; //canvasの現在の位置取得
                NowPos = TransformSlidePanel.position; //現在の位置用の変数に位置を代入
                SlidePos.x += 205.3f; //現在の位置から右方向にパネル１枚分の距離を加算(パネルが右に動くため画面では左のステージが中央に来る)
                StartCoroutine("PanelMoveLeft"); //コルーチンで動いてるようなアニメーションをする
                NowStage--; //現在表示中のステージ変数を代入
                ClickPositionDif.x = 0; //クリックしたことをしてないことに初期化
            }

            if (ClickPositionDif.x < 0 && NowStage <= 2)//右方向にフリックし、ステージ1かステージ2表示中だったら
            {
                NowMove = true; //動いてる判定ON
                TransformSlidePanel = SlidePanelCanvas.transform; //canvasの現在の位置取得
                SlidePos = TransformSlidePanel.position; //canvasの現在の位置取得
                NowPos = TransformSlidePanel.position; //現在の位置用の変数に位置を代入
                SlidePos.x -= 205.3f; //現在の位置から右方向にパネル１枚分の距離を減算(パネルが左に動くため画面では右のステージが中央に来る)
                StartCoroutine("PanelMoveRight"); //コルーチンで動いてるようなアニメーションをする
                NowStage++; //現在表示中のステージ変数を代入
                ClickPositionDif.x = 0; //クリックしたことをしてないことに初期化
            }
        }
    }

    IEnumerator PanelMoveLeft () //左にあるパネルが中央に来る
    {
        audioManager.StageSelectSlide();
        while (NowPos.x <= SlidePos.x) //現在の位置が目的位置まで到達していない場合
        {
            NowPos.x += SlideSpeed; //少しずつ動かす
            TransformSlidePanel.position = NowPos; //現在の位置更新
            yield return null;

            if (NowPos.x > SlidePos.x) //現在の位置が目的地をオーバーしたら
            {
                TransformSlidePanel.position = SlidePos; //現在地を目的地に修正し
                NowMove = false; //動いてる判定OFF
                yield break; //終わり
            }
        }
    }

    IEnumerator PanelMoveRight() //右にあるパネルが中央に来る
    {
        audioManager.StageSelectSlide();
        while (NowPos.x >= SlidePos.x) //現在の位置が目的位置まで到達していない場合
        {
            NowPos.x -= SlideSpeed; //少しずつ動かす
            TransformSlidePanel.position = NowPos; //現在の位置更新
            yield return null;

            if (NowPos.x < SlidePos.x) //現在の位置が目的地をオーバーしたら
            {
                TransformSlidePanel.position = SlidePos; //現在地を目的地に修正し
                NowMove = false; //動いてる判定OFF
                yield break; //終わり
            }
        }
    }

    public void PushStageButton(Button nameSender) //ステージ選択した際の処理(ボタンアタッチ用) ステージ選択時のボタン、ダイアログのボタンどちらでも同じメソッドを適用する
    {
        if (_audioManager.scene.name == "DontDestroyOnLoad") //オーディオマネージャーが消されない処理されてたら
        {
            SceneManager.MoveGameObjectToScene(_audioManager, SceneManager.GetActiveScene()); //一旦外す
        }
        audioManager.StageSelectSE(); //ステージ選択時のSE再生
        if (nameSender.name == "Yes") //挑戦するかどうかで「はい」選択したら
        {
            audioManager.StageSelectDialogYes(); //「Yes」押したときのSE再生
            topUIManager.StaminaNumber--; //スタミナ1消費
            StartCoroutine("LoadStageScene"); //遷移用のコルーチン再生
        }
        else if(nameSender.name == "No") //挑戦するかどうかで「いいえ」選択したら
        {
            audioManager.StageSelectDialogNo(); //「No」押したときのSE再生
            StageSelectDialog.SetActive(false); //ダイアログ閉じる
        }
        else //ダイアログの選択しじゃない場合(ステージ選択した際)
        {
            string StageName; //変数宣言
            StageName = nameSender.name; //選択したステージのオブジェクト名を取得
            PlayerPrefs.SetString("StageNameSave", StageName); //オブジェクト名を取得する
            StageSelectDialog.SetActive(true); //ダイアログを表示する
            DialogText.text = StageName + "に挑戦しますか？"; //選択中のステージに挑戦するかどうかのテキスト表示
        }
    }

    IEnumerator LoadStageScene()
    {
        yield return new WaitForSeconds(1.0f); //SE終わるまでの秒数指定
        SceneManager.LoadScene("ForTestScene"); //画面遷移
    }
}
