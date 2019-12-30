using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem
{
    public class TriggerRuntime
    {
        public string TriggerName = "NoName Trigger";
        public bool isEnable = true;
        public bool ignoreCondition = false;

        protected Func<bool> DoCondition;
        protected Action DoActions;
        protected Action Unload;

        public void RegisterNormalEvent(EventIndex index){
            EventSignals.OnGameEvent[(int)index] += RunTrigger;
            Unload += () => {
                EventSignals.OnGameEvent[(int)index] -= RunTrigger;
            };
        }

        public void AddCondition(TriggerCondition cond){
            DoCondition += cond.GetConditionFunc;
        }

        public void AddConditionCode(Func<bool> cond){
            DoCondition += cond;
        }

        public void AddAction(TriggerAction act){
            DoActions += act.GetActionFunc;
        }

        public void AddActionCode(Action act){
            DoActions += act;
        }

        public void RunTrigger(){
            if(isEnable == false)
                return;

            if(ignoreCondition)
            {
                DoActions?.Invoke();
                return;
            }
            
            if(DoCondition != null)
                foreach (Func<bool> cond in DoCondition.GetInvocationList()) {
                    if(cond() == false)
                        return;
                }

            DoActions?.Invoke();
        }

        public void DoUnload(){
            Unload?.Invoke();
        }
    }
}
