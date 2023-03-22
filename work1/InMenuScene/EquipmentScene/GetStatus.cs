using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetStatus : MonoBehaviour
{
    Text powerText;
    Text searchText;
    Text luckText;
    Text haveText;

    string powerString;
    string searchString;
    string luckString;
    string haveString;

    public int powerInt;
    public int searchInt;
    public int luckInt;
    public int haveInt;


    // Start is called before the first frame update
    void Start()
    {
        GameObject _gameObject;
        GameObject _gameObject2;

        _gameObject = this.gameObject;

        _gameObject2 = _gameObject.transform.GetChild(2).gameObject;
        powerText = _gameObject2.GetComponent<Text>();
        powerString = powerText.text;
        powerInt = int.Parse(powerString);

        _gameObject2 = _gameObject.transform.GetChild(3).gameObject;
        searchText = _gameObject2.GetComponent<Text>();
        searchString = searchText.text;
        searchInt = int.Parse(searchString);

        _gameObject2 = _gameObject.transform.GetChild(4).gameObject;
        luckText = _gameObject2.GetComponent<Text>();
        luckString = luckText.text;
        luckInt = int.Parse(luckString);

        _gameObject2 = _gameObject.transform.GetChild(5).gameObject;
        haveText = _gameObject2.GetComponent<Text>();
        haveString = haveText.text;
        haveInt = int.Parse(haveString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
