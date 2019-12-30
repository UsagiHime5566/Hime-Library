using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TriggerSystem
{
    public class ConditionInteger : TriggerCondition
    {
        const int customDropdownWidth = 200;
        [HorizontalGroup("Group A"), ValueDropdown("GlobalInteger", ExpandAllMenuItems = true, DropdownWidth = customDropdownWidth)] public string RefA = "Value";
        [HorizontalGroup("Group A"), ShowIf("RefA", "Value")] public int valueA;
        public ConditionOperator operation;
        [HorizontalGroup("Group B"), ValueDropdown("GlobalInteger", ExpandAllMenuItems = true, DropdownWidth = customDropdownWidth)] public string RefB = "Value";
        [HorizontalGroup("Group B"), ShowIf("RefB", "Value")] public int valueB;

        private IEnumerable<string> GlobalInteger()
        {
            var globalValue = typeof(DummyPlayerData).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            List<string> _list = new List<string>(){"Value"};
            foreach (var item in globalValue)
            {
                //必須要是 Public , 具有 Tooltip 標籤  , 類型為 int
                if(System.Attribute.IsDefined(item, typeof(TooltipAttribute)) && item.FieldType == typeof(int) ){
                    _list.Add(item.Name);
                }
            }
            var ResponseValue = typeof(TriggerHandle).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var item in ResponseValue)
            {
                //必須要是 Public , 具有 Tooltip 標籤  , 類型為 int
                if(System.Attribute.IsDefined(item, typeof(TooltipAttribute)) && item.FieldType == typeof(int?) ){
                    _list.Add(item.Name);
                }
            }
            return _list;
        }

        public override bool GetConditionFunc() { return CompareValue(GetValueA(), GetValueB(), operation); }
        public int GetValueA() { return TriggerVariable.GetIntValue(RefA) ?? valueA; }
        public int GetValueB() { return TriggerVariable.GetIntValue(RefB) ?? valueB; }
    }
}