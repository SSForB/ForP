using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Vector3 gridPos;
    GameObject gridObject;
    RaycastHit hit;
    Ray ray;

    GameObject wallObject;
    GameObject wallObjectDupe;
    GameObject wallObjectStore;

    GameObject playerObject;
    GameObject playerObjectDupe;
    GameObject playerObjectStore;

    Vector3 wallScale;
    Vector3 playerScale;

    bool[] menuBool = new bool[5] { false, false, false, false,false }; //0=summon 1=delete 2=synhthesis 3=wallset 4=start

    void Start()
    {
        wallObject = GameObject.Find("Wall");
        wallObjectStore = GameObject.Find("ForWallObject");
        wallScale = wallObject.transform.localScale;

        playerObject = GameObject.Find("Player");
        playerObjectStore = GameObject.Find("ForPlayerObject");
        playerScale = playerObject.transform.localScale;
        playerObject.SetActive(false);
    }


    void Update()
    {
        //味方召喚
        if(menuBool[0])
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "GridOb")
                {
                    playerObjectDupe = Instantiate(playerObject);
                    playerObjectDupe.SetActive(true);
                    playerObjectDupe.transform.parent = playerObjectStore.transform;
                    playerObjectDupe.transform.localScale = playerScale;
                    playerObjectDupe.transform.Rotate(90, 0, 0);
                    gridObject = hit.collider.gameObject;
                    gridPos = gridObject.transform.localPosition;
                    gridPos.z = -40;
                    playerObjectDupe.transform.localPosition = gridPos;
                }
            }
        }

        //味方or壁削除
        if(menuBool[1])
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Player")
                {
                    Destroy(hit.collider.gameObject);
                }
                else if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Wall")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }

        //壁設置
        if(menuBool[3])
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "GridOb")
                {
                    wallObjectDupe = Instantiate(wallObject);
                    wallObjectDupe.transform.parent = wallObjectStore.transform;
                    wallObjectDupe.transform.localScale = wallScale;
                    wallObjectDupe.transform.Rotate(90, 0, 0);
                    gridObject = hit.collider.gameObject;
                    gridPos = gridObject.transform.localPosition;
                    wallObjectDupe.transform.localPosition = gridPos;
                }
            }
        }
    }

    //押したボタンによって有効化の有無を切り替え
    public void PushButton(int buttonNum)//0=summon 1=delete 2=synhthesis 3=wallset 4=start
    {
        menuBool[buttonNum] = true;

        for (int i = 0; i<5 ; i++)
        {
            if(i == buttonNum)
            {
                continue;
            }
            menuBool[i] = false;
        }
    }
}
