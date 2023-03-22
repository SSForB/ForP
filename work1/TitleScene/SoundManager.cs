using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    static public SoundManager instance = null; //シングルトンにしてすべてのシーンで使えるようにするための宣言

    [Header("タイトルシーン用BGMクリップ")]
    public AudioClip TitleScene; //タイトルシーン用BGM

    [Header("タイトルシーン用SEクリップ")]
    public AudioClip TitleToLoadGame; //LoadGame押したときのSE
    public AudioClip TitleToNewGame; //NewGame押したときのSE
    public AudioClip TitleToOption; //Option押したときのSE

    [Header("オプションシーン用SEクリップ")] 
    public AudioClip OptionToTitle; //タイトルへ戻るを押したときのSE
    public AudioClip ChangeMessageSpeed; //メッセージスピード変更ボタンを押したときのSE

    [Header("ステージ1用BGMクリップ")]
    public AudioClip Stage1Scene;

    [Header("メニュー用SEクリップ")]
    public AudioClip MenuSwitch; //メニュー開閉時のSE
    public AudioClip MenuButton; //メニューボタン押下時のSE

    [Header("AddComponent用なのでいじるの厳禁")] 
    public AudioSource se; //SE用オーディオソース取得のために宣言

    public float MasterVol;
    public float SEVol;
    public float BGMVol;

    void Awake()
    {
        if (instance == null) //もしインスタンスにnullが入っていたら(初期状態)
        {
            instance = this; //このオブジェクトを格納し
            DontDestroyOnLoad(gameObject); //ロードでなくならないようにする
        }
        else //もしinstanceに何か入っていたら(同じシーンに戻るなどして新たに同じオブジェクトが作られたとき、instanceの中身は入っているのでelseになる)
        {
            Destroy(gameObject); //新たに作られた同じオブジェクトを削除
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this); //シーンが変わってもオブジェクトが破棄されないように

        MasterVol = PlayerPrefs.GetFloat("MasterVolume", 50f);
        SEVol = PlayerPrefs.GetFloat("SEVolume", 50f);
        BGMVol = PlayerPrefs.GetFloat("BGMVolume", 50f);

        se = gameObject.AddComponent<AudioSource>(); //SE用にもう１つオーディオソースを追加し取得
        AudioSource bgm = GetComponent<AudioSource>(); //BGM用オーディオソースを取得

        bgm.volume = ((BGMVol / (100 / MasterVol)) / 100);
        se.volume = ((SEVol / (100 / MasterVol)) / 100);
    }

    // Update is called once per frame
    void Update()
    {
        MasterVol = PlayerPrefs.GetFloat("MasterVolume", 50f);
        SEVol = PlayerPrefs.GetFloat("SEVolume", 50f);
        BGMVol = PlayerPrefs.GetFloat("BGMVolume", 50f);
    }
}
