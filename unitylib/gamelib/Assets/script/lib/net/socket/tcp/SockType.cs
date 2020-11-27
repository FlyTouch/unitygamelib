using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BytesUtils;

/// <summary>
/// 回调类型
/// </summary>
public enum SockType : int
{
    ChannelActive = 1,
    ChannelRegistered = 2,
    ChannelUnregistered = 3,
    ChannelRead = 4,
    ChannelResetRegistered = 5,
    ChannelConnectionFail = 6,
    ChannelWillBreak = 7
}

/// <summary>
/// 返回的数据
/// </summary>
public class NetCoreBackData
{
    /// <summary>
    /// socket 回调类型
    /// </summary>
    public SockType sockType { get; set; }

    /// <summary>
    /// 整数,lua 调用
    /// </summary>
    public int _sockType { get { return System.Convert.ToInt32(sockType); } }

    /// <summary>
    /// 返回的消息体
    /// </summary>
    public NetSerialize netSerialize { get; set; }
}