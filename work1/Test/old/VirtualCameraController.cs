﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            //プレイヤーの位置に連動させる
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
}
