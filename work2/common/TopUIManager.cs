using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TopUIManager : MonoBehaviour
{
    public Sprite StaminaON;
    public Sprite StaminaOFF;
    public int StaminaNumber;
    int PreStaminaNumber;
    int StaminaUpdateTimeSecondsDif;
    int StaminaUpdateTimeMinutesDif;
    public int StaminaMinutesTime;
    public int StaminaSecondsTime;
    Image Stamina1;
    Image Stamina2;
    Image Stamina3;
    Image Stamina4;
    Image Stamina5;
    Text StaminaCoolTime;
    DateTime NowTime;
    DateTime StaminaUpdateTime;
    TimeSpan StaminaUpdateDif;
    TimeSpan AddTime;
    bool WaitStaminaUpdate;
    bool AlreadyCount;
    public GameObject StaminaCoolTimeText;
    float StaminaUpdateSeconds;
    float StaminaUpdateMinutes;
    float testAbs;
    float test;
    float test2 = 0.0f;

    private void Awake()
    {
        NowTime = DateTime.Now; //現在の時刻取得
        AddTime = new TimeSpan(0, 0, StaminaMinutesTime, StaminaSecondsTime); //スタミナ回復時間設定用
        string defult = new DateTime(2000, 1, 1, 16, 32, 0, DateTimeKind.Local).ToBinary().ToString();　//保存していたデータが存在しない時用のデフォルト値
        string StaminaUpdateTimeString = PlayerPrefs.GetString("StaminaUpdateTime", defult); //前回保存していたスタミナ時間をstringで読み込み
        StaminaUpdateTime = System.DateTime.FromBinary(System.Convert.ToInt64(StaminaUpdateTimeString)); //stringデータをdatetimeに変換
        StaminaUpdateDif = StaminaUpdateTime - NowTime; //スタミナ回復時間と現在の時間を差異を計算
        StaminaUpdateSeconds = StaminaUpdateDif.Seconds; //差異の秒数抽出
        StaminaUpdateMinutes = StaminaUpdateDif.Minutes; //差異の分数抽出
        test = StaminaUpdateSeconds + (StaminaUpdateMinutes * 60); //秒数にしてトータル秒数を出す
        testAbs = Mathf.Abs(test); //絶対値にしてマイナス値を排除
        float testdif = testAbs / (StaminaSecondsTime + (StaminaMinutesTime * 60)); //スタミナ回復時間で割ってどのくらいオーバーしてるか計算
        testdif = Mathf.Floor(testdif); //小数点切り捨て

        StaminaNumber = PlayerPrefs.GetInt("Stamina", 5); //前回保存時のスタミナ数を読み込み
        PreStaminaNumber = StaminaNumber; //スタミナ更新されたことを感知するための代入

        if (testdif >= 1 && test <= 0) //割った時の答えが1以上のとき
        {
            StaminaNumber += (int)testdif; //超えた分スタミナ回復

            if(StaminaNumber <= 4) //スタミナ4以下のとき
            {
                while(test2 <= 0.0f)
                {
                    Debug.Log("処理走ったよ");
                    AlreadyCount = true;
                    StaminaUpdateTime += AddTime;
                    StaminaUpdateDif = StaminaUpdateTime - NowTime;
                    StaminaUpdateSeconds = StaminaUpdateDif.Seconds; //差異の秒数抽出
                    StaminaUpdateMinutes = StaminaUpdateDif.Minutes; //差異の分数抽出
                    test = StaminaUpdateSeconds + (StaminaUpdateMinutes * 60); //秒数にしてトータル秒数を出す
                    Debug.Log(test);
                    test2 = Mathf.Sign(test);
                }
            }

            if (StaminaNumber >= 5) //スタミナ6以上になった時の処理
            {
                StaminaNumber = 5;
                PlayerPrefs.SetInt("Stamina", StaminaNumber);
            }
        }

        if(StaminaNumber < 5) //スタミナが4以下の時は回復時計が進むためのフラグON
        {
            WaitStaminaUpdate = true;
        }

        Stamina1 = GameObject.Find("Stamina1Image").GetComponent<Image>(); //スタミナアイコン用
        Stamina2 = GameObject.Find("Stamina2Image").GetComponent<Image>(); //スタミナアイコン用
        Stamina3 = GameObject.Find("Stamina3Image").GetComponent<Image>(); //スタミナアイコン用
        Stamina4 = GameObject.Find("Stamina4Image").GetComponent<Image>(); //スタミナアイコン用
        Stamina5 = GameObject.Find("Stamina5Image").GetComponent<Image>(); //スタミナアイコン用
        StaminaCoolTime = GameObject.Find("StaminaCoolTimeText").GetComponent<Text>(); //時間表示テキストの取得
        StaminaUpdate(); //スタミナアイコン更新
    }
    void Start()
    {

    }

    void Update()
    {

        if(StaminaNumber < 0) //スタミナがマイナスになってしまった時用の念のための処理
        {
            StaminaNumber = 0;
            PreStaminaNumber = StaminaNumber;
            PlayerPrefs.SetInt("Stamina", StaminaNumber);
        }

        if(StaminaNumber > 5) //スタミナがオーバーしてしまった時用の念のための処理
        {
            StaminaNumber = 5;
            PreStaminaNumber = StaminaNumber;
            PlayerPrefs.SetInt("Stamina", StaminaNumber);
        }

        NowTime = DateTime.Now; //現在の時刻取得

        StaminaUpdateDif = StaminaUpdateTime - NowTime; //スタミナ回復時間と現在の時間を差異を計算
        StaminaCoolTime.text = StaminaUpdateDif.ToString(); //回復までの時間をテキストにstringで表示
        StaminaUpdateTimeSecondsDif = StaminaUpdateTime.Second - NowTime.Second; //スタミナ回復までの秒数を計算
        StaminaUpdateTimeMinutesDif = StaminaUpdateTime.Minute - NowTime.Minute; //スタミナ回復までの分数を計算

        if (StaminaUpdateTimeSecondsDif == 0 && WaitStaminaUpdate && StaminaUpdateTimeMinutesDif == 0) //残り分数も秒数も0かつ、スタミナアップデートを待っているとき
        {
            WaitStaminaUpdate = false;
            AlreadyCount = false;
            StaminaNumber++; //スタミナ+1
        }

        if (StaminaNumber == 5) //スタミナ5のとき
        {
            StaminaCoolTimeText.SetActive(false); //回復時間非表示
        }

        if (PreStaminaNumber != StaminaNumber) //スタミナ更新されたとき
        {
            PreStaminaNumber = StaminaNumber; //スタミナ更新されたことを感知するため変数に代入
            PlayerPrefs.SetInt("Stamina", StaminaNumber); //データ保存
            StaminaUpdate(); //スタミナアイコン更新

            if(StaminaNumber <= 4 && !AlreadyCount) //スタミナ4以下かつまだカウントが進んでいないとき ※すでに回復時間が進んでいるときに新たに更新すると時間が最初からになってしまうためフラグ管理
            {
                StaminaCoolTimeText.SetActive(true); //回復時間表示
                StaminaUpdateTime = NowTime + AddTime; //現在時刻にスタミナ回復時間を足した時間を算出
                PlayerPrefs.SetString("StaminaUpdateTime", StaminaUpdateTime.ToBinary().ToString()); //スタミナ回復時刻を保存
                WaitStaminaUpdate = true; //スタミナアップデート待ちON
                AlreadyCount = true; //カウント進んでいるので新たに更新があってもここの処置は走らないようにする
            }
        }
    }

    public void DebugStaminaButton() //デバッグ用カウント-1メソッド
    {
        StaminaNumber--;
        PlayerPrefs.SetInt("Stamina", StaminaNumber);
    }

    void StaminaUpdate()
    {
        switch (StaminaNumber) //スタミナによってアイコン切り替え
        {
            case 0:
                Stamina1.sprite = StaminaOFF;
                Stamina2.sprite = StaminaOFF;
                Stamina3.sprite = StaminaOFF;
                Stamina4.sprite = StaminaOFF;
                Stamina5.sprite = StaminaOFF;
                break;

            case 1:
                Stamina1.sprite = StaminaON;
                Stamina2.sprite = StaminaOFF;
                Stamina3.sprite = StaminaOFF;
                Stamina4.sprite = StaminaOFF;
                Stamina5.sprite = StaminaOFF;
                break;

            case 2:
                Stamina1.sprite = StaminaON;
                Stamina2.sprite = StaminaON;
                Stamina3.sprite = StaminaOFF;
                Stamina4.sprite = StaminaOFF;
                Stamina5.sprite = StaminaOFF;
                break;

            case 3:
                Stamina1.sprite = StaminaON;
                Stamina2.sprite = StaminaON;
                Stamina3.sprite = StaminaON;
                Stamina4.sprite = StaminaOFF;
                Stamina5.sprite = StaminaOFF;
                break;

            case 4:
                Stamina1.sprite = StaminaON;
                Stamina2.sprite = StaminaON;
                Stamina3.sprite = StaminaON;
                Stamina4.sprite = StaminaON;
                Stamina5.sprite = StaminaOFF;
                break;

            case 5:
                Stamina1.sprite = StaminaON;
                Stamina2.sprite = StaminaON;
                Stamina3.sprite = StaminaON;
                Stamina4.sprite = StaminaON;
                Stamina5.sprite = StaminaON;
                break;
        }
    }
}
