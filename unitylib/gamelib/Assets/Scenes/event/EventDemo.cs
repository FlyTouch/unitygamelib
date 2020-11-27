using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.AddEvent("hd", delegate (object obj) {

            Debug.Log(obj);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventManager.Instance.TriggerEvent("hd","我很好的");
        }
        
    }
}
