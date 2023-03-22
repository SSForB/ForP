using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SubQuestFlagSaveData
{
    public int[] subQuestFlag = new int[999]; //0:フラグなし 1:フラグあり未クリア 2:フラグありクリア
}


public class JsonSubQuestFlag : MonoBehaviour
{
    int count;
    string filePath; //保存先のパスを指定するための変数
    public SubQuestFlagSaveData saveData;

    void Awake()
    {
        filePath = Application.dataPath + "/savedata/SubQuestFlagSaveData.json"; //セーブデータを置く場所と、名前を指定
        saveData = new SubQuestFlagSaveData();
        saveData = Load();
    }

    public void Save(SubQuestFlagSaveData saveData)
    {
        string jsonDate = JsonUtility.ToJson(saveData); //saveDataに入っている変数とその値をjsonに入れるために変換してjsonData変数に代入(すべてstring型にしたりもろもろ)

        using (StreamWriter streamWriter = new StreamWriter(filePath)) //書き込む
        {
            streamWriter.Write(jsonDate); //jsonDataに入った変数と値を実際に書き込む
        }
    }

    public SubQuestFlagSaveData Load()
    {
        if (File.Exists(filePath)) //最初にセーブデータがすでにあるか確認
        {
            StreamReader streamReader;

            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd(); //jsonデータをすべて読み込みdata変数に代入
            streamReader.Close();

            return JsonUtility.FromJson<SubQuestFlagSaveData>(data); //dataをこっちで読み込めるように変換してsaveDataに代入
        }

        SubQuestFlagSaveData savedata = new SubQuestFlagSaveData(); //セーブデータがなかったら
        saveData.subQuestFlag[0] = 0;
        return saveData; //その後上書きする
    }

    public void PushSaveButton()
    {
        Save(saveData);
    }

    public void PushLoadButton()
    {
        SubQuestFlagSaveData saveData = Load();
    }
}
