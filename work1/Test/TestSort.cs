using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class TestSort : MonoBehaviour
{

    GameObject[] weaponObject = new GameObject[999];
    int[] weaponPowerInt = new int[999];
    int[] weaponSort = new int[999];
    GameObject _gameObject;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("waitGetvalue", 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushSortButton()
    {
        //var weaponSort = weaponObject.OrderBy(x => x.GetComponent<TestGetValue>().powerInt).ToArray();
        weaponSort = weaponPowerInt.OrderBy(x => x).ToArray();

        //Debug.Log(weaponSort);

        foreach (var item in weaponSort)
        {
            Debug.Log(item);
        }

        for (int i = 0; i < weaponPowerInt.Length; i++)
        {
            var index = Array.IndexOf(weaponSort, weaponPowerInt[i]);
            Debug.Log("weaponObject" + i + "のソート後の位置は"+index + "です");

            if (weaponObject[i])
            {
                weaponObject[i].transform.SetSiblingIndex(index);
            }
            else
            {
                break;
            }
        }
    }

    void waitGetvalue()
    {
        GameObject weaponContent = GameObject.Find("WeaponContent");

        _gameObject = GameObject.Find($"Weapon ({count})");

        while (_gameObject)
        {
            weaponObject[count] = _gameObject;
            weaponPowerInt[count] = _gameObject.GetComponent<TestGetValue>().powerInt;
            Debug.Log(_gameObject + "は" + weaponPowerInt[count]);
            count++;
            _gameObject = GameObject.Find($"Weapon ({count})");

            if (_gameObject == null)
            {
                break;
            }
        }
    }
}
