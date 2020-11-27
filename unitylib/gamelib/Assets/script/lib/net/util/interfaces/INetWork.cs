using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 网络使用接口，HTTP,UDP,TCP,均需集成此接口
/// </summary>
public interface INetWork 
{
    /// <summary>
    /// 定义初始化接口
    /// </summary>
    /// <param name="netConfig"></param>
    void InitLize(NetConfig netConfig);

    /// <summary>
    /// 开启进程
    /// </summary>
    bool Start();

    /// <summary>
    /// 销毁进程
    /// </summary>
    void Dispose();

    /// <summary>
    /// 定义发送接口
    /// </summary>
    /// <param name="netSendModel"></param>
    void Send(NetSendModel netSendModel);

}
