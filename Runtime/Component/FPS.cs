using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class FPS : MonoBehaviour {

    public bool IsEnable = true;
 
    const int sampleCount = 60;
    int[] fpsData = new int[sampleCount];
    int index;
 
    int highestFPS;
    int averageFPS;
    int lowestFPS = int.MaxValue;
 
    const float updateTime = .5f;
    float currentTime;

    public Text FPS1;
    public Text FPS2;
    public Text FPS3;
 
    void Start () {
         if(IsEnable == false){
             this.enabled = false;
             FPS1.enabled = false;
             FPS2.enabled = false;
             FPS3.enabled = false;
         }
    }
 
    void Update()
    {
        currentTime += Time.deltaTime;
        //caculate fps
        fpsData[index++ % sampleCount] = (int)(1f / Time.unscaledDeltaTime);
 
        if (currentTime < updateTime) return;
        else currentTime = 0;
        //reset fps data
        if (index >= sampleCount)
        {
            //index = 0;
            highestFPS = 0;
            lowestFPS = int.MaxValue;
        }
        int sum = 0;
        for (int i = 0; i < sampleCount; i++)
        {
            sum += fpsData[i];
            if (fpsData[i] > highestFPS)
                highestFPS = fpsData[i];
            if (fpsData[i] < lowestFPS)
                lowestFPS = fpsData[i];
        }
        averageFPS = sum / sampleCount;

        FPS1.text = string.Format("Highest FPS:{0}", highestFPS);
        FPS2.text = string.Format("Average FPS:{0}", averageFPS);
        FPS3.text = string.Format("Lowest FPS:{0}", lowestFPS);
    }
    
}