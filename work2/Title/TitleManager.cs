using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    public GameObject _audioManager;


    private void Awake()
    {
        //ゲームフィニッシュ後そのままステージセレクト行くとオーディオマネージャーがないので一旦こちらを挟む暫定処理
        if (PlayerPrefs.GetString("SceneName") == "ForTestScene")
        {
            PlayerPrefs.SetString("SceneName", "なし");
            DontDestroyOnLoad(_audioManager);
            SceneManager.LoadScene("StageSelectScene");
        }
    }
    void Start()
    {
        //AudioManager取得
        AudioManager audioManager = GetComponent<AudioManager>();
    }

    void Update()
    {

        //画面タップしたらAudioManagerのメソッド実行してSE鳴らして画面遷移
        if (Input.GetMouseButtonDown(0))
        {
            _audioManager.GetComponent<AudioManager>().TapToStartSE();
            DontDestroyOnLoad(_audioManager);
            StartCoroutine("LoadStageSelectScene"); //遷移用のコルーチン再生
        }
    }

    IEnumerator LoadStageSelectScene()
    {
        yield return new WaitForSeconds(1.0f); //SE終わるまでの秒数指定
        SceneManager.LoadScene("StageSelectScene"); //画面遷移
    }
}
