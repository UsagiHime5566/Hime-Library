using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TriggerSystem
{
    //[Serializable]
    public abstract class BaseBooleanFunction
    {
        public virtual bool GetBoolResult() { return false; }
    }
}
