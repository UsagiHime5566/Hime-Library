﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoCameraTrapezoidMapping : MonoBehaviour
{
    public readonly string tip1 = @"使用3個 Camera, 其中兩個Camera 輸出至RenderTexture, 第3個Camera 拍攝兩個RawImage(RenderTexture)";
    public readonly string tip2 = @"F1F2 設置要調整的目標RawImage, WASD上下左右, QE旋轉, ZX水平縮放, CV垂直縮放, 倒退鍵 恢復預設";
    public RawImage rawImage1;
    public RawImage rawImage2;

    public float moveFactor = 0.1f;
    public float scaleFactor = 0.1f;

    RectTransform rt1;
    RectTransform rt2;

    RectTransform targetRt;

    void Start()
    {
        rt1 = rawImage1.GetComponent<RectTransform>();
        rt2 = rawImage2.GetComponent<RectTransform>();

        float _x = PlayerPrefs.GetFloat("r1_pos_x", 0);
        float _y = PlayerPrefs.GetFloat("r1_pos_y", 0);
        float _rot_x = PlayerPrefs.GetFloat("r1_rot_x", 0);
        float _s_x = PlayerPrefs.GetFloat("r1_scale_x", 1);
        float _s_y = PlayerPrefs.GetFloat("r1_scale_y", 1);

        rt1.anchoredPosition = new Vector2(_x, _y);
        rt1.rotation = Quaternion.Euler(_rot_x, 0, 0);
        rt1.localScale = new Vector3(_s_x, _s_y, 1);

        _x = PlayerPrefs.GetFloat("r2_pos_x", 0);
        _y = PlayerPrefs.GetFloat("r2_pos_y", 0);
        _rot_x = PlayerPrefs.GetFloat("r2_rot_x", 0);
        _s_x = PlayerPrefs.GetFloat("r2_scale_x", 1);
        _s_y = PlayerPrefs.GetFloat("r2_scale_y", 1);

        rt2.anchoredPosition = new Vector2(_x, _y);
        rt2.rotation = Quaternion.Euler(_rot_x, 0, 0);
        rt2.localScale = new Vector3(_s_x, _s_y, 1);

        targetRt = rt1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F1)){
            targetRt = rt1;
        }
        if(Input.GetKey(KeyCode.F2)){
            targetRt = rt2;
        }
        if(Input.GetKey(KeyCode.W)){
            targetRt.anchoredPosition = targetRt.anchoredPosition + new Vector2(0, moveFactor);
            SaveWarpParam();
        }
        if(Input.GetKey(KeyCode.S)){
            targetRt.anchoredPosition = targetRt.anchoredPosition + new Vector2(0, -moveFactor);
            SaveWarpParam();
        }
        if(Input.GetKey(KeyCode.A)){
            targetRt.anchoredPosition = targetRt.anchoredPosition + new Vector2(-moveFactor, 0);
            SaveWarpParam();
        }
        if(Input.GetKey(KeyCode.D)){
            targetRt.anchoredPosition = targetRt.anchoredPosition + new Vector2(moveFactor, 0);
            SaveWarpParam();
        }
        if(Input.GetKey(KeyCode.Q)){
            targetRt.Rotate(new Vector3(-moveFactor, 0, 0),Space.Self);
            SaveWarpParam();
        }
        if(Input.GetKey(KeyCode.E)){
            targetRt.Rotate(new Vector3(moveFactor, 0, 0),Space.Self);
            SaveWarpParam();
        }
        if(Input.GetKey(KeyCode.Z)){
            targetRt.localScale = targetRt.localScale + new Vector3(-scaleFactor, 0, 0);
            SaveWarpParam();
        }
        if(Input.GetKey(KeyCode.X)){
            targetRt.localScale = targetRt.localScale + new Vector3(scaleFactor, 0, 0);
            SaveWarpParam();
        }
        if(Input.GetKey(KeyCode.C)){
            targetRt.localScale = targetRt.localScale + new Vector3(0, -scaleFactor, 0);
            SaveWarpParam();
        }
        if(Input.GetKey(KeyCode.V)){
            targetRt.localScale = targetRt.localScale + new Vector3(0, scaleFactor, 0);
            SaveWarpParam();
        }
        if(Input.GetKey(KeyCode.Backslash)){
            ResetWarp();
            SaveWarpParam();
        }
    }

    public void SaveWarpParam(){
        PlayerPrefs.SetFloat("r1_pos_x", rt1.anchoredPosition.x);
        PlayerPrefs.SetFloat("r1_pos_y", rt1.anchoredPosition.y);
        PlayerPrefs.SetFloat("r1_rot_x", rt1.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("r1_scale_x", rt1.localScale.x);
        PlayerPrefs.SetFloat("r1_scale_y", rt1.localScale.y);

        PlayerPrefs.SetFloat("r2_pos_x", rt2.anchoredPosition.x);
        PlayerPrefs.SetFloat("r2_pos_y", rt2.anchoredPosition.y);
        PlayerPrefs.SetFloat("r2_rot_x", rt2.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("r2_scale_x", rt2.localScale.x);
        PlayerPrefs.SetFloat("r2_scale_y", rt2.localScale.y);
    }

    public void ResetWarp(){
        targetRt.anchoredPosition = new Vector2(0, 0);
        targetRt.rotation = Quaternion.Euler(0, 0, 0);
        targetRt.localScale = new Vector3(1, 1, 1);
    }
}