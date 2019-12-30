using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HimeLib
{
    public class RandomValue
    {
        /// <summary>
        /// 獲得視覺上較可接受的隨機顏色
        /// </summary>
        public static Color GetRandomColor()
        {
            //float color, float density, float light
            //return Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0.3f, 0.75f), 0.8f);

            return Random.ColorHSV( 0, 1,
                                    0.3f,0.75f,
                                    0.8f, 0.8f);
        }

        /// <summary>
        /// 獲得完全隨機顏色
        /// </summary>
        public static Color GetAllRandomColor(){
            return Random.ColorHSV();
        }
    }
}