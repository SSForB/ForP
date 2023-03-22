using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ArmySaveData
{
    public int[] armyFlag = new int[20]; //0:雇っていない　1:雇っている
    public string[] armyID = new string[20]; //ID
    public int[] armyLevel = new int[20]; //レベル
    public int[] armyPower = new int[20]; //戦闘力
    public int[] armySearch = new int[20]; //探索力
    public int[] armyLuck = new int[20]; //幸運
    public string[] armyWeapon = new string[20]; //所持している武器ID
    public string[] armyHead = new string[20]; //所持している頭装備ID
    public string[] armyBody = new string[20]; //所持している体装備ID
    public string[] armyShield = new string[20]; //所持している盾装備ID
}


public class JsonArmy : MonoBehaviour
{
    int count;
    string filePath; //保存先のパスを指定するための変数
    public ArmySaveData saveData;

    void Awake()
    {
        filePath = Application.dataPath + "/savedata/ArmySaveData.json"; //セーブデータを置く場所と、名前を指定
        saveData = new ArmySaveData();
        saveData = Load();
    }

    public void Save(ArmySaveData saveData)
    {
        string jsonDate = JsonUtility.ToJson(saveData); //saveDataに入っている変数とその値をjsonに入れるために変換してjsonData変数に代入(すべてstring型にしたりもろもろ)

        using (StreamWriter streamWriter = new StreamWriter(filePath)) //書き込む
        {
            streamWriter.Write(jsonDate); //jsonDataに入った変数と値を実際に書き込む
        }
    }

    public ArmySaveData Load()
    {
        if (File.Exists(filePath)) //最初にセーブデータがすでにあるか確認
        {
            StreamReader streamReader;

            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd(); //jsonデータをすべて読み込みdata変数に代入
            streamReader.Close();

            return JsonUtility.FromJson<ArmySaveData>(data); //dataをこっちで読み込めるように変換してsaveDataに代入
        }

        WeaponSaveData savedata = new WeaponSaveData(); //セーブデータがなかったら
        saveData.armyFlag[0] = 0; 
        saveData.armyID[0] = ""; 
        saveData.armyLevel[0] = 0; 
        saveData.armyPower[0] = 0;
        saveData.armySearch[0] = 0;
        saveData.armyLuck[0] = 0;
        saveData.armyWeapon[0] = "";
        saveData.armyHead[0] = "";
        saveData.armyBody[0] = "";
        saveData.armyShield[0] = "";

        return saveData; //その後上書きする
    }
}
