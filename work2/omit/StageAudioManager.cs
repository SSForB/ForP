using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAudioManager : MonoBehaviour
{
    public AudioClip[] StageBGM;
    StageManager stageManager;
    int ListNum;

    public AudioClip JumpSE;
    public AudioClip SlideSE;
    public AudioClip ExplosionSE;
    public AudioClip GameOverSE;
    public AudioClip ClearSE;
    public AudioClip NextStageOrReTrySE;
    public AudioClip BackToStageSelectSE;

    [HideInInspector]
    public AudioSource StageSceneBGM;
    [HideInInspector]
    public AudioSource StageSceneSE;

    [Header("0.001～1の間で数値を記載してください。中央値は0.5です。")]
    public float BGMVolume;
    public float SEVolume;

    void Start()
    {
        StageSceneBGM = gameObject.GetComponent<AudioSource>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        ListNum = stageManager.ListNumber;
        StageSceneSE = gameObject.AddComponent<AudioSource>();
        StageSceneBGM.volume = BGMVolume;
        StageSceneSE.volume = SEVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageBGMStart()
    {
        StartCoroutine("StartBGM");
    }

    IEnumerator StartBGM()
    {
        yield return null;
        StageSceneBGM.clip = StageBGM[ListNum];
        StageSceneBGM.Play();
    }

    public void StopBGM()
    {
        StageSceneBGM.Stop();
    }

    public void SE(int i)
    {
        switch(i)
        {
            case 1:
                StageSceneSE.clip = JumpSE;
                Debug.Log(1);
                break;
            case 2:
                StageSceneSE.clip = SlideSE;
                Debug.Log(2);
                break;
            case 3:
                StageSceneSE.clip = ExplosionSE;
                Debug.Log(3);
                break;
            case 4:
                StageSceneSE.clip = GameOverSE;
                Debug.Log(4);
                break;
            case 5:
                StageSceneSE.clip = ClearSE;
                Debug.Log(5);
                break;
            case 6:
                StageSceneSE.clip = NextStageOrReTrySE;
                Debug.Log(6);
                break;
            case 7:
                StageSceneSE.clip = BackToStageSelectSE;
                Debug.Log(7);
                break;
        }
        StageSceneSE.Play();
    }

}
