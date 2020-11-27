using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager
{
    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Init()
    {
        DontDestroyOnLoad(gameObject);  //防止销毁自己
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = AppConst.GameFrameRate;
    }
}
