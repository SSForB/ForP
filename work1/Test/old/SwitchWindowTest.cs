using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWindowTest : MonoBehaviour
{
    GameObject menu;
    bool menuON = false;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("MenuImage");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(!menuON)
            {
                menu.SetActive(true);
                menuON = true;
            }
            else
            {
                menu.SetActive(false);
                menuON = false;
            }
        }
    }
}
