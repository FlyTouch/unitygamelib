using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 接口类，主要用户回调参数
/// </summary>
public interface ISocketMessage 
{
    void NetCoreCallBack(NetCoreBackData netCoreBackData);
}
