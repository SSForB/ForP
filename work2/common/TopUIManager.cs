using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TopUIManager : MonoBehaviour
{
    public Sprite StaminaON;
    public Sprite StaminaOFF;
    public int StaminaNumber;
    int PreStaminaNumber;
    int StaminaUpdateTimeSecondsDif;
    int StaminaUpdateTimeMinutesDif;
    public int StaminaMinutesTime;
    public int StaminaSecondsTime;
    Image Stamina1;
    Image Stamina2;
    Image Stamina3;
    Image Stamina4;
    Image Stamina5;
    Text StaminaCoolTime;
    DateTime NowTime;
    DateTime StaminaUpdateTime;
    TimeSpan StaminaUpdateDif;
    TimeSpan AddTime;
    bool WaitStaminaUpdate;
    bool AlreadyCount;
    public GameObject StaminaCoolTimeText;
    float StaminaUpdateSeconds;
    float StaminaUpdateMinutes;
    float testAbs;
    float test;
    float test2 = 0.0f;

    private void Awake()
    {
        NowTime = DateTime.Now; //���݂̎����擾
        AddTime = new TimeSpan(0, 0, StaminaMinutesTime, StaminaSecondsTime); //�X�^�~�i�񕜎��Ԑݒ�p
        string defult = new DateTime(2000, 1, 1, 16, 32, 0, DateTimeKind.Local).ToBinary().ToString();�@//�ۑ����Ă����f�[�^�����݂��Ȃ����p�̃f�t�H���g�l
        string StaminaUpdateTimeString = PlayerPrefs.GetString("StaminaUpdateTime", defult); //�O��ۑ����Ă����X�^�~�i���Ԃ�string�œǂݍ���
        StaminaUpdateTime = System.DateTime.FromBinary(System.Convert.ToInt64(StaminaUpdateTimeString)); //string�f�[�^��datetime�ɕϊ�
        StaminaUpdateDif = StaminaUpdateTime - NowTime; //�X�^�~�i�񕜎��Ԃƌ��݂̎��Ԃ����ق��v�Z
        StaminaUpdateSeconds = StaminaUpdateDif.Seconds; //���ق̕b�����o
        StaminaUpdateMinutes = StaminaUpdateDif.Minutes; //���ق̕������o
        test = StaminaUpdateSeconds + (StaminaUpdateMinutes * 60); //�b���ɂ��ăg�[�^���b�����o��
        testAbs = Mathf.Abs(test); //��Βl�ɂ��ă}�C�i�X�l��r��
        float testdif = testAbs / (StaminaSecondsTime + (StaminaMinutesTime * 60)); //�X�^�~�i�񕜎��ԂŊ����Ăǂ̂��炢�I�[�o�[���Ă邩�v�Z
        testdif = Mathf.Floor(testdif); //�����_�؂�̂�

        StaminaNumber = PlayerPrefs.GetInt("Stamina", 5); //�O��ۑ����̃X�^�~�i����ǂݍ���
        PreStaminaNumber = StaminaNumber; //�X�^�~�i�X�V���ꂽ���Ƃ����m���邽�߂̑��

        if (testdif >= 1 && test <= 0) //���������̓�����1�ȏ�̂Ƃ�
        {
            StaminaNumber += (int)testdif; //���������X�^�~�i��

            if(StaminaNumber <= 4) //�X�^�~�i4�ȉ��̂Ƃ�
            {
                while(test2 <= 0.0f)
                {
                    Debug.Log("������������");
                    AlreadyCount = true;
                    StaminaUpdateTime += AddTime;
                    StaminaUpdateDif = StaminaUpdateTime - NowTime;
                    StaminaUpdateSeconds = StaminaUpdateDif.Seconds; //���ق̕b�����o
                    StaminaUpdateMinutes = StaminaUpdateDif.Minutes; //���ق̕������o
                    test = StaminaUpdateSeconds + (StaminaUpdateMinutes * 60); //�b���ɂ��ăg�[�^���b�����o��
                    Debug.Log(test);
                    test2 = Mathf.Sign(test);
                }
            }

            if (StaminaNumber >= 5) //�X�^�~�i6�ȏ�ɂȂ������̏���
            {
                StaminaNumber = 5;
                PlayerPrefs.SetInt("Stamina", StaminaNumber);
            }
        }

        if(StaminaNumber < 5) //�X�^�~�i��4�ȉ��̎��͉񕜎��v���i�ނ��߂̃t���OON
        {
            WaitStaminaUpdate = true;
        }

        Stamina1 = GameObject.Find("Stamina1Image").GetComponent<Image>(); //�X�^�~�i�A�C�R���p
        Stamina2 = GameObject.Find("Stamina2Image").GetComponent<Image>(); //�X�^�~�i�A�C�R���p
        Stamina3 = GameObject.Find("Stamina3Image").GetComponent<Image>(); //�X�^�~�i�A�C�R���p
        Stamina4 = GameObject.Find("Stamina4Image").GetComponent<Image>(); //�X�^�~�i�A�C�R���p
        Stamina5 = GameObject.Find("Stamina5Image").GetComponent<Image>(); //�X�^�~�i�A�C�R���p
        StaminaCoolTime = GameObject.Find("StaminaCoolTimeText").GetComponent<Text>(); //���ԕ\���e�L�X�g�̎擾
        StaminaUpdate(); //�X�^�~�i�A�C�R���X�V
    }
    void Start()
    {

    }

    void Update()
    {

        if(StaminaNumber < 0) //�X�^�~�i���}�C�i�X�ɂȂ��Ă��܂������p�̔O�̂��߂̏���
        {
            StaminaNumber = 0;
            PreStaminaNumber = StaminaNumber;
            PlayerPrefs.SetInt("Stamina", StaminaNumber);
        }

        if(StaminaNumber > 5) //�X�^�~�i���I�[�o�[���Ă��܂������p�̔O�̂��߂̏���
        {
            StaminaNumber = 5;
            PreStaminaNumber = StaminaNumber;
            PlayerPrefs.SetInt("Stamina", StaminaNumber);
        }

        NowTime = DateTime.Now; //���݂̎����擾

        StaminaUpdateDif = StaminaUpdateTime - NowTime; //�X�^�~�i�񕜎��Ԃƌ��݂̎��Ԃ����ق��v�Z
        StaminaCoolTime.text = StaminaUpdateDif.ToString(); //�񕜂܂ł̎��Ԃ��e�L�X�g��string�ŕ\��
        StaminaUpdateTimeSecondsDif = StaminaUpdateTime.Second - NowTime.Second; //�X�^�~�i�񕜂܂ł̕b�����v�Z
        StaminaUpdateTimeMinutesDif = StaminaUpdateTime.Minute - NowTime.Minute; //�X�^�~�i�񕜂܂ł̕������v�Z

        if (StaminaUpdateTimeSecondsDif == 0 && WaitStaminaUpdate && StaminaUpdateTimeMinutesDif == 0) //�c�蕪�����b����0���A�X�^�~�i�A�b�v�f�[�g��҂��Ă���Ƃ�
        {
            WaitStaminaUpdate = false;
            AlreadyCount = false;
            StaminaNumber++; //�X�^�~�i+1
        }

        if (StaminaNumber == 5) //�X�^�~�i5�̂Ƃ�
        {
            StaminaCoolTimeText.SetActive(false); //�񕜎��Ԕ�\��
        }

        if (PreStaminaNumber != StaminaNumber) //�X�^�~�i�X�V���ꂽ�Ƃ�
        {
            PreStaminaNumber = StaminaNumber; //�X�^�~�i�X�V���ꂽ���Ƃ����m���邽�ߕϐ��ɑ��
            PlayerPrefs.SetInt("Stamina", StaminaNumber); //�f�[�^�ۑ�
            StaminaUpdate(); //�X�^�~�i�A�C�R���X�V

            if(StaminaNumber <= 4 && !AlreadyCount) //�X�^�~�i4�ȉ����܂��J�E���g���i��ł��Ȃ��Ƃ� �����łɉ񕜎��Ԃ��i��ł���Ƃ��ɐV���ɍX�V����Ǝ��Ԃ��ŏ�����ɂȂ��Ă��܂����߃t���O�Ǘ�
            {
                StaminaCoolTimeText.SetActive(true); //�񕜎��ԕ\��
                StaminaUpdateTime = NowTime + AddTime; //���ݎ����ɃX�^�~�i�񕜎��Ԃ𑫂������Ԃ��Z�o
                PlayerPrefs.SetString("StaminaUpdateTime", StaminaUpdateTime.ToBinary().ToString()); //�X�^�~�i�񕜎�����ۑ�
                WaitStaminaUpdate = true; //�X�^�~�i�A�b�v�f�[�g�҂�ON
                AlreadyCount = true; //�J�E���g�i��ł���̂ŐV���ɍX�V�������Ă������̏��u�͑���Ȃ��悤�ɂ���
            }
        }
    }

    public void DebugStaminaButton() //�f�o�b�O�p�J�E���g-1���\�b�h
    {
        StaminaNumber--;
        PlayerPrefs.SetInt("Stamina", StaminaNumber);
    }

    void StaminaUpdate()
    {
        switch (StaminaNumber) //�X�^�~�i�ɂ���ăA�C�R���؂�ւ�
        {
            case 0:
                Stamina1.sprite = StaminaOFF;
                Stamina2.sprite = StaminaOFF;
                Stamina3.sprite = StaminaOFF;
                Stamina4.sprite = StaminaOFF;
                Stamina5.sprite = StaminaOFF;
                break;

            case 1:
                Stamina1.sprite = StaminaON;
                Stamina2.sprite = StaminaOFF;
                Stamina3.sprite = StaminaOFF;
                Stamina4.sprite = StaminaOFF;
                Stamina5.sprite = StaminaOFF;
                break;

            case 2:
                Stamina1.sprite = StaminaON;
                Stamina2.sprite = StaminaON;
                Stamina3.sprite = StaminaOFF;
                Stamina4.sprite = StaminaOFF;
                Stamina5.sprite = StaminaOFF;
                break;

            case 3:
                Stamina1.sprite = StaminaON;
                Stamina2.sprite = StaminaON;
                Stamina3.sprite = StaminaON;
                Stamina4.sprite = StaminaOFF;
                Stamina5.sprite = StaminaOFF;
                break;

            case 4:
                Stamina1.sprite = StaminaON;
                Stamina2.sprite = StaminaON;
                Stamina3.sprite = StaminaON;
                Stamina4.sprite = StaminaON;
                Stamina5.sprite = StaminaOFF;
                break;

            case 5:
                Stamina1.sprite = StaminaON;
                Stamina2.sprite = StaminaON;
                Stamina3.sprite = StaminaON;
                Stamina4.sprite = StaminaON;
                Stamina5.sprite = StaminaON;
                break;
        }
    }
}
