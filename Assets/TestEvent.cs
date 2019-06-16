using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestEvent : MonoBehaviour
{
    private EventMgr m_eventMgr;
    // Start is called before the first frame update
    public int sendCnt = 100;
    void Start()
    {
        m_eventMgr = Singleton.GetInstance<EventMgr>();
        m_eventMgr.RegisterEvent(EventType.Event_One, ShowMove);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < sendCnt; ++i ) {
            m_eventMgr.SendEvent(EventType.Event_One, new MoveData());
        }
        
    }

    void ShowMove(EventArgs args ) {
        MoveData data = args as MoveData;
        Debug.LogFormat("hero move: {0} {1}", data.x, data.y);
    }

    class MoveData : EventArgs {
        public float x = 1;
        public float y = 1;
        public MoveData() {
            x = UnityEngine.Random.Range(-1f, 1f);
            y = UnityEngine.Random.Range(-1f, 1f);
        }
    }
}
