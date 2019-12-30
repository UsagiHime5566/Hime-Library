using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem
{
    public class _Value : BaseBooleanFunction
    {
        public bool Value;

        public override bool GetBoolResult(){
            return Value;
        }
    }
}