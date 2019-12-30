using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

//主題引數基類
public abstract class SubjectArgs
{
    public object sender = null;
    public abstract int SubjectId { get; }
}
//主題管理器
public class Subject_Manager
{
    //主題字典，以主題傳遞的引數型別提供的hashCode為key
    private Dictionary<int, Subject<SubjectArgs>> mSubjectDic = new Dictionary<int, Subject<SubjectArgs>>();
    //通過id拿到主題  
    public IObservable<T> GetSubject<T>() where T : SubjectArgs
    {
        int subjectId = typeof(T).GetHashCode();
        Subject<SubjectArgs> subject = null;
        if (!mSubjectDic.TryGetValue(subjectId, out subject))
        {
            subject = new Subject<SubjectArgs>();
            mSubjectDic.Add(subjectId, subject);

        }
        return subject.Select(_ => _ as T);
    }
    //資料，發射！
    public void Fire<T>(T e) where T : SubjectArgs
    {
        Subject<SubjectArgs> subject = null;
        if (!mSubjectDic.TryGetValue(e.SubjectId, out subject))
            return;

        subject.OnNext(e);
    }

}

public static class SubjectManager
{
    private static Subject_Manager mInstance = new Subject_Manager();
    public static IObservable<T> GetSubject<T>() where T : SubjectArgs
    {
        return mInstance.GetSubject<T>();
    }
    public static void Fire<T>(T e) where T : SubjectArgs
    {
        mInstance.Fire<T>(e);
    }
}