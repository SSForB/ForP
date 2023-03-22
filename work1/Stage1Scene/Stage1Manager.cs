using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Manager : MonoBehaviour
{
    public GameObject menu;
    bool menuON = false;

    SoundManager soundManager; //スクリプト(SoundManager)を取得するための宣言
    AudioSource Stage1BGM; //BGM用オーディオソース取得用の宣言
    AudioSource Stage1SE; //SE用オーディオソース取得用の宣言

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SoundsManager(DontDestroy)").GetComponent<SoundManager>(); //サウンドマネージャのスクリプト(SoundManager)を取得　※SE用オーディオソース「se」を読み込むため
        Stage1BGM = GameObject.Find("SoundsManager(DontDestroy)").GetComponent<AudioSource>(); //BGM用オーディオソースを取得
        Stage1SE = soundManager.se; //SE用オーディオソースを取得

        Stage1BGM.clip = soundManager.Stage1Scene;
        Stage1BGM.Play();

        PlayerPrefs.SetString("StageName", "始まりの村");
        PlayerPrefs.SetString("StageSceneName", "Stage1");
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Stage1SE.clip = soundManager.MenuSwitch;
            Stage1SE.Play();

            if (!menuON)
            {
                menu.SetActive(true);
                menuON = true;
            }
            else
            {
                menu.SetActive(false);
                menuON = false;
            }
        }
    }

    public void PushMapButton()
    {
        Stage1SE.clip = soundManager.MenuButton;
        Stage1SE.Play();
        SceneManager.LoadScene("MapScene");
    }

    public void PushWeaponButton()
    {
        Stage1SE.clip = soundManager.MenuButton;
        Stage1SE.Play();
        SceneManager.LoadScene("TitleScene");
    }

    public void PushArmorButton()
    {
        Stage1SE.clip = soundManager.MenuButton;
        Stage1SE.Play();
        SceneManager.LoadScene("TitleScene");
    }

    public void PushMaterialButton()
    {
        Stage1SE.clip = soundManager.MenuButton;
        Stage1SE.Play();
        SceneManager.LoadScene("TitleScene");
    }

    public void PushFurnitureButton()
    {
        Stage1SE.clip = soundManager.MenuButton;
        Stage1SE.Play();
        SceneManager.LoadScene("TitleScene");
    }

    public void PushMercenaryButton()
    {
        Stage1SE.clip = soundManager.MenuButton;
        Stage1SE.Play();
        SceneManager.LoadScene("TitleScene");
    }

    public void PushBookButton()
    {
        Stage1SE.clip = soundManager.MenuButton;
        Stage1SE.Play();
        SceneManager.LoadScene("TitleScene");
    }
}

