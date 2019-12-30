using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem
{
    //[Serializable]
    public class HasItemOfAsset : BaseBooleanFunction
    {
        //public ItemContent ItemName;  // for other game to design
        public ScriptableObject ItemName;

        public override bool GetBoolResult(){
            //List<string> _list = MainManager.instance.PlayerData.Items;  // for other game to design
            List<string> _list = new List<string>();

            if(_list == null)
                return false;

            foreach (string item in _list)
            {
                if(item == ItemName.name)
                    return true;
            }

            return false;
        }
    }
}