using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class EquipmentManager : MonoBehaviour
{
    public JsonWeapon jsonWeapon;
    public JsonArmor jsonArmor;
    public WeaponDatabase weaponDatabase;
    public ArmorDatabase armorDatabase;
    public GameObject weaponScrollView;
    public GameObject armorScrollView;

    Text[] weaponNameText = new Text[999];
    Text[] weaponPowerText = new Text[999];
    Text[] weaponSearchText = new Text[999];
    Text[] weaponHaveText = new Text[999];
    Text[] weaponLuckText = new Text[999];
    int[] weaponFlag = new int[999];

    Text[] armorNameText = new Text[999];
    Text[] armorPowerText = new Text[999];
    Text[] armorSearchText = new Text[999];
    Text[] armorHaveText = new Text[999];
    Text[] armorLuckText = new Text[999];
    int[] armorFlag = new int[999];

    string weaponName;
    string armorName;
    int weaponHave;
    int armorHave;
    int weaponKind;
    int armorKind;
    int weaponPower;
    int armorPower;
    int weaponLuck;
    int armorLuck;
    int weaponSearch;
    int armorSearch;
    Sprite _sprite;

    int weaponCount;
    int armorCount;
    int count;
    int count2;
    int count3;
    int count4;
    GameObject _gameObject;
    GameObject _gameObject2;
    GameObject _gameObject3;
    GameObject _gameObject4;
    GameObject[] weaponObject = new GameObject[999];
    GameObject[] armorObject = new GameObject[999];
    Image _image;

    bool switchButton = true;
    bool bWeaponPowerSort = true;
    bool bWeaponSearchSort = true;
    bool bWeaponLuckSort = true;
    bool bWeaponHaveSort = true;
    bool bArmorPowerSort = true;
    bool bArmorSearchSort = true;
    bool bArmorLuckSort = true;
    bool bArmorHaveSort = true;

    int[] weaponPowerInt = new int[999];
    int[] weaponSearchInt = new int[999];
    int[] weaponLuckInt = new int[999];
    int[] weaponHaveInt = new int[999];
    int[] weaponSort = new int[999];

    int[] armorPowerInt = new int[999];
    int[] armorSearchInt = new int[999];
    int[] armorLuckInt = new int[999];
    int[] armorHaveInt = new int[999];
    int[] armorSort = new int[999];

    WeaponDatabase.Param weaponParam;
    ArmorDatabase.Param armorParam;

    private void Awake()
    {
        jsonArmor.Load();
        jsonWeapon.Load();

        weaponScrollView.SetActive(true);
        armorScrollView.SetActive(true);

        for (count2 = 0; count2 < 999; count2++) //セーブデータ0～999個までのフラグ番号をすべて取得
        {
            weaponFlag[count2] = jsonWeapon.saveData.weaponFlag[count2]; //対応した物を入れていく
            armorFlag[count2] = jsonArmor.saveData.armorFlag[count2]; //対応した物を入れていく
        }

        GetWeapon();

        GetArmor();

        count3 = 0;
        _gameObject = GameObject.Find("Weapon ("+count3+")");

        while (true) //無限ループ
        {
            switch (weaponFlag[count3])
            {
                case 0:
                    _gameObject.SetActive(false);
                    break;

                default:
                    break;
            }

            count3++; //カウントを1進める
            _gameObject = GameObject.Find("Weapon ("+count3+")"); //オブジェクトの取得

            if (_gameObject == null) //もしゲームオブジェクトがなかったら処理を抜ける
            {
                break;
            }
        }

        count4 = 0;
        _gameObject = GameObject.Find("Armor ("+count4+")");

        while (true) //無限ループ
        {
            //Debug.Log(_gameObject+"のフラグ番号は"+armorFlag[count4]+"です");

            switch (armorFlag[count4])
            {
                case 0:
                    _gameObject.SetActive(false);
                    break;

                default:
                    break;
            }

            count4++; //カウントを1進める
            _gameObject = GameObject.Find("Armor ("+count4+")"); //オブジェクトの取得

            if (_gameObject == null) //もしゲームオブジェクトがなかったら処理を抜ける
            {
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        count = 0;

        GameObject weaponContent = GameObject.Find("WeaponContent");

        _gameObject3 = GameObject.Find("Weapon ("+count+")");

        while (_gameObject3)
        {
            weaponObject[count] = _gameObject3;
            weaponPowerInt[count] = _gameObject3.GetComponent<GetStatus>().powerInt;
            weaponSearchInt[count] = _gameObject3.GetComponent<GetStatus>().searchInt;
            weaponLuckInt[count] = _gameObject3.GetComponent<GetStatus>().luckInt;
            weaponHaveInt[count] = _gameObject3.GetComponent<GetStatus>().haveInt;
            count++;
            _gameObject3 = GameObject.Find("Weapon (" + count + ")");

            if (_gameObject3 == null)
            {
                break;
            }
        }

        count = 0;

        GameObject armorContent = GameObject.Find("ArmorContent");

        _gameObject4 = GameObject.Find("Armor (" + count + ")");

        while (_gameObject4)
        {
            armorObject[count] = _gameObject4;
            armorPowerInt[count] = _gameObject4.GetComponent<GetStatus>().powerInt;
            armorSearchInt[count] = _gameObject4.GetComponent<GetStatus>().searchInt;
            armorLuckInt[count] = _gameObject4.GetComponent<GetStatus>().luckInt;
            armorHaveInt[count] = _gameObject4.GetComponent<GetStatus>().haveInt;
            count++;
            _gameObject4 = GameObject.Find("Armor (" + count + ")");

            if (_gameObject4 == null)
            {
                break;
            }
        }

        weaponScrollView.SetActive(true);
        armorScrollView.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintSenderNameWeapon(Button nameSender) //ネットから拾ってきたもの
    {
        string gameObjectName = nameSender.name; //Onclickにアタッチしたボタンオブジェクトの名前を取得
        string gameObjectNameReplace = gameObjectName.Replace("Weapon", ""); //名前から数字だけを取り出す
        count = int.Parse(gameObjectNameReplace); //intに変換して代入
        weaponParam = weaponDatabase.list[count]; //データベースの配列番号を指定

        GameObject.Find("EquipmentNameText").GetComponent<Text>().text = weaponParam.weaponName;
        GameObject.Find("EquipmentPowerText").GetComponent<Text>().text = weaponParam.weaponPower.ToString();
        GameObject.Find("EquipmentSearchText").GetComponent<Text>().text = weaponParam.weaponSearch.ToString();
        GameObject.Find("EquipmentLuckText").GetComponent<Text>().text = weaponParam.weaponLuck.ToString();
    }

    public void PrintSenderNameArmor(Button nameSender) //ネットから拾ってきたもの
    {
        string gameObjectName = nameSender.name; //Onclickにアタッチしたボタンオブジェクトの名前を取得
        string gameObjectNameReplace = gameObjectName.Replace("Armor", ""); //名前から数字だけを取り出す
        count = int.Parse(gameObjectNameReplace); //intに変換して代入
        armorParam = armorDatabase.list[count]; //データベースの配列番号を指定

        GameObject.Find("EquipmentNameText").GetComponent<Text>().text = armorParam.armorName;
        GameObject.Find("EquipmentPowerText").GetComponent<Text>().text = armorParam.armorPower.ToString();
        GameObject.Find("EquipmentSearchText").GetComponent<Text>().text = armorParam.armorSearch.ToString();
        GameObject.Find("EquipmentLuckText").GetComponent<Text>().text = armorParam.armorLuck.ToString();
    }

    public void SwitchEquipmentButton()
    {
        if(switchButton) //true = weapon表示中 false = armor表示中
        {
            weaponScrollView.SetActive(false);
            armorScrollView.SetActive(true);
            switchButton = false;
        }
        else
        {
            weaponScrollView.SetActive(true);
            armorScrollView.SetActive(false);
            switchButton = true;
        }
    }

    public void PushPowerSort()
    {
        if (switchButton)
        {
            if (bWeaponPowerSort)
            {
                weaponSort = weaponPowerInt.OrderBy(x => x).ToArray();
                bWeaponPowerSort = false;
            }
            else
            {
                weaponSort = weaponPowerInt.OrderByDescending(x => x).ToArray();
                bWeaponPowerSort = true;
            }

            for (int i = 0; i < weaponPowerInt.Length; i++)
            {
                var index = Array.IndexOf(weaponSort, weaponPowerInt[i]);

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
        else
        {
            if (bArmorPowerSort)
            {
                armorSort = armorPowerInt.OrderBy(x => x).ToArray();
                bArmorPowerSort = false;
            }
            else
            {
                armorSort = armorPowerInt.OrderByDescending(x => x).ToArray();
                bArmorPowerSort = true;
            }

            for (int i = 0; i < armorPowerInt.Length; i++)
            {
                var index = Array.IndexOf(armorSort, armorPowerInt[i]);

                if (armorObject[i])
                {
                    armorObject[i].transform.SetSiblingIndex(index);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void PushSearchSort()
    {
        if (switchButton)
        {
            if (bWeaponSearchSort)
            {
                weaponSort = weaponSearchInt.OrderBy(x => x).ToArray();
                bWeaponSearchSort = false;
            }
            else
            {
                weaponSort = weaponSearchInt.OrderByDescending(x => x).ToArray();
                bWeaponSearchSort = true;
            }

            for (int i = 0; i < weaponSearchInt.Length; i++)
            {
                var index = Array.IndexOf(weaponSort, weaponSearchInt[i]);

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
        else
        {
            if (bArmorSearchSort)
            {
                armorSort = armorSearchInt.OrderBy(x => x).ToArray();
                bArmorSearchSort = false;
            }
            else
            {
                armorSort = armorSearchInt.OrderByDescending(x => x).ToArray();
                bArmorSearchSort = true;
            }

            for (int i = 0; i < armorSearchInt.Length; i++)
            {
                var index = Array.IndexOf(armorSort, armorSearchInt[i]);

                if (armorObject[i])
                {
                    armorObject[i].transform.SetSiblingIndex(index);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void PushLuckSort()
    {
        if (switchButton)
        {
            if (bWeaponLuckSort)
            {
                weaponSort = weaponLuckInt.OrderBy(x => x).ToArray();
                bWeaponLuckSort = false;
            }
            else
            {
                weaponSort = weaponLuckInt.OrderByDescending(x => x).ToArray();
                bWeaponLuckSort = true;
            }

            for (int i = 0; i < weaponLuckInt.Length; i++)
            {
                var index = Array.IndexOf(weaponSort, weaponLuckInt[i]);

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
        else
        {
            if (bArmorLuckSort)
            {
                armorSort = armorLuckInt.OrderBy(x => x).ToArray();
                bArmorLuckSort = false;
            }
            else
            {
                armorSort = armorLuckInt.OrderByDescending(x => x).ToArray();
                bArmorLuckSort = true;
            }

            for (int i = 0; i < armorLuckInt.Length; i++)
            {
                var index = Array.IndexOf(armorSort, armorLuckInt[i]);

                if (armorObject[i])
                {
                    armorObject[i].transform.SetSiblingIndex(index);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void PushHaveSort()
    {
        if (switchButton)
        {
            if (bWeaponHaveSort)
            {
                weaponSort = weaponHaveInt.OrderBy(x => x).ToArray();
                bWeaponHaveSort = false;
            }
            else
            {
                weaponSort = weaponHaveInt.OrderByDescending(x => x).ToArray();
                bWeaponHaveSort = true;
            }

            for (int i = 0; i < weaponHaveInt.Length; i++)
            {
                var index = Array.IndexOf(weaponSort, weaponHaveInt[i]);

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
        else
        {
            if (bArmorHaveSort)
            {
                armorSort = armorHaveInt.OrderBy(x => x).ToArray();
                bArmorHaveSort = false;
            }
            else
            {
                armorSort = armorHaveInt.OrderByDescending(x => x).ToArray();
                bArmorHaveSort = true;
            }

            for (int i = 0; i < armorHaveInt.Length; i++)
            {
                var index = Array.IndexOf(armorSort, armorHaveInt[i]);

                if (armorObject[i])
                {
                    armorObject[i].transform.SetSiblingIndex(index);
                }
                else
                {
                    break;
                }
            }
        }
    }

    void GetWeapon()
    {
        weaponCount = 0;

        _gameObject = GameObject.Find("Weapon ("+weaponCount+")");

        while (_gameObject) //ゲームオブジェクトが存在しているなら
        {
            weaponParam = weaponDatabase.list[weaponCount]; //データベースの配列番号を指定

            _gameObject2 = _gameObject.transform.GetChild(0).gameObject;
            _image = _gameObject2.GetComponent<Image>();
            weaponKind = weaponParam.weaponKind;
            switch (weaponKind)
            {
                case 0:
                    _image.sprite = weaponDatabase.weaponSword;
                    //Debug.Log("剣の画像を入れるよ");
                    break;

                case 1:
                    _image.sprite = weaponDatabase.weaponAxe;
                    //Debug.Log("斧の画像を入れるよ");
                    break;

                case 2:
                    _image.sprite = weaponDatabase.weaponWand;
                    //Debug.Log("斧の画像を入れるよ");
                    break;

                default:
                    break;
            }

            weaponName = weaponParam.weaponName;
            _gameObject2 = _gameObject.transform.GetChild(1).gameObject;
            weaponNameText[weaponCount] = _gameObject2.GetComponent<Text>();
            weaponNameText[weaponCount].text = weaponName;

            weaponPower = weaponParam.weaponPower;
            _gameObject2 = _gameObject.transform.GetChild(2).gameObject;
            weaponPowerText[weaponCount] = _gameObject2.GetComponent<Text>();
            weaponPowerText[weaponCount].text = weaponPower.ToString();

            weaponSearch = weaponParam.weaponSearch;
            _gameObject2 = _gameObject.transform.GetChild(3).gameObject;
            weaponSearchText[weaponCount] = _gameObject2.GetComponent<Text>();
            weaponSearchText[weaponCount].text = weaponSearch.ToString();

            weaponLuck = weaponParam.weaponLuck;
            _gameObject2 = _gameObject.transform.GetChild(4).gameObject;
            weaponLuckText[weaponCount] = _gameObject2.GetComponent<Text>();
            weaponLuckText[weaponCount].text = weaponLuck.ToString();

            weaponHave = jsonWeapon.saveData.weaponHave[weaponCount];
            _gameObject2 = _gameObject.transform.GetChild(5).gameObject;
            weaponHaveText[weaponCount] = _gameObject2.GetComponent<Text>();
            weaponHaveText[weaponCount].text = weaponHave.ToString();

            weaponCount++; //カウントを1進める
            _gameObject = GameObject.Find("Weapon ("+weaponCount+")"); //次のゲームオブジェクトを取得 ※見つからなかったらループ処理を抜ける
        }
    }

    void GetArmor()
    {
        armorCount = 0;

        _gameObject = GameObject.Find("Armor ("+armorCount+")");

        while (_gameObject) //ゲームオブジェクトが存在しているなら
        {
            armorParam = armorDatabase.list[armorCount]; //データベースの配列番号を指定

            _gameObject2 = _gameObject.transform.GetChild(0).gameObject;
            _image = _gameObject2.GetComponent<Image>();
            armorKind = armorParam.armorKind;
            switch (armorKind)
            {
                case 0:
                    _image.sprite = armorDatabase.armorHead;
                    break;

                case 1:
                    _image.sprite = armorDatabase.armorBody;
                    break;

                case 2:
                    _image.sprite = armorDatabase.armorLeg;
                    break;

                default:
                    break;
            }

            armorName = armorParam.armorName;
            _gameObject2 = _gameObject.transform.GetChild(1).gameObject;
            armorNameText[armorCount] = _gameObject2.GetComponent<Text>();
            armorNameText[armorCount].text = armorName;

            armorPower = armorParam.armorPower;
            _gameObject2 = _gameObject.transform.GetChild(2).gameObject;
            armorPowerText[armorCount] = _gameObject2.GetComponent<Text>();
            armorPowerText[armorCount].text = armorPower.ToString();

            armorSearch = armorParam.armorSearch;
            _gameObject2 = _gameObject.transform.GetChild(3).gameObject;
            armorSearchText[armorCount] = _gameObject2.GetComponent<Text>();
            armorSearchText[armorCount].text = armorSearch.ToString();

            armorLuck = armorParam.armorLuck;
            _gameObject2 = _gameObject.transform.GetChild(4).gameObject;
            armorLuckText[armorCount] = _gameObject2.GetComponent<Text>();
            armorLuckText[armorCount].text = armorLuck.ToString();

            armorHave = jsonArmor.saveData.armorHave[armorCount];
            _gameObject2 = _gameObject.transform.GetChild(5).gameObject;
            armorHaveText[armorCount] = _gameObject2.GetComponent<Text>();
            armorHaveText[armorCount].text = armorHave.ToString();

            armorCount++; //カウントを1進める
            _gameObject = GameObject.Find("Armor ("+armorCount+")"); //次のゲームオブジェクトを取得 ※見つからなかったらループ処理を抜ける
        }
    }
}
