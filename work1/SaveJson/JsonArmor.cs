using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ArmorSaveData
{
    public int[] armorFlag = new int[999]; //0:レシピなし　1:レシピあり
    public int[] armorHave = new int[999]; //その装備を持っている数
}


public class JsonArmor : MonoBehaviour
{
    int count;
    string filePath; //保存先のパスを指定するための変数
    public ArmorSaveData saveData;

    void Awake()
    {
        filePath = Application.dataPath + "/savedata/ArmorSaveData.json"; //セーブデータを置く場所と、名前を指定
        saveData = new ArmorSaveData();
        saveData = Load();
        Save(saveData);
    }

    public void Save(ArmorSaveData saveData)
    {
        string jsonDate = JsonUtility.ToJson(saveData); //saveDataに入っている変数とその値をjsonに入れるために変換してjsonData変数に代入(すべてstring型にしたりもろもろ)

        using (StreamWriter streamWriter = new StreamWriter(filePath)) //書き込む
        {
            streamWriter.Write(jsonDate); //jsonDataに入った変数と値を実際に書き込む
        }
    }

    public ArmorSaveData Load()
    {
        if (File.Exists(filePath)) //最初にセーブデータがすでにあるか確認
        {
            StreamReader streamReader;

            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd(); //jsonデータをすべて読み込みdata変数に代入
            streamReader.Close();

            return JsonUtility.FromJson<ArmorSaveData>(data); //dataをこっちで読み込めるように変換してsaveDataに代入
        }

        ArmorSaveData savedata = new ArmorSaveData(); //セーブデータがなかったら
        saveData.armorFlag[0] = 0;
        savedata.armorHave[0] = 0;
        return saveData; //その後上書きする
    }

    public void PushSaveButton()
    {
        Save(saveData);
    }

    public void PushLoadButton()
    {
        ArmorSaveData saveData = Load();
    }
}
