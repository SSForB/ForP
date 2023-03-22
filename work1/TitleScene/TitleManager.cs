using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Text highScoreText; //ハイスコア用テキストの宣言
    public int x; //仮

    SoundManager soundManager; //スクリプト(SoundManager)を取得するための宣言
    AudioSource OptionBGM; //BGM用オーディオソース取得用の宣言
    AudioSource OptionSE; //SE用オーディオソース取得用の宣言

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SoundsManager(DontDestroy)").GetComponent<SoundManager>(); //サウンドマネージャのスクリプト(SoundManager)を取得　※SE用オーディオソース「se」を読み込むため
        OptionBGM = GameObject.Find("SoundsManager(DontDestroy)").GetComponent<AudioSource>(); //BGM用オーディオソースを取得
        OptionSE = soundManager.se; //SE用オーディオソースを取得

        highScoreText.text = "HighScore:" + x + "days"; //仮

        if (OptionBGM.clip != soundManager.TitleScene)
        {
            OptionBGM.clip = soundManager.TitleScene; //タイトルBGMのセット
            OptionBGM.Play(); //再生
        }

        if (!OptionBGM.isPlaying) //もしオプション画面のBGMが再生中でなければ
        {
            OptionBGM.clip = soundManager.TitleScene; //タイトルBGMのセット
            OptionBGM.Play(); //再生
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushLoadGameButton() //LoadGameを押した時
    {
        OptionSE.clip = soundManager.TitleToLoadGame; //SE用オーディオソースのクリップに「TitleToLoadGame」をセット
        OptionSE.Play(); //SE用オーディオソースを再生
        StartCoroutine("WaitingLoadSE"); //コルーチンスタート
    }

    public void PushNewGameButton() //NewGameを押した時
    {
        OptionSE.clip = soundManager.TitleToNewGame; //SE用オーディオソースのクリップに「TitleToNewGame」をセット
        OptionSE.Play(); //SE用オーディオソースを再生
        StartCoroutine("WaitingNewSE"); //コルーチンスタート
    }
    
    public void PushOptionButton() //Optionを押した時
    {
        OptionSE.clip = soundManager.TitleToOption; //SE用オーディオソースのクリップに「TitleToNewGame」をセット
        OptionSE.Play(); //SE用オーディオソースを再生
        StartCoroutine("WaitingOptionSE"); //コルーチンスタート
    }

    public void PushDateResetButton()
    {
        PlayerPrefs.DeleteAll();
    }

    private IEnumerator WaitingLoadSE() //LoadGame押したときにSEが終了するのを待ってから画面遷移する
    {
        while(true)
        {
            yield return new WaitForFixedUpdate();
            if(!OptionSE.isPlaying)
            {
                break;
            }
        }
        SceneManager.LoadScene("Stage1");
    }

    private IEnumerator WaitingNewSE() //NewGame押したときにSEが終了するのを待ってから画面遷移する
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!OptionSE.isPlaying)
            {
                break;
            }
        }
        SceneManager.LoadScene("Stage1");
    }

    private IEnumerator WaitingOptionSE() //Option押したときにSEが終了するのを待ってから画面遷移する
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!OptionSE.isPlaying)
            {
                break;
            }
        }
        SceneManager.LoadScene("OptionScene");
    }
}
