using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLog : MonoBehaviour
{
    void Start()
    {
        Log.WriteLog("Test", "helloUnity");
        Debug.Log("Log sent (fire-and-forget).");
    }
    
}
