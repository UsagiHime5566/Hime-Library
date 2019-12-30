using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HimeLib
{
    public static class SerializeDeepClone
    {
        /// <summary>
        /// 這裏藉助序列化來實現深複製，因此別忘記給需要深複製的對象的類定義上面加上可序列化的標籤[Serializable]。
        /// </summary>
        public static object Clone(this object src)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, src);
            ms.Position = 0;
            return bf.Deserialize(ms);
        }

        // public object Clone()
        // {
        //     BinaryFormatter bf = new BinaryFormatter();
        //     MemoryStream ms = new MemoryStream();
        //     bf.Serialize(ms, this);
        //     ms.Position = 0;
        //     return bf.Deserialize(ms);
        // }
    }
}