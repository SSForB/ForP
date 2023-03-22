using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectSceneManager : MonoBehaviour
{

    Vector3 ClickStartPosition;
    Vector3 ClickGoalPosition;
    Vector3 ClickPositionDif;
    Vector3 SlidePos;
    Vector3 NowPos;

    Transform TransformSlidePanel;

    public float FlickJudgeDistance;
    public float SlideSpeed;
    float FlickDistanceX;
    int NowStage;
    Text DialogText;

    public GameObject SlidePanelCanvas;
    public GameObject StageSelectDialog;
    public AudioManager audioManager;
    public TopUIManager topUIManager;
    GameObject _audioManager;

    bool NowMove;

    void Start()
    {
        DialogText = GameObject.Find("DialogText").GetComponent<Text>(); //�Q�[���t�B�j�b�V����̃_�C�A���O�p�e�L�X�g�擾
        topUIManager = GameObject.Find("TopUIManager").GetComponent<TopUIManager>(); //TopUI��script�擾
        _audioManager = GameObject.Find("AudioManager"); //BGM�p�Q�[���I�u�W�F�N�g�擾
        audioManager = _audioManager.GetComponent<AudioManager>(); //script�擾

        TransformSlidePanel = SlidePanelCanvas.transform; //canvas�̌��݈ʒu���擾
        NowStage = 1; //���̓X�e�[�W1��\����
        NowMove = false; //���̓X���C�h���ł͂Ȃ�
        StageSelectDialog.SetActive(false); //�X�e�[�W�I����̃_�C�A���O���\��
    }

    void Update()
    {
        if(NowMove) //�����X���C�h����������
        {
            return; //�X���C�h���I���܂œ��������������s�����Ȃ�
        }

        if (Input.GetMouseButtonDown(0)) //��ʂ��^�b�v�����Ƃ�
        {
            ClickStartPosition = Input.mousePosition; //�������ʒu���L�^
        }

        if (Input.GetMouseButtonUp(0)) //��ʃ^�b�v�𗣂����Ƃ�
        {
            ClickGoalPosition = Input.mousePosition; //�������ʒu���L�^
            ClickPositionDif = ClickGoalPosition - ClickStartPosition; //�������ʒu�Ɨ������ʒu�̍����o��
            FlickDistanceX = Mathf.Abs(ClickPositionDif.x); //��Βl�ɂ��}�C�i�X�l��r��
        }

        if(FlickDistanceX >= FlickJudgeDistance) //�t���b�N�������A�ݒ肵���t���b�N���苗�����傫��������
        {
            if(ClickPositionDif.x > 0 && NowStage >= 2)//�������Ƀt���b�N���A�X�e�[�W2���X�e�[�W3�\������������
            {
                NowMove = true; //�����Ă锻��ON
                TransformSlidePanel = SlidePanelCanvas.transform; //canvas�̌��݂̈ʒu�擾
                SlidePos = TransformSlidePanel.position; //canvas�̌��݂̈ʒu�擾
                NowPos = TransformSlidePanel.position; //���݂̈ʒu�p�̕ϐ��Ɉʒu����
                SlidePos.x += 205.3f; //���݂̈ʒu����E�����Ƀp�l���P�����̋��������Z(�p�l�����E�ɓ������߉�ʂł͍��̃X�e�[�W�������ɗ���)
                StartCoroutine("PanelMoveLeft"); //�R���[�`���œ����Ă�悤�ȃA�j���[�V����������
                NowStage--; //���ݕ\�����̃X�e�[�W�ϐ�����
                ClickPositionDif.x = 0; //�N���b�N�������Ƃ����ĂȂ����Ƃɏ�����
            }

            if (ClickPositionDif.x < 0 && NowStage <= 2)//�E�����Ƀt���b�N���A�X�e�[�W1���X�e�[�W2�\������������
            {
                NowMove = true; //�����Ă锻��ON
                TransformSlidePanel = SlidePanelCanvas.transform; //canvas�̌��݂̈ʒu�擾
                SlidePos = TransformSlidePanel.position; //canvas�̌��݂̈ʒu�擾
                NowPos = TransformSlidePanel.position; //���݂̈ʒu�p�̕ϐ��Ɉʒu����
                SlidePos.x -= 205.3f; //���݂̈ʒu����E�����Ƀp�l���P�����̋��������Z(�p�l�������ɓ������߉�ʂł͉E�̃X�e�[�W�������ɗ���)
                StartCoroutine("PanelMoveRight"); //�R���[�`���œ����Ă�悤�ȃA�j���[�V����������
                NowStage++; //���ݕ\�����̃X�e�[�W�ϐ�����
                ClickPositionDif.x = 0; //�N���b�N�������Ƃ����ĂȂ����Ƃɏ�����
            }
        }
    }

    IEnumerator PanelMoveLeft () //���ɂ���p�l���������ɗ���
    {
        audioManager.StageSelectSlide();
        while (NowPos.x <= SlidePos.x) //���݂̈ʒu���ړI�ʒu�܂œ��B���Ă��Ȃ��ꍇ
        {
            NowPos.x += SlideSpeed; //������������
            TransformSlidePanel.position = NowPos; //���݂̈ʒu�X�V
            yield return null;

            if (NowPos.x > SlidePos.x) //���݂̈ʒu���ړI�n���I�[�o�[������
            {
                TransformSlidePanel.position = SlidePos; //���ݒn��ړI�n�ɏC����
                NowMove = false; //�����Ă锻��OFF
                yield break; //�I���
            }
        }
    }

    IEnumerator PanelMoveRight() //�E�ɂ���p�l���������ɗ���
    {
        audioManager.StageSelectSlide();
        while (NowPos.x >= SlidePos.x) //���݂̈ʒu���ړI�ʒu�܂œ��B���Ă��Ȃ��ꍇ
        {
            NowPos.x -= SlideSpeed; //������������
            TransformSlidePanel.position = NowPos; //���݂̈ʒu�X�V
            yield return null;

            if (NowPos.x < SlidePos.x) //���݂̈ʒu���ړI�n���I�[�o�[������
            {
                TransformSlidePanel.position = SlidePos; //���ݒn��ړI�n�ɏC����
                NowMove = false; //�����Ă锻��OFF
                yield break; //�I���
            }
        }
    }

    public void PushStageButton(Button nameSender) //�X�e�[�W�I�������ۂ̏���(�{�^���A�^�b�`�p) �X�e�[�W�I�����̃{�^���A�_�C�A���O�̃{�^���ǂ���ł��������\�b�h��K�p����
    {
        if (_audioManager.scene.name == "DontDestroyOnLoad") //�I�[�f�B�I�}�l�[�W���[��������Ȃ���������Ă���
        {
            SceneManager.MoveGameObjectToScene(_audioManager, SceneManager.GetActiveScene()); //��U�O��
        }
        audioManager.StageSelectSE(); //�X�e�[�W�I������SE�Đ�
        if (nameSender.name == "Yes") //���킷�邩�ǂ����Łu�͂��v�I��������
        {
            audioManager.StageSelectDialogYes(); //�uYes�v�������Ƃ���SE�Đ�
            topUIManager.StaminaNumber--; //�X�^�~�i1����
            StartCoroutine("LoadStageScene"); //�J�ڗp�̃R���[�`���Đ�
        }
        else if(nameSender.name == "No") //���킷�邩�ǂ����Łu�������v�I��������
        {
            audioManager.StageSelectDialogNo(); //�uNo�v�������Ƃ���SE�Đ�
            StageSelectDialog.SetActive(false); //�_�C�A���O����
        }
        else //�_�C�A���O�̑I��������Ȃ��ꍇ(�X�e�[�W�I��������)
        {
            string StageName; //�ϐ��錾
            StageName = nameSender.name; //�I�������X�e�[�W�̃I�u�W�F�N�g�����擾
            PlayerPrefs.SetString("StageNameSave", StageName); //�I�u�W�F�N�g�����擾����
            StageSelectDialog.SetActive(true); //�_�C�A���O��\������
            DialogText.text = StageName + "�ɒ��킵�܂����H"; //�I�𒆂̃X�e�[�W�ɒ��킷�邩�ǂ����̃e�L�X�g�\��
        }
    }

    IEnumerator LoadStageScene()
    {
        yield return new WaitForSeconds(1.0f); //SE�I���܂ł̕b���w��
        SceneManager.LoadScene("ForTestScene"); //��ʑJ��
    }
}
