using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    string StageName;
    GameObject StagePlane;
    public GameObject[] StagePlaneAttach;
    public float[] SlideSpeed;
    GameObject LastStagePlaneAttach;
    GameObject StageStart;
    BallManager ballManager;

    [System.NonSerialized]
    public int ListNumber;

    Transform TransformPlane;
    Vector3 NowPos;
    StageAudioManager stageAudioManager;

    BallCollider ballCollider;

    Text StageStartText;
    Text GameFinishText;
    GameObject GameFinishTextObj;
    GameObject GameFinishBackImageObj;
    GameObject GameFinishDialogImage;
    GameObject TopUIManagerObj;
    TopUIManager topUIManager;
    public GameObject NextOrRetryButtonText;

    int count2 = 3;

    private void Awake()
    {
        TopUIManagerObj = GameObject.Find("TopUIManager");
        topUIManager = TopUIManagerObj.GetComponent<TopUIManager>();
        GameFinishDialogImage = GameObject.Find("GameFinishDialogImage");
        GameFinishBackImageObj = GameObject.Find("GameFinishBackImage");
        GameFinishTextObj = GameObject.Find("GameFinishText");
        GameFinishBackImageObj.SetActive(false);
        GameFinishDialogImage.SetActive(false);
        TopUIManagerObj.SetActive(false);
        ballManager = GameObject.Find("BallManager").GetComponent<BallManager>();
        StageStart = GameObject.Find("StageStartText");
        StageStartText = StageStart.GetComponent<Text>();
        ballCollider = GameObject.Find("Sphere").GetComponent<BallCollider>();
        stageAudioManager = GameObject.Find("StageAudioManager").GetComponent<StageAudioManager>();
        StageName = PlayerPrefs.GetString("StageNameSave");
        StagePlane = GameObject.Find(StageName);
        LastStagePlaneAttach = StagePlaneAttach.Last();
        TransformPlane = StagePlane.transform;
        NowPos = TransformPlane.position;
        int count = 0;

        while (true)
        {

            if (StagePlane == null) //�I�������X�e�[�W���Ȃ������甲����
            {
                break;
            }

            if (StagePlane.name == StagePlaneAttach[count].name) //�I�������X�e�[�W�̃I�u�W�F�N�g�����A�^�b�`�����I�u�W�F�N�g�̖��O����v���Ă���ꍇ
            {
                ListNumber = count; //���Ԗڂ��L�^
                count++; //���̃I�u�W�F�N�g��
                continue;
            }
            StagePlaneAttach[count].SetActive(false); //�I�������X�e�[�W����Ȃ��ꍇ���ꂪ�I������A��\���ɂȂ�
            count++; //���̃I�u�W�F�N�g��

            if (StagePlaneAttach[count] == LastStagePlaneAttach) //�Ō�܂ŃI�u�W�F�N�g�̕\����\�����肪������
            {
                StagePlaneAttach[count].SetActive(false); //�Ō�̃I�u�W�F�N�g������
                break; //�������I����
            }
        }
    }
    void Start()
    {
        StartCoroutine("PlaneMove"); //�X�e�[�W�J�n���̉��o�ƃp�l���𓮂����p�̃R���[�`���Đ�
    }

    // Update is called once per frame
    void Update()
    {
        if (ballCollider.GameOver) //�Q�[���I�[�o�[�ɂȂ�����
        {
            GameFinishTextObj.GetComponent<Text>().text = "Game Over"; //�e�L�X�g��ݒ�
            GameFinishBackImageObj.SetActive(true); //��ʂɃQ�[���I�[�o�[�\��
            stageAudioManager.StopBGM(); //BGM�~�߂�
            StartCoroutine(DisplayDialog(0)); //���g���Cor�߂�_�C�A���O��\��
            StopCoroutine("PlaneMove");
            ballCollider.GameOver = false;
        }

        if(ballCollider.GameClear) //�Q�[���N���A��
        {
            GameFinishTextObj.GetComponent<Text>().text = "Game Clear"; //�e�L�X�g��ݒ�
            GameFinishBackImageObj.SetActive(true); //��ʂɃQ�[���I�[�o�[�\��
            stageAudioManager.StopBGM(); //BGM�~�߂�
            StartCoroutine(DisplayDialog(1)); //���̃X�e�[�Wor�߂�_�C�A���O��\��
            StopCoroutine("PlaneMove");
            ballCollider.GameClear = false;
        }
    }

    IEnumerator PlaneMove()
    {
        while (count2 > 0) //�J�E���g�ϐ���0���傫���ꍇ�J��Ԃ�
        {
            StageStartText.text = count2.ToString(); //���݂̃J�E���g���\��
            yield return new WaitForSeconds(0.9f); //0.9�b�ҋ@
            count2--; //�J�E���g1���炷
            if (count2 == 0) //�J�E���g��0�܂ŗ�����
            {
                StageStartText.text = "Start"; //Start�ƕ\��
                yield return new WaitForSeconds(0.9f); //0.9�b�ҋ@
                StageStart.SetActive(false); //�Q�[���J�n���̃J�E���g��\��
                stageAudioManager.StageBGMStart(); //BGM�X�^�[�g
                ballManager.isTouch = false; //�{�[���𓮂�����悤��
                ballManager.NowMove = false; //�{�[���𓮂�����悤��
                ballManager.CanPlay = true;
            }
        }
        if (ballCollider.GameOver || ballCollider.GameClear) //�Q�[���I�[�o�[���N���A�����Ƃ�
        {
            yield break; //�p�l���𓮂����̂��~�߂�
        }

        while (!ballCollider.GameOver && !ballCollider.GameClear) //�Q�[���I�[�o�[���N���A��Ԃł͂Ȃ��Ƃ�
        {
            NowPos.z -= SlideSpeed[ListNumber] * Time.deltaTime; //�p�l���𓮂���
            TransformPlane.position = NowPos; //���݂̈ʒu�擾
            yield return null;
        }
    }

    IEnumerator DisplayDialog(int i) //0 = �Q�[���I�[�o�[    1 = �N���A
    {
        if(i == 0)
        {
            stageAudioManager.SE(4);
        }
        else
        {
            stageAudioManager.SE(5);
        }
        yield return new WaitForSeconds(1.5f); //�Q�[���I�[�o�[���肩��w��b���҂�
        GameFinishDialogImage.SetActive(true); //�_�C�A���O�\��
        if (i == 0) //GameOver
        {
            NextOrRetryButtonText.GetComponent<Text>().text = "���g���C"; //�{�^���̃e�L�X�g�����g���C��
        }
        else //GameClear
        {
            NextOrRetryButtonText.GetComponent<Text>().text = "���̃X�e�[�W��"; //�{�^���̃e�L�X�g�����̃X�e�[�W�ւ�
        }
        TopUIManagerObj.SetActive(true); //TopUI�\��
        yield break;
    }

    public void PushDialog(Button ButtonName) //�Q�[���t�B�j�b�V����̃{�^���I�����̃��\�b�h(�{�^���A�^�b�`�p)
    {
        if (ButtonName.name == "BackToStageSelectButton") //�X�e�[�W�I����ʂ֖߂�
        {
            stageAudioManager.SE(7);
            PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name); //���݂̃V�[�������Z�[�u��
            SceneManager.LoadScene("Title Scene"); //�^�C�g����ʂɖ߂�(�^�C�g����ʂ̏����ŃX�e�[�W����J�ڂ��ꂽ��X�e�[�W�Z���N�g�ɑJ�ڂ��鏈�������Ă���)
        }
        else if (ballCollider.GameOver)//GameOver�����g���C
        {
            stageAudioManager.SE(6);
            topUIManager.StaminaNumber--; //�X�^�~�i1����
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //�X�e�[�W�X�V
        }
        else //GameClear�����̃X�e�[�W��
        {
            stageAudioManager.SE(6);
            ListNumber++; //�X�e�[�W�i���o�[�����̂�
            StageName = StagePlaneAttach[ListNumber].name; //�X�e�[�W����ۑ�
            PlayerPrefs.SetString("StageNameSave", StageName); //�X�e�[�W����ۑ�
            topUIManager.StaminaNumber--; //�X�^�~�i1����
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //�X�e�[�W�X�V
        }
    }
}
