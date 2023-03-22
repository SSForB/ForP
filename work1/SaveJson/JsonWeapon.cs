using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class WeaponSaveData
{
    public int[] weaponFlag = new int[999]; //0:レシピなし　1:レシピあり
    public int[] weaponHave = new int[999]; //その装備を持っている数
}


public class JsonWeapon : MonoBehaviour
{
    int count;
    string filePath; //保存先のパスを指定するための変数
    public WeaponSaveData saveData;

    void Awake()
    {
        filePath = Application.dataPath + "/savedata/WeaponSaveData.json"; //セーブデータを置く場所と、名前を指定
        saveData = new WeaponSaveData();
        saveData = Load();
        Save(saveData);
    }

    public void Save(WeaponSaveData saveData)
    {
        string jsonDate = JsonUtility.ToJson(saveData); //saveDataに入っている変数とその値をjsonに入れるために変換してjsonData変数に代入(すべてstring型にしたりもろもろ)

        using (StreamWriter streamWriter = new StreamWriter(filePath)) //書き込む
        {
            streamWriter.Write(jsonDate); //jsonDataに入った変数と値を実際に書き込む
        }
    }

    public WeaponSaveData Load()
    {
        if (File.Exists(filePath)) //最初にセーブデータがすでにあるか確認
        {
            StreamReader streamReader;

            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd(); //jsonデータをすべて読み込みdata変数に代入
            streamReader.Close();

            return JsonUtility.FromJson<WeaponSaveData>(data); //dataをこっちで読み込めるように変換してsaveDataに代入
        }

        WeaponSaveData savedata = new WeaponSaveData(); //セーブデータがなかったら
        saveData.weaponFlag[0] = 0;
        savedata.weaponHave[0] = 0;
        return saveData; //その後上書きする
    }

    public void PushSaveButton()
    {
        Save(saveData);
    }

    public void PushLoadButton()
    {
        WeaponSaveData saveData = Load();
    }
}
