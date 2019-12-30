using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TriggerSystem
{
    public class ConditionBoolean : TriggerCondition
    {
        const int customDropdownWidth = 200;
        [HorizontalGroup("Group A")] public BaseBooleanFunction RefA = null;
        public ConditionOperator operation;
        [HorizontalGroup("Group B")] public BaseBooleanFunction RefB = null;

        private bool RefA_Visible()
        {
            return RefA == null || RefA.GetType() == typeof(_Value);
        }
        private bool RefB_Visible()
        {
            return RefB == null || RefB.GetType() == typeof(_Value);
        }

        public override bool GetConditionFunc() { return CompareValue(GetValueA(), GetValueB(), operation); }
        public bool GetValueA() {
            if(RefA == null)
                return false;

            return RefA.GetBoolResult();
        }
        public bool GetValueB() {
            if(RefB == null)
                return false;

            return RefB.GetBoolResult();
        }
    }
}