using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubQuestManager : MonoBehaviour
{
    public JsonSubQuestFlag jsonSubQuestFlag; //Jsonのセーブデータを取得するアタッチするための変数
    public SubQuestDatabase subQuestDatabase; //データベースをアタッチするための変数

    Text[] subQuestTitleText = new Text[999]; //サブクエストのタイトル用テキスト
    string[] subQuestOverviewText = new string[999]; //サブクエストの説明用テキスト
    int[] subQuestFlag = new int[999]; //サブクエストフラグ用の変数

    SubQuestDatabase.Summary summary; //配列を取得するための変数

    GameObject _gameObject; //ゲームオブジェクトを入れるための変数

    int count; //カウンタ変数
    int count2; //カウンタ変数
    int count3; //カウンタ変数

    // Start is called before the first frame update
    void Start()
    {
        int count = 0; //初期化
        int count3 = 0; //初期化

        _gameObject = GameObject.Find("SubQuest"+count); //ボタンオブジェクトの取得

        for (count2 = 0; count2 < 999; count2++) //セーブデータ0～999個までのフラグ番号をすべて取得
        {
            subQuestFlag[count2] = jsonSubQuestFlag.saveData.subQuestFlag[count2]; //対応した物を入れていく
        }

        while (_gameObject) //ゲームオブジェクトが存在しているなら
        {
            subQuestTitleText[count] = _gameObject.GetComponentInChildren<Text>(); //そのゲームオブジェクトのテキストコンポーネントを取得する
            summary = subQuestDatabase.list[count]; //データベースの配列番号を指定
            subQuestTitleText[count].text = summary.SubQuestTitle; //先ほど取得したゲームオブジェクトのテキストにデータベースのサブクエスト題名テキストを代入
            subQuestOverviewText[count] = summary.SubQuestOverview; //先ほど取得したゲームオブジェクトのテキストにデータベースのサブクエスト概要テキストを代入
            count++; //カウントを1進める
            _gameObject = GameObject.Find("SubQuest" + count); //次のゲームオブジェクトを取得 ※見つからなかったらループ処理を抜ける
        }

        _gameObject = GameObject.Find("SubQuest" + count3); //ボタンオブジェクトの取得

        while (true) //無限ループ
        {
            switch (subQuestFlag[count3]) //サブクエストのフラグ番号でスイッチ
            {
                case 2: //フラグ番号が2だったら色を変える ※2はフラグ持ちクリア済み番号
                    _gameObject.GetComponent<Image>().color = Color.gray; //灰色になれ
                    break;

                case 0: //フラグ番号が0だったら非アクティブにする ※0はフラグなし番号
                    _gameObject.SetActive(false); //非アクティブにする
                    break; 

                default:
                    break;
            }

            count3++; //カウントを1進める
            _gameObject = GameObject.Find("SubQuest" + count3); //ボタンオブジェクトの取得

            if (_gameObject == null) //もしゲームオブジェクトがなかったら処理を抜ける
            {
                break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushSubQuestButton()
    { 
        /*
        _gameObject = this.gameObject;
        string gameObjectName = _gameObject.name;
        string gameObjectNameReplace = gameObjectName.Replace("SubQuest", "");
        Debug.Log(gameObjectNameReplace);
        count = int.Parse(gameObjectNameReplace);
        GameObject.Find("SummaryText").GetComponent<Text>().text = subQuestOverviewText[count]; //説明欄に説明を入れる
        */
    }

    public void PrintSenderName(Button nameSender) //ネットから拾ってきたもの
    {
        string gameObjectName = nameSender.name; //Onclickにアタッチしたボタンオブジェクトの名前を取得
        string gameObjectNameReplace = gameObjectName.Replace("SubQuest", ""); //名前から数字だけを取り出す
        count = int.Parse(gameObjectNameReplace); //intに変換して代入
        GameObject.Find("SummaryText").GetComponent<Text>().text = subQuestOverviewText[count]; //説明欄に対応した説明を入れる
    }
}
