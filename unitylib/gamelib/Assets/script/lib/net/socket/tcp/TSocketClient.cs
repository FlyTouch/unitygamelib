using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// TCP 客户端连接实例
/// </summary>
public class TSocketClient : SockProxy
{
    /// <summary>
    /// 是否自动重连
    /// </summary>
    public bool autoconnec = true;

    /// <summary>
    /// 是否首次连接
    /// </summary>
    public bool isFirstConnec = true;

    /// <summary>
    /// 是否连接中
    /// </summary>
    public bool isConnection = false;

    /// <summary>
    /// 是否连接中
    /// </summary>
    public bool isConnecing = false;

    /// <summary>
    /// 自动重连事件
    /// </summary>
    public int autoConnecSecond = 1000;
 

    public NetHandler.NetCoreCallBack netCoreCallBack = null;

    /// <summary>
    /// 接受字节
    /// </summary>
    public BytesUtils bytesUtils = new BytesUtils(new byte[0]);

    /// <summary>
    /// 设置回调
    /// </summary>
    /// <param name="netCoreBackData"></param>
    public virtual void setCallBack(NetCoreBackData netCoreBackData)
    {
        if (netCoreCallBack != null)
        {
            netCoreCallBack.Invoke(netCoreBackData);
        }
    }
}
