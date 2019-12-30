using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem
{
    //[Serializable]
    public class HasItemOfName : BaseBooleanFunction
    {
        public string ItemName;

        public override bool GetBoolResult(){
            //List<string> _list = MainManager.instance.PlayerData.Items;
            List<string> _list = new List<string>();

            if(_list == null)
                return false;

            foreach (string item in _list)
            {
                if(item == ItemName)
                    return true;
            }

            return false;
        }
    }
}