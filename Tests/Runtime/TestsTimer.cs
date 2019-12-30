using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HimeLib;

public class TestsTimer : MonoBehaviour
{
    private IEnumerator coroutine;

    public int count = 60;

    void Start()
    {
        // XiaTimer Tests declare
        coroutine = XiaTimer.Start(0.5f, true, () => {
			count--;
            if (count <= 0) StopCoroutine(coroutine);
		});

        // Run XiaTimer
        StartCoroutine(coroutine);
        StartCoroutine(XiaTimer.NextFrame(() => {
            Debug.Log("Something in next frame");
        }));

        // DsTimer Tests
        DsTimerData td1 = DsTimer.Instance.AddTimer(5, () =>
            {
                Debug.Log("5秒後調用該方法1");
            });

        DsTimerData td2 = DsTimer.Instance.AddIntervalTimer(5, 1, () =>
            {
                Debug.Log("5秒後調用該方法2");
            },
            (float remainTime) =>
            {
                Debug.Log("每一秒調用執行此處：剩餘時間" + remainTime);
            }
        );

    }
}
