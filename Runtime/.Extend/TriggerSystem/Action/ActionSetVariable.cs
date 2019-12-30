using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TriggerSystem
{
    public class ActionSetVariable : TriggerAction
    {
        [ValueDropdown("GetGlobalVars", ExpandAllMenuItems = true, DropdownWidth = customDropdownWidth)] public string Variable = "none";

        [Header("=")]
        [ValueDropdown("GetGlobalVarsWithValue", ExpandAllMenuItems = true, DropdownWidth = customDropdownWidth)] public string Ref = STR_Object;
        [ShowIf("Ref", STR_Object)] public object valueObj;
        [ShowIf("Ref", STR_Boolean)] public bool valueBool;
        [ShowIf("Ref", STR_Integer)] public int valueInt;
        [ShowIf("Ref", STR_Real)] public float valueReal;
        

        public override void GetActionFunc()
        {
            var objA = TriggerVariable.GetMember(Variable);

            if(Ref == STR_Object)
                objA.SetValue(DummyPlayerData.instance , valueObj);
            else if(Ref == STR_Boolean)
                objA.SetValue(DummyPlayerData.instance , valueBool);
            else if(Ref == STR_Integer)
                objA.SetValue(DummyPlayerData.instance , valueInt);
            else if(Ref == STR_Real)
                objA.SetValue(DummyPlayerData.instance , valueReal);
            else
            {
                var objB = TriggerVariable.GetObjectValue(Ref);
                objA.SetValue(DummyPlayerData.instance , objB);
            }
        }
    }
}