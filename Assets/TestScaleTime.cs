using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScaleTime : MonoBehaviour
{
    [Range(0, 2)] public float scale = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = scale;
        transform.Rotate(new Vector3(1, 0, 0));
    }

    private void FixedUpdate()
    {
        
    }
}
