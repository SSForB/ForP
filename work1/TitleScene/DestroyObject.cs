using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    static AudioSource prevaudiomaster = null;
    public AudioSource audiomaster;

    void Start()
    {
        //他画面遷移後、また戻ってくるとBGMが2重に再生されるのを防ぐ処理

        if (prevaudiomaster == null)
        {
            prevaudiomaster = audiomaster;
            audiomaster.Play();
        }
        else //もしnullじゃなかったら
        {
            Destroy(audiomaster);
            audiomaster = prevaudiomaster;
        }
    }

    void Update()
    {
        
    }
}
