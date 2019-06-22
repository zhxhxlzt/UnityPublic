using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TestWeakRef : MonoBehaviour
{
    RefCall m_RefCall = new RefCall();
    public Transform testTrans;
    public Func<string, Transform> func;
    Test test1;
    Test test2;
    // Start is called before the first frame update
    void Start()
    {
        //test1 = new Test();
        //test2 = test1;
        //m_RefCall.AddFuncWeakRef(test1.Say);   
        func = testTrans.Find;
    }

    // Update is called once per frame
    void Update()
    {
        //m_RefCall.Call();
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    test1 = null;
        //}
        //test2.Say();
        if (Input.GetKeyDown(KeyCode.K))
        {
            Destroy(testTrans.gameObject);
            Debug.Log("Destroy");
        }

        func?.Invoke("hello");
        foreach (var e in func.GetInvocationList())
        {
            
        }
    }
    
    class Test
    {
        public System.Action func;
        public void Say() => Debug.Log("Hello world");
    }

}

public class RefCall
{
    WeakReference<System.Action> m_FuncWeakRef;

    public void AddFuncWeakRef( System.Action func )
    {
        m_FuncWeakRef = new WeakReference<System.Action>(func);
    }

    public void Call()
    {
        if (m_FuncWeakRef != null && m_FuncWeakRef.TryGetTarget(out System.Action func))
        {
            func();
        }
        else
        {
            Debug.Log("no func");
        }
    }
}
