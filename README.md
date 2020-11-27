# unitygamelib
Unity部分底层框架，包含于网络框架(TCP/HTTP/UDP)，热更新框架，加载框架等，protobuff,lua, 

内部集成代码片段。以模块区分，具体模块介绍如下。本Git为本人在闲余时间整理，更新不及时敬请谅解，有什么问题可直接提问，
项目内容如下：

#1.网络部分

   1.TCP通信，服务器采用 NetCore Donetty 网络框架，客户端为异步逻辑，处理方面， 关于TCP的粘包，断线重连，移动端切后台处理，封装方式简单，
     基本几行代码就可以实现一个Tcp实例,
     
     协议指定方式:
     消息头:byte  0x09
     消息ID:short 
     消息长度:int
     消息体:自定义
     
     代码部分:TSock.cs  demo TcpDemo  启动后自动连接，按A键发送消息
     使用说明:
        
            创建socket 实例:
            NetWorkMgr networkMgr = AppFacade.Instance.GetManager<NetWorkMgr>(ManagerName.NetWorkMgr);
            TSock sock = networkMgr.CreateTcpSocket(config,socketMessage,autoconnec,autoConnecSecond,bufferSize)
            其他构造参数可选:
            /// <summary>
             /// 构造函数
             /// </summary>
             /// <param name="tConfig">Socket基础配置</param>
             /// <param name="protocolType">Socket 类型，默认为TCP</param>
             /// <param name="autoconnec">是否重连</param>
             /// <param name="autoConnecSecond">重连间隔</param>
             /// <param name="bufferSize">自定义的缓冲区大小，</param>
             /// <param name="ISocketMessage">自定义回调，</param>
            //添加socket 事件监听
             
            //启动socket
            sock.Start();
            
             //接口实现
             public void NetCoreCallBack(NetCoreBackData netCoreBackData)
             {
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
             }
            
            发送消息:
            //msgId 为消息ID  array 为字节数组
            sock.Send(new TSendModel() {  msgId = 1, array = array});
            
            注意，关闭游戏/编辑器请释放此socket 对象，调用方式
       
             private void OnDestroy()
             {
                 sock.Dispose();
             }
             
             
#2.定时器功能实现
              类:TimerDemo
              调用方式: 
                    TimerInfo timerInfo = new TimerInfo("TimerDemo", timerCommond,10); //参数: 定时器Name, 回调接口，定时器执行的总次数(为0代表无线循环)
                    TimerManager timerManager = AppFacade.Instance.GetManager<TimerManager>(ManagerName.Timer);
                    timerManager.AddTimerEvent(timerInfo);
        
   
        
