using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMessage 
{
    string Name { get; }

    object Body { get; set; }

    string Type { get; set; }

    string ToString();
}
