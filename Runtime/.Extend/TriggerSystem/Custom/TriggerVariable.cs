using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem
{
    public class TriggerVariable
    {
        public static bool? GetBooleanValue(BaseBooleanFunction Ref){
            if(Ref == null)
                return null;
            
            //Future work

            return null;
        }

        public static int? GetIntValue(string Ref){
            if(Ref == "Value")
                return null;

            var nResult = GetMemberValue(DummyPlayerData.instance, Ref) ?? GetMemberValue(TriggerHandle.Data, Ref);

            if(nResult == null)
                Debug.Log("Get int value fault from Name : " + Ref);

            return nResult as int?;
        }

        public static float? GetRealValue(string Ref){
            if(Ref == "Value")
                return null;

            var nResult = GetMemberValue(DummyPlayerData.instance, Ref) ?? GetMemberValue(TriggerHandle.Data, Ref);

            if(nResult == null)
                Debug.Log("Get float value fault from Name : " + Ref);
                
            return nResult as float?;
        }

        public static string GetStringValue(string Ref){
            if(Ref == "Value")
                return null;

            var nResult = GetMemberValue(DummyPlayerData.instance, Ref) ?? GetMemberValue(TriggerHandle.Data, Ref);

            if(nResult == null)
                Debug.Log("Get string value fault from Name : " + Ref);
                
            return nResult as string;
        }

        public static object GetObjectValue(string Ref){
            var result = GetMemberValue(DummyPlayerData.instance, Ref) ?? GetMemberValue(TriggerHandle.Data, Ref);

            if(result == null)
                Debug.Log("Get game value fault from Name : " + Ref);
            
            return result;
        }

        public static object GetMemberValue(object src, string propName){
            //Debug.Log("By " + src + " / " + propName);
            var element = src.GetType().GetField(propName);
            return element?.GetValue(src);
        }

        public static System.Reflection.FieldInfo GetMember(string Ref){
            return GetMemberObject(DummyPlayerData.instance, Ref);
        }

        public static System.Reflection.FieldInfo GetMemberObject(object src, string propName){
            return src.GetType().GetField(propName);
        }
    }
}