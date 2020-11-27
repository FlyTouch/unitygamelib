using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 编码解码类
/// </summary>
public class BytesUtils:IDisposable
{
    /// <summary>
    /// 实体参数
    /// </summary>
    private byte[] array = null;

    /// <summary>
    /// 是否成功!
    /// </summary>
    public bool success = true;

    /// <summary>
    /// 消息ID
    /// </summary>
    private short msgId { get; set; }

    /// <summary>
    /// 消息头
    /// </summary>
    public byte msgHead = 0x09;


    /// <summary>
    /// 用于加锁
    /// </summary>
    private Hashtable hashtable = new Hashtable();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="array"></param>
    public BytesUtils(byte[] array)
    {
        this.array = array;
    }

    /// <summary>
    /// 构造
    /// </summary>
    public BytesUtils()
    {

    }

    /// <summary>
    /// 解包
    /// </summary>
    /// <returns></returns>
    public List<NetSerialize> DecodePackage(byte [] content)
    {
        List<NetSerialize> netSerializes = new List<NetSerialize>();
       //定义字节偏移，因为TCP可能出现粘包问题，所以务必要进行拆包
        lock (hashtable)  //因为可能解包的时候有新消息，所以务必将此缓冲Buff 加锁
        {
            //组合新数组
            using(MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(array, 0, array.Length); //写入历史包
                memoryStream.Write(content, 0, content.Length); //写入最新的包
                array = memoryStream.ToArray();  //这里就是最终的数据
                //memoryStream.Dispose();
            }
            //开始处理buff 数据
            while (true)  //循环解析消息
            {
                int offect = 0;
                var head = ReadByte(offect);
                if(head!= msgHead) //如果消息头不存在，就不对。
                {
                    break; 
                }
                offect += 1;
                NetSerialize serialize = new NetSerialize() {  msgId = ReadShort(offect) };
                offect += 2;
                int msgLen = ReadInt(offect); //读取消息长度
                offect += 4; //继续偏移
                serialize.content = ReadContent(offect,msgLen);
                offect += msgLen;
                netSerializes.Add(serialize);
                //将array 从新赋值。
                var buffer = new byte[array.Length - offect]; //这里就是剩余的包
                if(buffer.Length == 0) //如果是一个整包,就直接返回即可
                {
                    array = new byte[0];
                    break;
                }
                //否则，就将剩余包拷贝，等下下次处理
                Buffer.BlockCopy(array, offect, buffer, 0, buffer.Length); //剩余的包
                array = buffer;
            }
            return netSerializes;
        }
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="netId"></param>
    /// <param name="array"></param>
    public BytesUtils(short netId, byte[] array)
    {
        this.array = array;
        this.msgId = netId;
    }

    /// <summary>
    /// 转换
    /// </summary>
    /// <returns></returns>
    public byte[] toArray()
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            //写入消息头
            memoryStream.WriteByte(msgHead);
            //写入命令
            var netShort = BitConverter.GetBytes(msgId);
            memoryStream.Write(netShort, 0, netShort.Length);
            //写入字节长度
            var netLen = BitConverter.GetBytes(array.Length);
            memoryStream.Write(netLen, 0, netLen.Length);
            //写入消息体
            memoryStream.Write(array, 0, array.Length);
            return memoryStream.ToArray();
        }
    }

    /// <summary>
    /// 读取一个字节
    /// </summary>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public byte ReadByte(int startIndex = 0)
    {
        try
        {
            return array[startIndex];
        }
        catch (Exception e)
        {
            success = false;
            Console.WriteLine("ReadByte 数组越界!");
            return 0;
        }
    }

    /// <summary>
    /// 读取short
    /// </summary>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public short ReadShort(int startIndex = 0)
    {
        try
        {
            byte[] content = new byte[2];
            Buffer.BlockCopy(array, startIndex, content, 0, content.Length);
            return BitConverter.ToInt16(content, 0);
        }
        catch (Exception)
        {
            success = false;
            Console.WriteLine("ReadShort 数组越界!");
            return 0;
        }
    }

    /// <summary>
    /// 读取int
    /// </summary>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public int ReadInt(int startIndex = 0)
    {
        try
        {
            byte[] content = new byte[4];
            Buffer.BlockCopy(array, startIndex, content, 0, content.Length);
            return BitConverter.ToInt32(content,0);
        }
        catch (Exception)
        {
            success = false;
            return 0;
        }
    }

    /// <summary>
    /// 读消息
    /// </summary>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public byte[] ReadContent(int startIndex = 0,int length = 0)
    {
        byte[] content = new byte[length];
        try
        {
            Buffer.BlockCopy(array, startIndex, content, 0, length);
            return content;
        }
        catch (Exception e)
        {
            success = false;
            return content;
        }
    }

    public void Dispose()
    {
        array = null;
    }

    /// <summary>
    /// 消息编码
    /// </summary>
    public class NetSerialize
    {
        /// <summary>
        /// 消息头 0x07
        /// </summary>
        public byte head { get; set; }


        /// <summary>
        /// 消息号
        /// </summary>
        public short msgId { get; set; }


        /// <summary>
        /// 消息内容
        /// </summary>
        public byte[] content { get; set; }
    }
}
