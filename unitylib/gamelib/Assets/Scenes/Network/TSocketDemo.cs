using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSocketDemo : MonoBehaviour
{
    public string ip;

    public int port;

    TSock sock;
    // Start is called before the first frame update
    void Start()
    {

        sock = new TSock(new TConfig() { ip = ip, port = port });
        sock.AddHandler(delegate (NetCoreBackData netCoreBackData) {

            switch (netCoreBackData.sockType)
            {
                ///首次连接成功
                case SockType.ChannelRegistered:

                    break;
                //断线重连成功
                case SockType.ChannelResetRegistered:

                    break;
                //socket 断开
                case SockType.ChannelWillBreak:

                    break;
                //新消息
                case SockType.ChannelRead:

                    Debug.Log("接受到新消息了!");

                    break;
            }

        });
        sock.Start();
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (sock.isConnection)
            {
                byte[] hd = System.Text.Encoding.UTF8.GetBytes("你好呀");
                sock.Send(new TSendModel() {  msgId = 1, array = hd});
            }
        }
    }

    private void OnDestroy()
    {
        sock.Dispose();
    }
}
