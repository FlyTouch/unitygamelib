using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class SockProxy : INetWork
{
    public Socket socket;

    /// <summary>
    /// 配置数据
    /// </summary>
    public TConfig tConfig = null;

    /// <summary>
    /// 用于多线程的处理
    /// </summary>
    public Thread socketThread = null;

    /// <summary>
    /// 连接端口
    /// </summary>
    public IPEndPoint endPoint;

    /// <summary>
    /// socket 类型
    /// </summary>
    public ProtocolType protocolType = ProtocolType.Unknown;

    /// <summary>
    /// 定义的接收缓冲区大小
    /// </summary>
    public int bufferSize = 1024;

    /// <summary>
    /// 用于发送数据的SocketAsyncEventArgs
    /// </summary>
    public SocketAsyncEventArgs sendEventArg = null;


    /// <summary>
    /// 用于接收数据的SocketAsyncEventArgs
    /// </summary>
    public SocketAsyncEventArgs recvEventArg = null;


    /// <summary>
    /// 接受缓存数组
    /// </summary>
    private byte[] recvBuff = null;
    /// <summary>
    /// 发送缓存数组
    /// </summary>
    private byte[] sendBuff = null;


    public virtual void Dispose()
    {

    }

    public void InitLize(NetConfig netConfig)
    {
        tConfig = (TConfig)netConfig;
    }

    public virtual void Send(NetSendModel netSendModel)
    {

    }

    public virtual bool Start()
    {
        if(tConfig.ip.Length == 0) {

            Log("Ip 地址不能为空，暂时只支持 IPV4  配置IP地址为:{0}",tConfig.ip);
            return false;
        }
        //创建连接终点
        endPoint = new IPEndPoint(IPAddress.Parse(tConfig.ip),tConfig.port);
        switch (protocolType)
        {
            case ProtocolType.Tcp:
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                break;
            case ProtocolType.Udp:
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                break;
            default:
                Log("暂不支持 除TCP UDP 之外的连接，你选择的连接为:{0}", protocolType.ToString());
                return false;
        }
        sendBuff = new byte[bufferSize];
       
        recvBuff = new byte[bufferSize];
       

        Log("{0} Socket 创建成功! 准备多线程连接",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        return true;
    }


    /// <summary>
    /// 填充接收/发送对象实例
    /// </summary>
    public void FillSendAndRecvAsyncData(System.EventHandler<SocketAsyncEventArgs> eventHandler)
    {
        sendEventArg = new SocketAsyncEventArgs();
        sendEventArg.SetBuffer(sendBuff, 0, bufferSize);
        sendEventArg.Completed += eventHandler;
        recvEventArg = new SocketAsyncEventArgs();
        recvEventArg.SetBuffer(recvBuff, 0, bufferSize);
        recvEventArg.Completed += eventHandler;
    }

    /// <summary>
    /// 打印
    /// </summary>
    /// <param name="message"></param>
    /// <param name="param"></param>
    public void Log(string message,params object [] param)
    {
        Debug.Log(string.Format(message,param));
    }
}
