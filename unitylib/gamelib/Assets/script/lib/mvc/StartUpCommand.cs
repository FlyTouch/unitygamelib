using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpCommand : ControllerCommand
{
    public override void Execute(IMessage message)
    {
        AppFacade.Instance.AddManager<TimerManager>(ManagerName.Timer);
    }
}
