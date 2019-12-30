using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HimeLib
{
    public class CoroutineConst
    {
        /// <summary>
        /// 獲取等待幀末
        /// </summary>
        public static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        /// <summary>
        /// 獲取等待幀末
        /// </summary>
        public static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        private static Dictionary<float, WaitForSeconds> waitForSecondsDict = new Dictionary<float, WaitForSeconds>();

        /// <summary>
        /// 獲取等待秒數
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static WaitForSeconds GetWaitForSeconds(float seconds)
        {
            WaitForSeconds v;
            if (!waitForSecondsDict.TryGetValue(seconds, out v))
            {
                waitForSecondsDict.Add(seconds, new WaitForSeconds(seconds));
                v = waitForSecondsDict[seconds];
            }
            return v;
        }

        private static Dictionary<float, WaitForSecondsRealtime> waitForSecondsRealtimeDict
            = new Dictionary<float, WaitForSecondsRealtime>();

        /// <summary>
        /// 獲取等待秒數
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static WaitForSecondsRealtime GetWaitForSecondsReadtime(float seconds)
        {
            WaitForSecondsRealtime v;
            if (!waitForSecondsRealtimeDict.TryGetValue(seconds, out v))
            {
                waitForSecondsRealtimeDict.Add(seconds, new WaitForSecondsRealtime(seconds));
                v = waitForSecondsRealtimeDict[seconds];
            }
            return v;
        }

    }
}