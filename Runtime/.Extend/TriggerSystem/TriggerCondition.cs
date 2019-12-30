using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TriggerSystem
{
    public abstract class TriggerCondition
    {
        public enum ConditionOperator {
            Equal,
            NotEqual,
            Greater,
            GreaterEqual,
            Less,
            LessEqual,
        }

        public virtual bool GetConditionFunc(){
            return false;
        }

        public virtual bool CompareValue<T>(T a, T b, ConditionOperator operation) where T : System.IComparable {
            switch (operation)
            {
                case ConditionOperator.Equal:
                    if (a.CompareTo(b) == 0)
                        return true;
                    break;
                case ConditionOperator.NotEqual:
                    if (a.CompareTo(b) != 0)
                        return true;
                    break;
                case ConditionOperator.Greater:
                    if (a.CompareTo(b) > 0)
                        return true;
                    break;
                case ConditionOperator.GreaterEqual:
                    if (a.CompareTo(b) > 0 || a.CompareTo(b) == 0)
                        return true;
                    break;
                case ConditionOperator.Less:
                    if (a.CompareTo(b) < 0)
                        return true;
                    break;
                case ConditionOperator.LessEqual:
                    if (a.CompareTo(b) < 0 || a.CompareTo(b) == 0)
                        return true;
                    break;
                default:
                    Debug.LogWarning("Exception ConditionOperator");
                    break;
            }
            //when Not Equal return false
            return false;
        }
    }
}