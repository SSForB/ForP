using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGetValue : MonoBehaviour
{

    Text powerText;
    string powerString;
    public int powerInt;

    // Start is called before the first frame update
    void Start()
    {

        Invoke("waitGet", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void waitGet()
    { 

        GameObject _gameObject;
        GameObject _gameObject2;

        _gameObject = this.gameObject;

        _gameObject2 = _gameObject.transform.GetChild(2).gameObject;
        powerText = _gameObject2.GetComponent<Text>();
        powerString = powerText.text;
        powerInt = int.Parse(powerString);
        Debug.Log(powerInt);
    }
}
