using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public string[] weaponName = new string[999]; //とりあえず領域確保
    public string[] weaponID = new string[999]; //とりあえず領域確保
    public int[] weaponPower = new int[999]; //とりあえず領域確保
}


public class JsonTest : MonoBehaviour
{
    string filePath; //保存先のパスを指定するための変数
    SaveData saveData;
    int count = 0; //配列の番号を指定するための変数

    public ScriptabeObjectTest scriptabeObject; //ScriptabeObjectをアタッチするための変数
    ScriptabeObjectTest.Param param; //ScriptabeObject内のパラメータクラスを取得するための変数　リストの何番目の配列か指定する

    void Awake()
    {
        filePath = Application.dataPath + "/saveData.json"; //セーブデータを置く場所と、名前を指定
        saveData = new SaveData();
        param = scriptabeObject.list[count]; //paramに配列のcount番目を指定
    }

    public void Save(SaveData saveData)
    {
        string jsonDate = JsonUtility.ToJson(saveData); //saveDataに入っている変数とその値をjsonに入れるために変換してjsonData変数に代入(すべてstring型にしたりもろもろ)

        using (StreamWriter streamWriter = new StreamWriter(filePath)) //書き込む
        {
            streamWriter.Write(jsonDate); //jsonDataに入った変数と値を実際に書き込む
        }
    }

    public SaveData Load()
    {
        if (File.Exists(filePath)) //最初にセーブデータがすでにあるか確認
        {
            StreamReader streamReader;

            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd(); //jsonデータをすべて読み込みdata変数に代入
            streamReader.Close();

            return JsonUtility.FromJson<SaveData>(data); //dataをこっちで読み込めるように変換してsaveDataに代入
        }

        SaveData savedata = new SaveData(); //セーブデータがなかったら
        saveData.weaponName[0] = "a"; //適当な数値を入れて
        saveData.weaponID[0] = "a"; //まずはセーブデータファイルを作る
        saveData.weaponPower[0] = 0;
        return saveData; //その後上書きする
    }
    public void PushSaveButton()
    {
        SaveData save = new SaveData();

        count = 0; //数値が違うものになってることもあるのでいったん初期化

        while (param.weaponName != "") //scriptabeObjectの配列の武器名が空じゃないとき
        {
            Debug.Log(count);
            param = scriptabeObject.list[count]; //まずは配列の何番目か指定 初期は0

            saveData.weaponName[count] = param.weaponName.ToString(); //指定した配列の武器名を代入
            saveData.weaponID[count] = param.id; //指定した配列の武器IDを代入
            saveData.weaponPower[count] = param.weaponPower; //指定した配列の武器の威力を代入

            count++; //次の配列に進めるためカウントを１進める
            Save(saveData); //セーブ
            param = scriptabeObject.list[count]; //1進んだ数値の配列を代入
        }
    }

    public void PushLoadButton()
    {
        SaveData saveData = Load();
        count = 0; //数値が違うものになってることもあるのでいったん初期化

        while (saveData.weaponName[count] != "") //saveDataの武器名が空になるまで繰り返す
        {
            Debug.Log("武器名は" + saveData.weaponName[count]+"です。武器のIDは" + saveData.weaponID[count]+"です。武器の威力は"+saveData.weaponPower[count]+"です。");
            count++;
        }
    }
}
