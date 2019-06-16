using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCountTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float t;
        var begin = Time.realtimeSinceStartup;
        for (int i = 0; i < 100; i++)
        {
            TestFunc();
        }
        Debug.Log("empty cycle time usage:" + (Time.realtimeSinceStartup - begin));
        
        begin = Time.realtimeSinceStartup;
        for (int i = 0; i < 100; i++)
        {
            TestFunc();
            t = Time.realtimeSinceStartup;
        }
        Debug.Log("count time cycle time usage:" + (Time.realtimeSinceStartup - begin));


    }

    void TestFunc()
    {
        for (int i = 0; i < 10000; i++) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
