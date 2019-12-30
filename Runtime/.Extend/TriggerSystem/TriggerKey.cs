using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

//[CreateAssetMenu (menuName = "GameTriggerKey")]
[GlobalConfig ("ScriptableObjects/Resources/")]
public class TriggerKey : GlobalConfig<TriggerKey>
{
    [Header("放入所有的Trigger")]
    [AssetList(Path = "/ScriptableObjects/Trigger/", AutoPopulate = true)]
    public List<TriggerSystem.Trigger> GroupTrigger;
}
