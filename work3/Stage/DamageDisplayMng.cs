using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DamageDisplayMng : MonoBehaviour
{
    //�����n����EDF���ō̗p����Ă�悤�ȁA�_���[�W���̐��l��\�����邽�߂ɍ�����@�\�A���l�����Ȃ茩�ɂ��������̂ň�U�I�~�b�g
    TextMeshProUGUI damageText;
    void Start()
    {
        damageText = this.gameObject.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if(damageText.text !="")
        {
            Invoke(nameof(DestroyThisGameObject), 2.0f);
        }
    }

    void DestroyThisGameObject()
    {
        Destroy(this.gameObject);
    }
}
