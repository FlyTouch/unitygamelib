using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class NetWorkMgr : Manager
{

    /// <summary>
    /// tcp socket list 集合
    /// </summary>
    public List<TSock> tcpSockList = new List<TSock>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// 创建TCP对象接口
    /// </summary>
    /// <param name="config"></param>
    /// <param name="protocolType"></param>
    /// <param name="autoconnec"></param>
    /// <param name="autoConnecSecond"></param>
    /// <param name="bufferSize"></param>
    /// <returns></returns>
    public TSock CreateTcpSocket(TConfig config, ISocketMessage socketMessage, bool autoconnec = true, int autoConnecSecond = 1000, int bufferSize = 1024)
    {
        TSock sock = new TSock(config, socketMessage,  ProtocolType.Tcp, autoconnec,autoConnecSecond,bufferSize);
        var temp = tcpSockList.Find(m => m.tConfig.name.Equals(config.name));
        if (temp != null)
        {
            temp.Dispose();
            tcpSockList.Remove(temp);
        }
        tcpSockList.Add(sock);
        return sock;
    }


 
}
