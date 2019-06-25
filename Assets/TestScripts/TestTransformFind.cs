using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTransformFind : MonoBehaviour
{
    public Transform light1;
    public string tarName = "Point Light";
    public Object target;
    private void Awake()
    {
        Time.timeScale = 2;
        light1 = transform.FindDownNode("Point Light");
    }

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            target = transform.FindDownNode(tarName);
        }
    }
}
