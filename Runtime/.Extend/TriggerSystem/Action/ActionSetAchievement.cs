using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TriggerSystem
{
    public class ActionSetAchievement : TriggerAction
    {
        public string AchievementID = "";
        public override void GetActionFunc()
        {
            //AchievementManager.SetAchievementGeneric(AchievementID);  //it's for steamworks
        }
    }
}