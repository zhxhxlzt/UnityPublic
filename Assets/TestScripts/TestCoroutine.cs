using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    ObjCoroutine oo;
    // Start is called before the first frame update
    void Start()
    {
        oo = new ObjCoroutine();
    }

    // Update is called once per frame
    void Update()
    {
    }
}


public class ObjCoroutine : ObjectBehaviour
{
    public Coroutine ct;
    public IEnumerator t;
    protected override void Awake()
    {
        Debug.Log("Born");
        t = Say();

        ct = StartCoroutine(t);
    }

    public IEnumerator Say()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("Hello Coroutine" + Time.time);
        }
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StopCoroutine(t);
        }
    }

}

public class TestCo
{
    public IEnumerator Test()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Hello co");
        }

    }
}
