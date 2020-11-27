using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCommond : ITimerBehaviour
{

    /// <summary>
    /// 回调
    /// </summary>
    public delegate void OnTimerCallBack();


    public OnTimerCallBack onTimerCallBack;

    /// <summary>
    /// 时间执行
    /// </summary>
    public void TimerUpdate()
    {
        if (onTimerCallBack != null)
        {
            onTimerCallBack.Invoke();
        }
    }
}
