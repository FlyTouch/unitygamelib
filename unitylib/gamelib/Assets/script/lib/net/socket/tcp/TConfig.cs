using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TConfig : NetConfig
{
    /// <summary>
    /// ip 地址
    /// </summary>
    public string ip { get; set; }

    /// <summary>
    /// 连接端口
    /// </summary>
    public int port { get; set; }

    /// <summary>
    /// socket 名称
    /// </summary>
    public string name { get; set; }
}
