using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class HappySubjectArgs : SubjectArgs //Todo用物件池快取這些炮彈...
{
    public static readonly int ID = typeof(HappySubjectArgs).GetHashCode();
    public override int SubjectId { get { return ID; } }
    public int HappyDegree = 0;
}

public class SMTest : MonoBehaviour
{
    //登出器集合，所有訂閱的登出器都往這裡塞
    CompositeDisposable mSubjectUnRegister = new CompositeDisposable();
    void Start()
    {
        //只接收開心程度大於10的發射...
        SubjectManager.GetSubject<HappySubjectArgs>().Where(e => e.HappyDegree > 10)
            .Subscribe(e => print("開心程度:" + e.HappyDegree + "...看起來很開心")).AddTo(mSubjectUnRegister);
        //開心程度會隨著點選增長...
        Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
            .Select((_, count) => count)
            .Subscribe(count => SubjectManager.Fire(new HappySubjectArgs() { HappyDegree = count }));
        //沒有多餘欄位的類...看起來真的很舒爽...
    }
    void OnDestroy()
    {
        //此物件銷燬時登出器登出 就算重複登出也沒關係 這就是UniRx用起來舒服的點點滴滴...
        mSubjectUnRegister.Dispose();
    }
}