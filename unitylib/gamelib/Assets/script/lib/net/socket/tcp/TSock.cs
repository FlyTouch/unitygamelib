using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

public class TSock : TSocketClient 
{

    /// <summary>
    /// 构造函数，由于创建socket 实例配置
    /// </summary>
    /// <param name="tConfig"></param>
    /// <param name="autoconnec"></param>
    public TSock(TConfig tConfig, ProtocolType protocolType = ProtocolType.Tcp, bool autoconnec = true)
    {
        InitLize(tConfig);
        this.protocolType = protocolType;
        this.autoconnec = autoconnec;
    }

    /// <summary>
    /// 启动socket
    /// </summary>
    /// <returns></returns>
    public override bool Start()
    {
       var connstate = base.Start();
       if(connstate == false)
        {
            return false;
        }
        FillSendAndRecvAsyncData(IO_Completed);
        Action();
        return true;
    }


    /// <summary>
    /// 启动Socket 进程
    /// </summary>
    private void Action()
    {
        try
        {

            isConnecing = true;
            SocketAsyncEventArgs connectEventArg = new SocketAsyncEventArgs();
            connectEventArg.Completed += new System.EventHandler<SocketAsyncEventArgs>(ConnectEventArgs_Completed);
            connectEventArg.RemoteEndPoint = endPoint;
            bool willRaiseEvent = socket.ConnectAsync(connectEventArg);
            if (!willRaiseEvent)
            {
                ProcessConnect(connectEventArg);
            }
        }
        catch (System.Exception e)
        {
            Log("连接失败：ip = {0} ,port = {1}", tConfig.ip, tConfig.port);
            setCallBack(new NetCoreBackData() { sockType = isFirstConnec ? SockType.ChannelRegistered : SockType.ChannelResetRegistered });
        }
    }


    /// <summary>
    /// 连接事件
    /// </summary>
    /// <param name="socketAsyncEventArgs"></param>
    private void ConnectEventArgs_Completed(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
    {
        if(socketAsyncEventArgs.SocketError == SocketError.Success)
        {
            Log("连接服务器成功! {0}", isFirstConnec ? "首次连接" : "断线重连");
            setCallBack(new NetCoreBackData() {  sockType = isFirstConnec? SockType.ChannelRegistered: SockType.ChannelResetRegistered});
            isFirstConnec = true;
            isConnection = true;
            isConnecing = false;
            StartRecv();
        }
        else
        {
            isConnecing = false;
            Log("连接服务器失败! {0}", socketAsyncEventArgs.SocketError);
            setCallBack(new NetCoreBackData() {  sockType = SockType.ChannelConnectionFail});
            ResetConnection();
        }
    }

    /// <summary>
    /// 重连
    /// </summary>
    public void ResetConnection()
    {
        ///如果没有连接。就返回
        if (isConnecing == true)
        {
            return;
        }
        isConnecing = true;
        isConnection = false;
        Thread.Sleep(5000);
        if (socket != null) { socket.Close(); }
        Start();
    }

    /// <summary>
    /// 发送，接收完毕调用的函数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void IO_Completed(object sender, SocketAsyncEventArgs e)
    {
        switch (e.LastOperation)
        {
            case SocketAsyncOperation.Send:

                ProcessSend (e);

                break;

            case SocketAsyncOperation.Receive:

                ProcessReceive(e);

                break;
        }

    }

    /// <summary>
    /// 接收函数
    /// </summary>
    /// <param name="e"></param>
    private void ProcessReceive(SocketAsyncEventArgs e)
    {
        //如果接收正常
        if(e.SocketError == SocketError.Success && e.BytesTransferred > 0)
        {
            Log("嗨，BB, 你有新消息了 {0}",e.BytesTransferred);
            //否则，继续请求接收
            StartRecv();
        }
        //异常直接进行重连请求
        else 
        {
            Log("接收异常，请进行重连 {0}", e.SocketError);
            setCallBack(new NetCoreBackData() {  sockType = SockType.ChannelWillBreak });
            ResetConnection();
        }
    }


    /// <summary>
    /// 发送函数
    /// </summary>
    /// <param name="e"></param>
    private void ProcessSend(SocketAsyncEventArgs e)
    {
        //如果发送正常。代表发送成功!
        if(e.SocketError == SocketError.Success)
        {

        }
        else
        {
            Log("发送异常，请进行重连 {0}", e.SocketError);
            setCallBack(new NetCoreBackData() { sockType = SockType.ChannelWillBreak });
            ResetConnection();
        }
    }

    /// <summary>
    /// 连接成功响应事件
    /// </summary>
    /// <param name="e"></param>
    private void ProcessConnect(SocketAsyncEventArgs e)
    {
        StartRecv();
    }


    /// <summary>
    /// 开始准备接受服务器消息
    /// </summary>
    private void StartRecv()
    {
        bool willRaiseEvent = socket.ReceiveAsync(recvEventArg);
        if (!willRaiseEvent)
        {
            ProcessReceive(recvEventArg);
        }
    }

    /// <summary>
    /// 发送消息给服务器
    /// </summary>
    /// <param name="netSendModel"></param>
    public override void Send(NetSendModel netSendModel)
    {
        TSendModel sendModel = (TSendModel)netSendModel;
        BytesUtils bytesUtils = new BytesUtils(sendModel.msgId, sendModel.array);
        var buff = bytesUtils.toArray();
        sendEventArg.SetBuffer(buff, 0, buff.Length);
        bool willRaiseEvent = socket.SendAsync(sendEventArg);
        if (!willRaiseEvent)
        {
            ProcessSend(sendEventArg);
        }
    }
}
