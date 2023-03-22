using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStageAudioManager : MonoBehaviour
{

    public AudioClip JumpSE;
    public AudioClip SlideSE;
    public AudioClip ExplosionSE;
    public AudioClip GameOverSE;
    public AudioClip ClearSE;
    public AudioClip NextStageOrReTrySE;
    public AudioClip BackToStageSelectSE;

    [HideInInspector]
    public AudioSource MainStageBGM;
    [HideInInspector]
    public AudioSource MainStageSE;

    void Start()
    {
        MainStageBGM = gameObject.GetComponent<AudioSource>();
        MainStageSE = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SE(int i)
    {
        switch (i)
        {
            case 1:
                MainStageSE.clip = JumpSE;
                Debug.Log(1);
                break;
            case 2:
                MainStageSE.clip = SlideSE;
                Debug.Log(2);
                break;
            case 3:
                MainStageSE.clip = ExplosionSE;
                Debug.Log(3);
                break;
            case 4:
                MainStageSE.clip = GameOverSE;
                Debug.Log(4);
                break;
            case 5:
                MainStageSE.clip = ClearSE;
                Debug.Log(5);
                break;
            case 6:
                MainStageSE.clip = NextStageOrReTrySE;
                Debug.Log(6);
                break;
            case 7:
                MainStageSE.clip = BackToStageSelectSE;
                Debug.Log(7);
                break;
        }
        MainStageSE.Play();
    }
}
