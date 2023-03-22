using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [Header("タイトルとステージ選択画面のBGM")]
    public AudioClip titleBGM;
    [Header("タイトル画面タップ時のSE")]
    public AudioClip titleTapSE;
    [Header("ステージ選択時のSE")]
    public AudioClip stageSelectSE;
    [Header("ステージ選択ダイアログで「はい」押したときのSE")]
    public AudioClip StageSelect_YesSE;
    [Header("ステージ選択ダイアログで「いいえ」押したときのSE")]
    public AudioClip StageSelect_NoSE;
    [Header("ステージ切り替えスライドしたときのSE")]
    public AudioClip StageSelectSlideSE;
    [Header("控え")]
    public AudioClip SE4;
    [Header("控え")]
    public AudioClip SE5;
    [Header("控え")]
    public AudioClip SE6;
    [Header("控え")]

    [Header("0.001～1の間で数値を記載してください。中央値は0.5です。")]
    public float BGMVolume;
    public float SEVolume;

    [HideInInspector]
    public AudioSource bgm;
    [HideInInspector]
    public AudioSource se;


    private void Awake()
    {

        //BGMのオーディオソース取得
        bgm = GetComponent<AudioSource>();
        AudioClip audioClipBGM = bgm.GetComponent<AudioClip>();
        se = gameObject.AddComponent<AudioSource>();

        bgm.volume = BGMVolume;
        se.volume = SEVolume;

        //タイトル画面だったらタイトル画面のBGMを流す
        if (SceneManager.GetActiveScene().name == "Title Scene")
        {
            bgm.clip = titleBGM;
            bgm.Play();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    
    public void TapToStartSE()　//SE鳴らす用のメソッド(外部から呼ぶ)
    {
        se.clip = titleTapSE;
        se.Play();
    }

    public void StageSelectSE()　//SE鳴らす用のメソッド(外部から呼ぶ)
    {
        se.clip = stageSelectSE;
        se.Play();
    }

    public void StageSelectSlide()　//SE鳴らす用のメソッド(外部から呼ぶ)
    {
        se.clip = StageSelectSlideSE;
        se.Play();
    }

    public void StageSelectDialogYes()　//SE鳴らす用のメソッド(外部から呼ぶ)
    {
        se.clip = StageSelect_YesSE;
        se.Play();
    }
    
    public void StageSelectDialogNo()　//SE鳴らす用のメソッド(外部から呼ぶ)
    {
        se.clip = StageSelect_NoSE;
        se.Play();
    }
}