using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimer : MonoBehaviour
{
    public TimerMgr m_timerMgr;
    public Timer timer1;
    // Start is called before the first frame update
    void Start()
    {
        m_timerMgr.AddTimer(1, new Functor(InstantiateCube));
        timer1 = m_timerMgr.AddTimer(2, new Functor<string>(SayHello, "hello" + Time.deltaTime), -1, 1);
        m_timerMgr.AddTimer(5, new Functor(delegate { timer1.Discard(); }));
    }
    
    void InstantiateCube() {
        Debug.Log("创建Cube");
        GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    void SayHello( string words ) {
        Debug.Log("Say Words: " + words);
    }
}
