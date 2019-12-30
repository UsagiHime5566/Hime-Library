using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayerData
{
    public static DummyPlayerData instance;
    
    //必須有 tooltip 標籤的變數才會被 TriggerSystem 讀取到
    [Tooltip("Dummy Boolean")] public bool DummyBoolean;
    [Tooltip("Dummy Integer")] public int DummyInteger;
    [Tooltip("Dummy Float")] public float DummyFloat;
    [Tooltip("Dummy String")] public string DummyString;
}
