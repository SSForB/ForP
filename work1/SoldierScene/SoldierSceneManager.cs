using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierSceneManager : MonoBehaviour
{
    public ArmyDatabase armyDatabase;
    public WeaponDatabase weaponDatabase;
    public ArmorDatabase armorDatabase;
    public JsonArmy jsonArmy;
    public GameObject fireDialog;
    ArmySaveData armySaveData;

    ArmyDatabase.Param armyParam;
    WeaponDatabase.Param weaponParam;
    ArmorDatabase.Param armorParam;

    GameObject _gameObject;

    Text jobText;
    Text levelText;
    Text sexText;
    Text ageText;
    Text powerText;
    Text searchText;
    Text luckText;
    Text weaponText;
    Text armorHeadText;
    Text armorBodyText;
    Text armorShieldText;

    Image characterImage;

    string gameObjectName;
    string gameObjectNameDialog;

    int count2;
    int count3;

    Text[] soldierText = new Text[20];
    string[] soldierString = new string[20];

    // Start is called before the first frame update
    void Start()
    {
        //jsonArmy.Load();

        fireDialog.SetActive(false);
        armySaveData = jsonArmy.saveData;

        int count = 0;
        _gameObject = GameObject.Find("Soldier (" + count + ")");
        while (_gameObject)
        {
            soldierText[count] = _gameObject.GetComponentInChildren<Text>();
            armyParam = armyDatabase.list[count];
            soldierText[count].text = armyParam.armyName;
            if (armySaveData.armyFlag[count] == 0)
            {
                _gameObject.SetActive(false);
            }
            count++;
            _gameObject = GameObject.Find("Soldier (" + count + ")");

            if (_gameObject == null)
            {
                break;
            }
        }

        jobText = GameObject.Find("JobText").GetComponent<Text>();
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        sexText = GameObject.Find("SexText").GetComponent<Text>();
        ageText = GameObject.Find("AgeText").GetComponent<Text>();
        powerText = GameObject.Find("PowerText").GetComponent<Text>();
        searchText = GameObject.Find("SearchText").GetComponent<Text>();
        luckText = GameObject.Find("LuckText").GetComponent<Text>();
        weaponText = GameObject.Find("WeaponText").GetComponent<Text>();
        armorHeadText = GameObject.Find("ArmorHeadText").GetComponent<Text>();
        armorBodyText = GameObject.Find("ArmorBodyText").GetComponent<Text>();
        armorShieldText = GameObject.Find("ArmorShieldText").GetComponent<Text>();
        characterImage = GameObject.Find("CharacterImage").GetComponent<Image>();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintSenderName(Button nameSender) //ネットから拾ってきたもの
    {
        count2 = 0;
        count3 = 0;
        gameObjectName = nameSender.name; //Onclickにアタッチしたボタンオブジェクトの名前を取得
        string gameObjectNameReplace = gameObjectName.Replace("Soldier ", ""); //名前から数字だけを取り出す
        gameObjectNameReplace = gameObjectNameReplace.Replace("(", ""); //名前から数字だけを取り出す
        gameObjectNameReplace = gameObjectNameReplace.Replace(")", ""); //名前から数字だけを取り出す
        count2 = int.Parse(gameObjectNameReplace); //intに変換して代入
        armyParam = armyDatabase.list[count2]; //データベースの配列番号を指定

        characterImage.sprite = armyParam.armySprite;

        levelText.text = jsonArmy.saveData.armyLevel[count2].ToString();

        switch (armyParam.armySex)
        {
            case 0:
                sexText.text = "男";
                break;
            case 1:
                sexText.text = "女";
                break;
            case 2:
                sexText.text = "不明";
                break;
            default:
                sexText.text = "何も入力されていません";
                break;
        }

        ageText.text = armyParam.armyAge.ToString();
        powerText.text = jsonArmy.saveData.armyPower[count2].ToString();
        searchText.text = jsonArmy.saveData.armySearch[count2].ToString();
        luckText.text = jsonArmy.saveData.armyLuck[count2].ToString();

        weaponParam = weaponDatabase.list[count3];
        while(weaponParam.weaponID != jsonArmy.saveData.armyWeapon[count2])
        {
            count3++;
            weaponParam = weaponDatabase.list[count3];
        }
        weaponText.text = weaponParam.weaponName;

        /*count3 = 0;
        armorParam = armorDatabase.list[count3];
        while (armorParam.armorID != jsonArmy.saveData.armyHead[count2])
        {
            count3++;
            armorParam = armorDatabase.list[count3];
        }
        armorHeadText.text = armorParam.armorName;

        count3 = 0;
        armorParam = armorDatabase.list[count3];
        while (armorParam.armorID != jsonArmy.saveData.armyBody[count2])
        {
            count3++;
            armorParam = armorDatabase.list[count3];
        }
        armorBodyText.text = armorParam.armorName;

        count3 = 0;
        armorParam = armorDatabase.list[count3];
        while (armorParam.armorID != jsonArmy.saveData.armyShield[count2])
        {
            count3++;
            armorParam = armorDatabase.list[count3];
        }
        armorShieldText.text = armorParam.armorName;*/

        SetTextForEquipment(armorDatabase, armorHeadText, jsonArmy.saveData.armyHead,count2);
        SetTextForEquipment(armorDatabase, armorBodyText, jsonArmy.saveData.armyBody,count2);
        SetTextForEquipment(armorDatabase, armorShieldText, jsonArmy.saveData.armyShield,count2);
    }

    //アーマー関連のテキストを設定
    static void SetTextForEquipment(ArmorDatabase database,Text targetText,string[] armorKind,int count)
    {
        var count4 = 0;
        var armorParam = database.list[count4];
        
        while (armorParam.armorID != armorKind[count])
        {
            count4++;
            armorParam = database.list[count4];
        }
        targetText.text = armorParam.armorName;
    }
    

    public void PushFireButton()
    {
        if(gameObjectName == null)
        {
            Debug.Log("何も選択されていません");
        }
        else
        {
            fireDialog.SetActive(true);
        }
    }

    public void PushFireDialog(Button SelectName)
    {
        gameObjectNameDialog = SelectName.name;

        if(gameObjectNameDialog == "YesButton")
        {

        }
        else
        {
            fireDialog.SetActive(false);
        }
    }
}
