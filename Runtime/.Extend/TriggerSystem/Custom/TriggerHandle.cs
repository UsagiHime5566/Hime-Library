using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem
{
    public class TriggerHandle
    {
        static TriggerHandle _handle;

        public static TriggerHandle Data {
            get { return _handle; }
            set { _handle = value; }
        }

        [Tooltip("事件回應 - 獲得金錢量")] public int? EventResponse_Coin_Get;
        [Tooltip("事件回應 - 花費金錢量")] public int? EventResponse_Coin_Lost;
        [Tooltip("事件回應 - 獲得的壓力值")] public float? EventResponse_Pressure_Get;
        [Tooltip("事件回應 - 減少的壓力值")] public float? EventResponse_Pressure_Reduce;
        [Tooltip("事件回應 - 獲得的道具")] public string EventResponse_Item_Get;
        [Tooltip("事件回應 - 特殊週的名稱")] public string EventResponse_Week_Name;
    }
}