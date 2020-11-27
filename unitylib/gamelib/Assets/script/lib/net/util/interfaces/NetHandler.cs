using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有关于事件方式计入
/// </summary>
public class NetHandler 
{
    /// <summary>
    /// 定义消息回调
    /// </summary>
    /// <param name="netCoreBackData"></param>
    public delegate void NetCoreCallBack(NetCoreBackData netCoreBackData);
}
