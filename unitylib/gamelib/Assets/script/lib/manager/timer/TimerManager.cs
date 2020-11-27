using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Manager
{
    private float interval = 0;
    private List<TimerInfo> objects = new List<TimerInfo>();

    public float Interval
    {
        get { return interval; }
        set { interval = value; }
    }

    // Use this for initialization
    void Start()
    {
        StartTimer(AppConst.TimerInterval);
    }

    /// <summary>
    /// 启动计时器
    /// </summary>
    /// <param name="interval"></param>
    public void StartTimer(float value)
    {
        interval = value;
        InvokeRepeating("Run", 0, interval);
    }

    /// <summary>
    /// 停止计时器
    /// </summary>
    public void StopTimer()
    {
        CancelInvoke("Run");
    }

    /// <summary>
    /// 添加计时器事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="o"></param>
    public void AddTimerEvent(TimerInfo info)
    {
        if (!objects.Contains(info))
        {
            objects.Add(info);
        }
    }

    /// <summary>
    /// 删除计时器事件
    /// </summary>
    /// <param name="name"></param>
    public void RemoveTimerEvent(TimerInfo info)
    {
        if (objects.Contains(info) && info != null)
        {
            info.delete = true;
        }
    }

    /// <summary>
    /// 停止计时器事件
    /// </summary>
    /// <param name="info"></param>
    public void StopTimerEvent(TimerInfo info)
    {
        if (objects.Contains(info) && info != null)
        {
            info.stop = true;
        }
    }

    /// <summary>
    /// 继续计时器事件
    /// </summary>
    /// <param name="info"></param>
    public void ResumeTimerEvent(TimerInfo info)
    {
        if (objects.Contains(info) && info != null)
        {
            info.delete = false;
        }
    }

    /// <summary>
    /// 计时器运行
    /// </summary>
    void Run()
    {
        if (objects.Count == 0) return;
        for (int i = 0; i < objects.Count; i++)
        {
            TimerInfo o = objects[i];
            if (o.delete || o.stop) { continue; }
             
            o.target.TimerUpdate();
            o.tick++;
            if(o.fream == o.tick)
            {
                o.delete = true;
            }
        }
        /////////////////////////清除标记为删除的事件///////////////////////////
        for (int i = objects.Count - 1; i >= 0; i--)
        {
            if (objects[i].delete) { objects.Remove(objects[i]); }
        }
    }
}
/// <summary>
/// 定时器
/// </summary>
public class TimerInfo {

    public long tick;
    public bool stop;
    public bool delete;
    public long fream; //定义的执行次数,为0时代表无限循环
    public ITimerBehaviour target;
    public string className;

    public TimerInfo(string className, ITimerBehaviour target,long fream = 0)
    {
        this.className = className;
        this.target = target;
        this.fream = fream;
        delete = false;
    }
}