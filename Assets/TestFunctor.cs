using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TestFunctor : MonoBehaviour
{
    Test test0;
    System.Action func;
    // Start is called before the first frame update
    void Start()
    {
        test0 = new Test();
        func = Functor.Wrap(test0.Say, "hello world!");
        
    }

    // Update is called once per frame
    void Update()
    {
        func();
        if (Input.GetKeyDown(KeyCode.K))
        {
            test0 = null;
        }
    }

    void Say(string words )
    {
        Debug.Log(words);
    }
    
    class Test
    {
        public void Say() => Debug.Log("Hello");
        public void Say( string words ) => Debug.Log(words);
    }

}
