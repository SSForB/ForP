using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{

    string StageName;
    string StageSceneName;

    // Start is called before the first frame update
    void Start()
    {
        Text StageNameText = GameObject.Find("StageNameText").GetComponent<Text>();
        StageName = PlayerPrefs.GetString("StageName", "");
        StageSceneName = PlayerPrefs.GetString("StageSceneName", "TiTleScene");
        StageNameText.text = StageName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushBackButton()
    {
        SceneManager.LoadScene(StageSceneName);
    }
}
