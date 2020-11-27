using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDemo : MonoBehaviour 
{
    TimerInfo timerInfo;
    TimerCommond timerCommond;
    public void TimerUpdate()
    {
        Debug.Log("ok"+timerInfo.tick);
    }

    // Start is called before the first frame update
    void Start()
    {
        timerCommond = new TimerCommond() {

            onTimerCallBack = delegate () {
                Debug.Log("ok" + timerInfo.tick);
            }
        };
        timerInfo = new TimerInfo("TimerDemo", timerCommond,10);
           ///启动MVC架构
        AppFacade.Instance.StartUp();
        TimerManager timerManager = AppFacade.Instance.GetManager<TimerManager>(ManagerName.Timer);
        timerManager.AddTimerEvent(timerInfo);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
