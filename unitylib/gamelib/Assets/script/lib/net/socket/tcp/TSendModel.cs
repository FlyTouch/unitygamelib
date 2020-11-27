using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSendModel : NetSendModel
{
    /// <summary>
    /// 消息ID
    /// </summary>
    public short msgId { get; set; }
    
    /// <summary>
    /// 发送字节流
    /// </summary>
    public byte [] array { get; set; }
}
