using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestTimer : MonoBehaviour
{
    public int count = 0;

    System.Action func;

    private void Start()
    {
        for (int i = 0; i < 100000; i++)
        {
            float delay = Random.Range(0.02f, 100);
            int repeat = Random.Range(-1, 10);
            Timer.AddTimer(delay, () => { count++; }, repeat, delay);
        }
    }

    private void TestCall() => count++;


}
