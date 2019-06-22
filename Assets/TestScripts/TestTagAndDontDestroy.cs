using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTagAndDontDestroy : MonoBehaviour
{
    public GameObject gb;
    // Start is called before the first frame update
    void Start()
    {
        var gb = GameObject.FindGameObjectWithTag("singletonRoot");
        if (gb == null)
        {
            gb = new GameObject("singletonRoot")
            {
                tag = "singletonRoot"
            };
            DontDestroyOnLoad(gb);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gb = GameObject.FindGameObjectWithTag("singletonRoot");
    }
}
