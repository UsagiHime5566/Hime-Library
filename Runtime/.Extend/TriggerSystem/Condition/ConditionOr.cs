using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TriggerSystem
{
    public class ConditionOr : TriggerCondition
    {
        public List<TriggerCondition> OrList;

        public override bool GetConditionFunc() {
            if(OrList == null){
                    return true;
                }
            
            foreach (var item in OrList)
            {
                if(item.GetConditionFunc() == true)
                    return true;
            }

            return false;
        }
    }
}