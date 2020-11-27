using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 编码解码类
/// </summary>
public class BytesUtils
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
    public byte[] ReadContent(int startIndex = 0)
    {
        byte[] content = new byte[array.Length - startIndex];
        try
        {
            Buffer.BlockCopy(array, startIndex, content, 0, content.Length);
            return content;
        }
        catch (Exception e)
        {
            success = false;
            return content;
        }
    }


}
