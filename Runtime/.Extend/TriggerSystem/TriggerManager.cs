using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TriggerSystem
{
    public class TriggerManager : SerializedMonoBehaviour
    {
        [Header("不使用GlobalConfig 避免 bundle 重複")] public TriggerKey triggerKeyAsset;

        [TitleGroup("Trigger List")]
        public List<TriggerRuntime> TriggerRuntimes = new List<TriggerRuntime>();

        [TitleGroup("Tools")]
        [Button("Rebuild Trigger (Runtime only)", ButtonSizes.Medium)]
        public void RebuildTrigger(){
            if(!Application.isPlaying)
                return;

            foreach (var item in TriggerRuntimes)
            {
                item.DoUnload();
            }
            TriggerRuntimes.Clear();

            ProcessEvent();
            Debug.Log("Rebuild Trigger Finished");
        }

        [TitleGroup("Tools")]
        [Button("Invoke Test Module (Runtime only)", ButtonSizes.Medium)]
        public void TestModuleInvoke(){
            TriggerHandle.Data = new TriggerHandle();
            EventSignals.DoGameEvent(EventIndex.EVENT_TEST_MODULE);
        }

        void Awake()
        {
            ProcessEvent();
        }

        void OnDestroy() {
            foreach (var item in TriggerRuntimes)
            {
                item.DoUnload();
            }
        }

        void ProcessEvent(){

            foreach (var eachTrigger in triggerKeyAsset.GroupTrigger)
            {
                TriggerRuntime newTrigger = new TriggerRuntime();
                newTrigger.TriggerName = eachTrigger.name;
                newTrigger.isEnable = eachTrigger.isEnable;

                foreach (var item in eachTrigger.ListEvent)
                {
                    newTrigger.RegisterNormalEvent(item);
                }
                foreach (var item in eachTrigger.ListCondition)
                {
                    newTrigger.AddCondition(item);
                    
                }
                foreach (var item in eachTrigger.ListAction)
                {
                    newTrigger.AddAction(item);
                }

                TriggerRuntimes.Add(newTrigger);
            }
        }
    }
}