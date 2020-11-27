# unitygamelib
Unity部分底层框架，包含于网络框架(TCP/HTTP/UDP)，热更新框架，加载框架等，protobuff,lua, 

内部集成代码片段。以模块区分，具体模块介绍如下。本Git为本人在闲余时间整理，更新不及时敬请谅解，有什么问题可直接提问，
项目内容如下：

#1.网络部分

   1.TCP通信，服务器采用 NetCore Donetty 网络框架，客户端为异步逻辑，处理方面， 关于TCP的粘包，断线重连，移动端切后台处理，封装方式简单，
     基本几行代码就可以实现一个Tcp实例
     代码部分:TSock.cs  demo TSocketDemo.cs  启动后自动连接，按A键发送消息
     使用说明:
        
            创建socket 实例:
            TSock sock =  new TSock("127.0.0.1",9000);
            //添加socket 事件监听
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

                       break;
               }

            });       
            //启动socket
            sock.Start();
            
       注意，关闭游戏/编辑器请释放此socket 对象，调用方式
       
             private void OnDestroy()
             {
                 sock.Dispose();
             }

