using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed  class Singleton  
{
    private static Singleton _instance = null;
    private static readonly object SynObject = new object();

    Singleton()
    {
    }

    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static Singleton Instance
    {
        get
        {
            // Syn operation.
            lock (SynObject)
            {
                return _instance ?? (_instance = new Singleton());
            }
        }
    }
}
