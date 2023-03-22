using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [Header("�^�C�g���ƃX�e�[�W�I����ʂ�BGM")]
    public AudioClip titleBGM;
    [Header("�^�C�g����ʃ^�b�v����SE")]
    public AudioClip titleTapSE;
    [Header("�X�e�[�W�I������SE")]
    public AudioClip stageSelectSE;
    [Header("�X�e�[�W�I���_�C�A���O�Łu�͂��v�������Ƃ���SE")]
    public AudioClip StageSelect_YesSE;
    [Header("�X�e�[�W�I���_�C�A���O�Łu�������v�������Ƃ���SE")]
    public AudioClip StageSelect_NoSE;
    [Header("�X�e�[�W�؂�ւ��X���C�h�����Ƃ���SE")]
    public AudioClip StageSelectSlideSE;
    [Header("�T��")]
    public AudioClip SE4;
    [Header("�T��")]
    public AudioClip SE5;
    [Header("�T��")]
    public AudioClip SE6;
    [Header("�T��")]

    [Header("0.001�`1�̊ԂŐ��l���L�ڂ��Ă��������B�����l��0.5�ł��B")]
    public float BGMVolume;
    public float SEVolume;

    [HideInInspector]
    public AudioSource bgm;
    [HideInInspector]
    public AudioSource se;


    private void Awake()
    {

        //BGM�̃I�[�f�B�I�\�[�X�擾
        bgm = GetComponent<AudioSource>();
        AudioClip audioClipBGM = bgm.GetComponent<AudioClip>();
        se = gameObject.AddComponent<AudioSource>();

        bgm.volume = BGMVolume;
        se.volume = SEVolume;

        //�^�C�g����ʂ�������^�C�g����ʂ�BGM�𗬂�
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

    
    public void TapToStartSE()�@//SE�炷�p�̃��\�b�h(�O������Ă�)
    {
        se.clip = titleTapSE;
        se.Play();
    }

    public void StageSelectSE()�@//SE�炷�p�̃��\�b�h(�O������Ă�)
    {
        se.clip = stageSelectSE;
        se.Play();
    }

    public void StageSelectSlide()�@//SE�炷�p�̃��\�b�h(�O������Ă�)
    {
        se.clip = StageSelectSlideSE;
        se.Play();
    }

    public void StageSelectDialogYes()�@//SE�炷�p�̃��\�b�h(�O������Ă�)
    {
        se.clip = StageSelect_YesSE;
        se.Play();
    }
    
    public void StageSelectDialogNo()�@//SE�炷�p�̃��\�b�h(�O������Ă�)
    {
        se.clip = StageSelect_NoSE;
        se.Play();
    }
}