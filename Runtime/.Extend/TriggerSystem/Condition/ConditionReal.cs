﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TriggerSystem
{
    public class ConditionReal : TriggerCondition
    {
        const int customDropdownWidth = 200;
        [HorizontalGroup("Group A"), ValueDropdown("GlobalReal", ExpandAllMenuItems = true, DropdownWidth = customDropdownWidth)] public string RefA = "Value";
        [HorizontalGroup("Group A"), ShowIf("RefA", "Value")] public float valueA;
        public ConditionOperator operation;
        [HorizontalGroup("Group B"), ValueDropdown("GlobalReal", ExpandAllMenuItems = true, DropdownWidth = customDropdownWidth)] public string RefB = "Value";
        [HorizontalGroup("Group B"), ShowIf("RefB", "Value")] public float valueB;

        private IEnumerable<string> GlobalReal()
        {
            var globalValue = typeof(DummyPlayerData).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            List<string> _list = new List<string>(){"Value"};
            foreach (var item in globalValue)
            {
                //必須要是 Public , 具有 Tooltip 標籤  , 類型為 float
                if(System.Attribute.IsDefined(item, typeof(TooltipAttribute)) && item.FieldType == typeof(float) ){
                    _list.Add(item.Name);
                }
            }
            var ResponseValue = typeof(TriggerHandle).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var item in ResponseValue)
            {
                //必須要是 Public , 具有 Tooltip 標籤  , 類型為 int
                if(System.Attribute.IsDefined(item, typeof(TooltipAttribute)) && item.FieldType == typeof(float?) ){
                    _list.Add(item.Name);
                }
            }
            return _list;
        }

        public override bool GetConditionFunc() { return CompareValue(GetValueA(), GetValueB(), operation); }
        public float GetValueA() { return TriggerVariable.GetRealValue(RefA) ?? valueA; }
        public float GetValueB() { return TriggerVariable.GetRealValue(RefB) ?? valueB; }
    }
}