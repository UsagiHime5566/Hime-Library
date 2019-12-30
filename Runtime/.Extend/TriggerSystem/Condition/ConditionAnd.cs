using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TriggerSystem
{
    public class ConditionAnd : TriggerCondition
    {
        public List<TriggerCondition> AndList;

        public override bool GetConditionFunc() {
            if(AndList == null){
                    return true;
                }
            
            foreach (var item in AndList)
            {
                if(item.GetConditionFunc() == false)
                    return false;
            }

            return true;
        }
    }
}