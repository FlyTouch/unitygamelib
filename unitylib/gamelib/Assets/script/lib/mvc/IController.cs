using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController 
{
    /// <summary>
    /// 注册Controller 事件
    /// </summary>
    /// <param name="messageName"></param>
    /// <param name="commandType"></param>
    void RegisterCommand(string messageName, Type commandType);

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="view"></param>
    /// <param name="commandNames"></param>
    void RegisterViewCommand(IView view, string[] commandNames);

    /// <summary>
    /// 执行事件
    /// </summary>
    /// <param name="message"></param>
    void ExecuteCommand(IMessage message);

    /// <summary>
    ///移除事件
    /// </summary>
    /// <param name="messageName"></param>
    void RemoveCommand(string messageName);

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="view"></param>
    /// <param name="commandNames"></param>
    void RemoveViewCommand(IView view, string[] commandNames);

    /// <summary>
    /// 是否含有
    /// </summary>
    /// <param name="messageName"></param>
    /// <returns></returns>
    bool HasCommand(string messageName);
}
