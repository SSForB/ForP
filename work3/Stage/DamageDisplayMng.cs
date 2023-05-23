using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DamageDisplayMng : MonoBehaviour
{
    //モンハンやEDF等で採用されてるような、ダメージ毎の数値を表示するために作った機能、数値がかなり見にくかったので一旦オミット
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
