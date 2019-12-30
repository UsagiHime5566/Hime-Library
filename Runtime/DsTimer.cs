using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HimeLib
{
    public class DsTimer : MonoBehaviour
    {
        private static DsTimer instance;
        public static DsTimer Instance
        {
            get
            {
                if (!instance)
                {
                    instance = GameObject.FindObjectOfType<DsTimer>();
                }
                if (!instance)
                {
                    GameObject obj = new GameObject(typeof(DsTimer).ToString());
                    instance = obj.AddComponent<DsTimer>();
                }
                return instance;
            }
        }

        //正在使用的TimerData 
        private List<DsTimerData> mUseTimerDatas = new List<DsTimerData>();
        //空閒的TimerData 
        private List<DsTimerData> mNotUseTimerDatas = new List<DsTimerData>();

        //嘗試從空閒池中取一個TimerData 
        private DsTimerData GetTimerData(bool isAdd = true)
        {
            DsTimerData data = null;
            if (mNotUseTimerDatas.Count <= 0)
            {
                data = new DsTimerData();
            }
            else
            {
                data = mNotUseTimerDatas[0];
                mNotUseTimerDatas.RemoveAt(0);
            }

            mUseTimerDatas.Add(data);

            return data;
        }

        //創建一個計時器
        public DsTimerData AddTimer(float _duration, Action endCallBack, bool _isIgnoreTime = false)
        {
            DsTimerData data = GetTimerData();
            data.Init(_duration, endCallBack, _isIgnoreTime);

            return data;
        }

        //創建一個重複型計時器
        public DsTimerData AddIntervalTimer(float _duration, float _interval, Action _endCallBack, Action<float> _intervalCallBack, bool _isIgnoreTime = false)
        {
            DsTimerData data = GetTimerData();
            data.Init(_duration, _endCallBack, _isIgnoreTime, _interval, _intervalCallBack);

            return data;
        }

        public void Clear(DsTimerData data)
        {
            if (mUseTimerDatas.Remove(data))
            {
                mNotUseTimerDatas.Add(data);
            }
            else
            {
                Debug.LogWarning("GlobalTimer not find TimerData");
            }
        }

        void Update()
        {
            for (int i = 0; i < mUseTimerDatas.Count; ++i)
            {
                if (!mUseTimerDatas[i].Update())
                {
                    //沒更新成功，mUseTimerDatas長度減1，所以需要- -i 
                    --i;
                }
            }
        }
    }

    public class DsTimerData
    {
        //持續時間
        private float mDuration;
        //重複間隔
        private float mInterval;
        //結束回調
        private Action mEndCallBack;
        //每次重複回調
        private Action<float> mIntervalCallBack;
        //是否忽略時間
        private bool isIgnoreTime;
        //計時器
        private float mRunTime;
        //間隔計時器
        private float mRunIntervalTime;

        //初始化
        public void Init(float _duration, Action _endCallBack, bool _isIgnoreTime = false, float _interval = -1f, Action<float> _intervalCallBack = null)
        {
            mDuration = _duration;
            mInterval = _interval;
            mEndCallBack = _endCallBack;
            mIntervalCallBack = _intervalCallBack;
            isIgnoreTime = _isIgnoreTime;
            mRunTime = 0;
            mRunIntervalTime = 0;
        }

        //更新
        public bool Update()
        {
            float deltaTime = isIgnoreTime ? Time.unscaledDeltaTime : Time.deltaTime;
            mRunTime += deltaTime;
            mRunIntervalTime += deltaTime;

            if (mIntervalCallBack != null)
            {
                if (mRunIntervalTime >= mInterval)
                {
                    mRunIntervalTime -= mInterval;
                    mIntervalCallBack(mDuration - mRunTime);
                }
            }

            if (mRunTime >= mDuration)
            {
                if (mEndCallBack != null)
                {
                    mEndCallBack();
                }
                Clear();
                return false;
            }

            return true;
        }

        public void Clear()
        {
            DsTimer.Instance.Clear(this);
        }

        public void AddEndCallBack(Action _endCallBack)
        {
            mEndCallBack += _endCallBack;
        }
    }

    // from : https://blog.csdn.net/z625309640/article/details/78105626
}