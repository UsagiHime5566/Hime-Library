using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TriggerSystem
{
    public class ActionShowMessage : TriggerAction
    {
        public string Message;

        [ValueDropdown("GetGlobalVars", ExpandAllMenuItems = true, DropdownWidth = customDropdownWidth)]
        public List<string> TakeParam;

        public override void GetActionFunc()
        {
            List<object> objList = new List<object>();
            foreach (var item in TakeParam)
            {
                var obj = TriggerVariable.GetObjectValue(item);
                if(obj != null)
                    objList.Add(obj);
            }
            string _msg = string.Format(Message, objList.ToArray());

            Debug.Log(_msg);
        }
    }
}