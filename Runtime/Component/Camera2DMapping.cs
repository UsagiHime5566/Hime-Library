﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Camera2DMapping : MonoBehaviour
{
    public readonly string tip = "單Camera 2D縮放平移, 附帶 F12按鍵 結束程式";
    public float moveFactor = 0.05f;

    Camera main;
    float init_x;
    float init_y;
    float init_size;

    void Start()
    {
        main = GetComponent<Camera>();

        init_x = PlayerPrefs.GetFloat("camera_x", 0);
        init_y = PlayerPrefs.GetFloat("camera_y", 0);
        init_size = PlayerPrefs.GetFloat("camera_size", 5);

        transform.position = new Vector3(init_x,init_y, -3);
        main.orthographicSize = init_size;

        Screen.fullScreen = true;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.Translate(0,-moveFactor,0);
            SaveCameraState();
        }
        if(Input.GetKey(KeyCode.S)){
            transform.Translate(0,moveFactor,0);
            SaveCameraState();
        }
        if(Input.GetKey(KeyCode.A)){
            transform.Translate(moveFactor,0,0);
            SaveCameraState();
        }
        if(Input.GetKey(KeyCode.D)){
            transform.Translate(-moveFactor,0,0);
            SaveCameraState();
        }
        if(Input.GetKey(KeyCode.Q)){
            main.orthographicSize += moveFactor;
            SaveCameraState();
        }
        if(Input.GetKey(KeyCode.E)){
            main.orthographicSize -= moveFactor;
            SaveCameraState();
        }
        if(Input.GetKey(KeyCode.F12)){
            SaveCameraState();
            Application.Quit();
        }
    }

    void SaveCameraState(){
        PlayerPrefs.SetFloat("camera_x", transform.position.x);
        PlayerPrefs.SetFloat("camera_y", transform.position.y);
        PlayerPrefs.SetFloat("camera_size", main.orthographicSize);
    }
}

