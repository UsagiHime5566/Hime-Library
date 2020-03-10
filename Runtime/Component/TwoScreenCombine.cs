using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoScreenCombine : MonoBehaviour
{
    public readonly string tip = "F4 Editor編輯模式 , F5 左右螢幕模式 , F6 右左螢幕模式, ZX 調整螢幕間寬度, Del 寬度恢復預設";
    public int displayMode = 0;
    public int splitAmount = 0;

    public Camera CameraScene1;
    public Camera CameraScene2;
    public Camera CameraMonitor;

    float Camera1_X;
    float Camera2_X;

    public float splitValue = 0.001f;

    // Use this for initialization
    void Start()
    {
        displayMode = PlayerPrefs.GetInt("DisplayMode", 0);
        splitAmount = PlayerPrefs.GetInt("SplitAmount", 0);

        Camera1_X = CameraScene1.transform.position.x;
        Camera2_X = CameraScene2.transform.position.x;

        SwitchDisplayMode(displayMode);
        SetScreenSplitAmount(splitAmount);

        Screen.fullScreen = true;
        Cursor.visible = false;

        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON.
        // Check if additional displays are available and activate each.
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        if (Display.displays.Length > 2)
            Display.displays[2].Activate();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.F5)){
            displayMode = 0;
            SaveDisplayKey();
            SwitchDisplayMode(displayMode);
        }
        if(Input.GetKeyDown(KeyCode.F6)){
            displayMode = 1;
            SaveDisplayKey();
            SwitchDisplayMode(displayMode);
        }
        if(Input.GetKeyDown(KeyCode.F4)){
            displayMode = 2;
            SaveDisplayKey();
            SwitchDisplayMode(displayMode);
        }
        if(Input.GetKeyDown(KeyCode.F3)){
            Screen.fullScreen = !Screen.fullScreen;
        }
        if(Input.GetKey(KeyCode.Z)){
            splitAmount++;
            SaveDisplayKey();
            SetScreenSplitAmount(splitAmount);
        }
        if(Input.GetKey(KeyCode.X)){
            splitAmount--;
            SaveDisplayKey();
            SetScreenSplitAmount(splitAmount);
        }
        if(Input.GetKey(KeyCode.Delete)){
            splitAmount = 0;
            SaveDisplayKey();
            SetScreenSplitAmount(splitAmount);
        }
    }

    public void SwitchDisplayMode(int mode){
        if(mode == 0){
            CameraMonitor.targetDisplay = 2;
            CameraScene1.targetDisplay = 0;
            CameraScene2.targetDisplay = 1;
        }
        else if(mode == 1)
        {
            CameraMonitor.targetDisplay = 2;
            CameraScene1.targetDisplay = 1;
            CameraScene2.targetDisplay = 0;
        }
        else if(mode == 2)
        {
            CameraMonitor.targetDisplay = 0;
            CameraScene1.targetDisplay = 1;
            CameraScene2.targetDisplay = 2;
        }
    }

    public void SetScreenSplitAmount(int splitAmount){
        CameraScene1.transform.position = new Vector3(Camera1_X - splitAmount * splitValue, CameraScene1.transform.position.y, CameraScene1.transform.position.z);
        CameraScene2.transform.position = new Vector3(Camera2_X + splitAmount * splitValue, CameraScene1.transform.position.y, CameraScene1.transform.position.z);
    }

    void SaveDisplayKey(){
        PlayerPrefs.SetInt("DisplayMode", displayMode);
        PlayerPrefs.SetInt("SplitAmount", splitAmount);
    }
}

