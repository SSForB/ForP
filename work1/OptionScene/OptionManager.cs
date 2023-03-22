using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{

    public GameObject slowButton; //遅いボタン
    public GameObject normalButton; //普通ボタン
    public GameObject fastButton; //速いボタン

    public Slider masterSlider; //マスタースライダー
    public Slider bgmSlider; //BGMスライダー
    public Slider seSlider; //SEスライダー

    public Text masterText; //マスター数値のテキスト
    public Text bgmValueText; //bgm数値のテキスト
    public Text seValueText; //se数値のテキスト

    float masterNowValue;
    float seNowValue;
    float bgmNowValue;
    float pushButtonNo;

    SoundManager soundsManager; //soundsManagerスクリプトを取得するための宣言

    AudioSource OptionBGM; //soundsManagerからBGM用のオーディオソースを取得するため宣言
    AudioSource OptionSE; //soundsManagerからSE用のオーディオソースを取得するため宣言
    AudioSource SETest; //SE音量確認用のオーディオソースを取得するための宣言

    void Start()
    {
        //色を全部黄色に初期化
        slowButton.GetComponent<Image>().color = new Color(255.0f, 255.0f, 0.0f, 255.0f);　//遅いボタン黄色
        normalButton.GetComponent<Image>().color = new Color(255.0f, 255.0f, 0.0f, 255.0f); //普通ボタン黄色
        fastButton.GetComponent<Image>().color = new Color(255.0f, 255.0f, 0.0f, 255.0f); //速いボタン黄色

        pushButtonNo = PlayerPrefs.GetFloat("MessageButton", 2.0f);

        SETest = GameObject.Find("SESlider").GetComponent<AudioSource>(); //SEスライダーのオーディオソースを取得
        OptionBGM = GameObject.Find("SoundsManager(DontDestroy)").GetComponent<AudioSource>(); //サウンドマネージャーのBGM用オーディオソースを取得
        soundsManager = GameObject.Find("SoundsManager(DontDestroy)").GetComponent<SoundManager>(); // サウンドマネージャーのスクリプトを取得
        OptionSE = soundsManager.se; //サウンドマネージャーのスクリプト(SoundManager)で「AddComponentしたSE用オーディオソース(se)」の取得

        masterSlider.value = soundsManager.MasterVol;
        bgmSlider.value = soundsManager.BGMVol;
        seSlider.value = soundsManager.SEVol;

        switch(pushButtonNo)
        {
            case 1.0f:
                MessageSlow();
                break;
            case 2.0f:
                MessageNormal();
                break;
            case 3.0f:
                MessageFast();
                break;
        }
    }

    void Update()
    {
        masterNowValue = masterSlider.value; //マスタースライダーの値をmasterNowValueに代入
        bgmNowValue = bgmSlider.value; //BGMスライダーの値をbgmNowValueに代入
        seNowValue = seSlider.value; //マスタースライダーの値をseNowValueに代入

        OptionBGM.volume = ((bgmNowValue / (100  /masterNowValue)) / 100); //((BGMスライダーの値÷(100÷マスターボリュームの値)))÷100) 出た数値をBGMオーディオソースの値に代入　※スライダーの値は1～100に設定してあり、オーディオソースのvolume値は0.001～1.000で設定するため最後に100で割る
        OptionSE.volume = ((seNowValue / (100 / masterNowValue)) / 100); //((SEスライダーの値÷(100÷マスターボリュームの値)))÷100) 出た数値をSEオーディオソースの値に代入 ※スライダーの値は1～100に設定してあり、オーディオソースのvolume値は0.001～1.000で設定するため最後に100で割る
        SETest.volume = ((seNowValue / (100 / masterNowValue)) / 100); //((SEスライダーの値÷(100÷マスターボリュームの値)))÷100) 出た数値をSE音量確認用オーディオソースの値に代入 ※スライダーの値は1～100に設定してあり、オーディオソースのvolume値は0.001～1.000で設定するため最後に100で割る

        masterText.text = masterSlider.value.ToString(); //マスターテキストにBGMスライダーの値を代入　※テキストはstring型でスライダーの値はfloatのため変換
        bgmValueText.text = bgmSlider.value.ToString(); //BGMテキストにBGMスライダーの値を代入　※テキストはstring型でスライダーの値はfloatのため変換
        seValueText.text = seSlider.value.ToString(); //SEテキストにSEスライダーの値を代入　※テキストはstring型でスライダーの値はfloatのため変換
    }

    public void MessageSlow() //遅いボタンを押したとき
    {
        slowButton.GetComponent<Image>().color = new Color(0.0f, 255.0f, 255.0f, 255.0f);　//遅いボタン水色
        normalButton.GetComponent<Image>().color = new Color(255.0f, 255.0f, 0.0f, 255.0f); //普通ボタン黄色
        fastButton.GetComponent<Image>().color = new Color(255.0f, 255.0f, 0.0f, 255.0f); //速いボタン黄色

        if (pushButtonNo != 1.0f)
        {
            OptionSE.clip = soundsManager.ChangeMessageSpeed; //SE用オーディオソースのクリップにChangeMassageSpeedの音源を代入
            OptionSE.Play(); //SE用オーディオソースを再生
        }
 

        pushButtonNo = 1.0f;

        //この下に速さの指定とセーブデータ処理を書く予定
    }

    public void MessageNormal() //普通ボタンを押したとき
    {
        slowButton.GetComponent<Image>().color = new Color(255.0f, 255.0f, 0.0f, 255.0f);　//遅いボタン黄色
        normalButton.GetComponent<Image>().color = new Color(0.0f, 255.0f, 255.0f, 255.0f); //普通ボタン水色
        fastButton.GetComponent<Image>().color = new Color(255.0f, 255.0f, 0.0f, 255.0f); //速いボタン黄色

        if (pushButtonNo != 2.0f)
        {
            OptionSE.clip = soundsManager.ChangeMessageSpeed; //SE用オーディオソースのクリップにChangeMassageSpeedの音源を代入
            OptionSE.Play(); //SE用オーディオソースを再生
        }

        pushButtonNo = 2.0f;

        //この下に速さの指定とセーブデータ処理を書く予定
    }

    public void MessageFast() //速いボタンを押したとき
    {
        slowButton.GetComponent<Image>().color = new Color(255.0f, 255.0f, 0.0f, 255.0f);　//遅いボタン黄色
        normalButton.GetComponent<Image>().color = new Color(255.0f, 255.0f, 0.0f, 255.0f); //普通ボタン黄色
        fastButton.GetComponent<Image>().color = new Color(0.0f, 255.0f, 255.0f, 255.0f); //速いボタン水色

        if (pushButtonNo != 3.0f)
        {
            OptionSE.clip = soundsManager.ChangeMessageSpeed; //SE用オーディオソースのクリップにChangeMassageSpeedの音源を代入
            OptionSE.Play(); //SE用オーディオソースを再生
        }

        pushButtonNo = 3.0f;

        //この下に速さの指定とセーブデータ処理を書く予定
    }

    public void PushReturnToTitleButton() //タイトルへ戻るボタンを押したとき
    {
        PlayerPrefs.SetFloat("MasterVolume", masterNowValue);
        PlayerPrefs.SetFloat("BGMVolume", bgmNowValue);
        PlayerPrefs.SetFloat("SEVolume", seNowValue);
        PlayerPrefs.SetFloat("MessageButton", pushButtonNo);
        PlayerPrefs.Save();

        SceneManager.LoadScene("TitleScene"); //タイトル画面に遷移
        OptionSE.clip = soundsManager.OptionToTitle; //サウンドマネージャのパブリックオーディオクリップ「OptionToTitle」をSE用オーディオソースにセット
        OptionSE.Play(); //SE用オーディオソースを再生
    }
}
