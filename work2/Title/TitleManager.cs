using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    public GameObject _audioManager;


    private void Awake()
    {
        //�Q�[���t�B�j�b�V���セ�̂܂܃X�e�[�W�Z���N�g�s���ƃI�[�f�B�I�}�l�[�W���[���Ȃ��̂ň�U����������ގb�菈��
        if (PlayerPrefs.GetString("SceneName") == "ForTestScene")
        {
            PlayerPrefs.SetString("SceneName", "�Ȃ�");
            DontDestroyOnLoad(_audioManager);
            SceneManager.LoadScene("StageSelectScene");
        }
    }
    void Start()
    {
        //AudioManager�擾
        AudioManager audioManager = GetComponent<AudioManager>();
    }

    void Update()
    {

        //��ʃ^�b�v������AudioManager�̃��\�b�h���s����SE�炵�ĉ�ʑJ��
        if (Input.GetMouseButtonDown(0))
        {
            _audioManager.GetComponent<AudioManager>().TapToStartSE();
            DontDestroyOnLoad(_audioManager);
            StartCoroutine("LoadStageSelectScene"); //�J�ڗp�̃R���[�`���Đ�
        }
    }

    IEnumerator LoadStageSelectScene()
    {
        yield return new WaitForSeconds(1.0f); //SE�I���܂ł̕b���w��
        SceneManager.LoadScene("StageSelectScene"); //��ʑJ��
    }
}
