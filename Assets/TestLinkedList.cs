using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLinkedList : MonoBehaviour
{
    public LinkedList<int> link;
    // Start is called before the first frame update
    void Start()
    {
        var link1 = new LinkedList<int>();
        var link2 = new LinkedList<int>();
        var link3 = new LinkedList<int>();
        var list3 = new List<int>();
        for (int i = 0; i < 1000000; i++)
        {
            link1.AddLast(i);
            link2.AddLast(i);
            list3.Add(i);
        }

        var begin = Time.realtimeSinceStartup;
        //for (int i = 10000-1; i > -1; i--)
        //{
        //    link1.Remove(i);
        //}
        //Debug.Log("time usage: " + (Time.realtimeSinceStartup - begin).ToString());

        begin = Time.realtimeSinceStartup;
        for (int i = 0; i < 1000000; i++)
        {
            list3.RemoveAt(0);
        }
        Debug.Log("list time usage:" + (Time.realtimeSinceStartup - begin).ToString());



        begin = Time.realtimeSinceStartup;
        for (var it = link2.First; it != link2.Last;)
        {
            it.Value = it.Next.Value;
            link2.Remove(it.Next);
        }
        link2.RemoveLast();
        Debug.Log("time usage2: " + (Time.realtimeSinceStartup - begin).ToString());

        Debug.Log("link2 count:" + link2.Count);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
