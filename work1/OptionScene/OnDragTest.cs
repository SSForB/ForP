using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class OnDragTest : MonoBehaviour, IEndDragHandler
{
    AudioSource audioSource; //SE確認用オーディオソース取得するための宣言

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //SE確認用オーディオソースを取得
    }

    void Update()
    {

    }

    public void OnEndDrag(PointerEventData eventData)　//ドラッグ操作が終わったら
    {
        audioSource.Play(); //SE音量確認用のオーディオソースを再生
    }
}
