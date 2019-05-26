using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonoSingleton : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Space) ) {
            Singleton.GetComInstance<TimerMgr>().AddTimer(1, new Functor(Say), 3, 1);
        }
    }

    void Say() => Debug.Log("Hello!!!");
}
