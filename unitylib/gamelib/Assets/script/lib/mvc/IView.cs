using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IView 
{
    /// <summary>
    /// View 层
    /// </summary>
    /// <param name="message"></param>
    void OnMessage(IMessage message);
}
