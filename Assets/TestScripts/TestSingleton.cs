using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSingleton : MonoBehaviour
{
    public TestSingle m_Test1;
    public Rigidbody m_Test2;
    // Start is called before the first frame update
    void Start()
    {
        m_Test1 = Singleton.GetInstance<TestSingle>();
        m_Test2 = Singleton.GetInstance<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public class TestSingle
    {
        public int value=20;
        public void Say() => Debug.Log("My value is:" + value.ToString());
        private TestSingle() { }
    }
}



