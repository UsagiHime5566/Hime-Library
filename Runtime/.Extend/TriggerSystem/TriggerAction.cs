using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem
{
    public abstract class TriggerAction
    {
        protected const int customDropdownWidth = 200;
        protected const string STR_Object = "(Object)";
        protected const string STR_Boolean = "(Boolean)";
        protected const string STR_Integer = "(Integer)";
        protected const string STR_Real = "(Real)";
        public virtual void GetActionFunc(){}

        public IEnumerable<string> GetGlobalVarsWithValue(){
            List<string> _list = new List<string>(){STR_Object, STR_Boolean, STR_Integer, STR_Real};
            _list.AddRange(GetGlobalVars());
            return _list;
        }

        private IEnumerable<string> GetGlobalVars()
        {
            var globalValue = typeof(DummyPlayerData).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            List<string> _list = new List<string>();
            foreach (var item in globalValue)
            {
                //必須要是 Public , 具有 Tooltip 標籤  , 類型為 int , float
                if(System.Attribute.IsDefined(item, typeof(TooltipAttribute)) && (item.FieldType == typeof(int) || item.FieldType == typeof(float)) ){
                    _list.Add(item.Name);
                }
            }
            var ResponseValue = typeof(TriggerHandle).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var item in ResponseValue)
            {
                //必須要是 Public , 具有 Tooltip 標籤  , 類型為 int
                if(System.Attribute.IsDefined(item, typeof(TooltipAttribute)) && (item.FieldType == typeof(int?) || item.FieldType == typeof(float?)) ){
                    _list.Add(item.Name);
                }
            }
            return _list;
        }
    }
}

