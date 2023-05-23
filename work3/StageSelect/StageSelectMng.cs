using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectMng : MonoBehaviour
{
    GameObject mapImage;
    Transform mapTransform;
    Vector3 mapPosition;
    Vector3 clickPos;
    Vector3 beforePos;
    Vector3 movePos;
    public int balance;
    string stageName;

    void Start()
    {
        mapImage = GameObject.Find("MapImage");
        mapTransform = mapImage.transform;
        mapPosition = mapImage.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            beforePos = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            clickPos = Input.mousePosition;
            movePos = beforePos - clickPos;
            movePos = movePos / balance;
            mapPosition -= movePos;
            mapTransform.position = mapPosition;
            beforePos = clickPos;
        }
    }

    public void StageSelect(Button buttonName)
    {
        stageName = buttonName.name;
        SceneManager.LoadScene(stageName);
    }
}
