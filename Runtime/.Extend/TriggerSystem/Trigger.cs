using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TriggerSystem
{
    [CreateAssetMenu (menuName = "GameEvent")]
    public class Trigger : SerializedScriptableObject
    {
        [Header("是否開啟")]
        public bool isEnable = true;
        
        [Header("觸發事件")]
        public List<EventIndex> ListEvent = new List<EventIndex>();

        [Header("觸發條件"), ValueDropdown("TreeViewOfCondition", ExpandAllMenuItems = true)]
        public List<TriggerCondition> ListCondition = new List<TriggerCondition>();

        [Header("觸發行動")]
        public List<TriggerAction> ListAction = new List<TriggerAction>();

        private IEnumerable TreeViewOfCondition = new ValueDropdownList<TriggerCondition>()
        {
            { "Or", new ConditionOr() },
            { "And", new ConditionAnd() },
            { "Boolean", new ConditionBoolean() },
            { "Integer", new ConditionInteger() },
            { "Real", new ConditionReal() },
            { "String", new ConditionString() },
        };

        private IEnumerable TreeViewOfAction = new ValueDropdownList<TriggerAction>()
        {
            { "Debug.Log", new ActionShowMessage() },
            { "Set Variable", new ActionSetVariable() },
        };

        

        ////////////////
        ////////////////
        ////////////////

    }
}
