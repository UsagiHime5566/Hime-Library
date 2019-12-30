using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem
{
    //[Serializable]
    public class HasFinWeekSpecial : BaseBooleanFunction
    {
        //public SpecialWeekContent specialWeekContent; // for other game to design
        public ScriptableObject specialWeekContent;

        public override bool GetBoolResult(){
            //List<string> _list = MainManager.instance.PlayerData.WeekSpecialMemo;   // for other game to design
            List<string> _list = new List<string>();

            if(_list == null)
                return false;

            foreach (string item in _list)
            {
                if(item == specialWeekContent.name)
                    return true;
            }

            return false;
        }
    }
}