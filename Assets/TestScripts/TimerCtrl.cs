using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCtrl : MonoBehaviour
{
    [Range(0.1f, 3)]public float timescale=1;
    public float deltatime;
    public float fixeddeltatime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timescale;
        deltatime = Time.deltaTime;
        fixeddeltatime = Time.fixedDeltaTime;
    }
}
