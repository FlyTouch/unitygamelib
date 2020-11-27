using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private AppFacade m_Facade;
    private TimerManager m_TimerMgr;


    /// <summary>
    /// 注册消息
    /// </summary>
    /// <param name="view"></param>
    /// <param name="messages"></param>
    protected void RegisterMessage(IView view, List<string> messages)
    {
        if (messages == null || messages.Count == 0) return;
        Controller.Instance.RegisterViewCommand(view, messages.ToArray());
    }

    /// <summary>
    /// 移除消息
    /// </summary>
    /// <param name="view"></param>
    /// <param name="messages"></param>
    protected void RemoveMessage(IView view, List<string> messages)
    {
        if (messages == null || messages.Count == 0) return;
        Controller.Instance.RemoveViewCommand(view, messages.ToArray());
    }

    protected AppFacade facade
    {
        get
        {
            if (m_Facade == null)
            {
                m_Facade = AppFacade.Instance;
            }
            return m_Facade;
        }
    }

    protected TimerManager TimerManager
    {
        get
        {
            if (m_TimerMgr == null)
            {
                m_TimerMgr = facade.GetManager<TimerManager>(ManagerName.Timer);
            }
            return m_TimerMgr;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
