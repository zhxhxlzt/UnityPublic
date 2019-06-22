using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ScriptEventArgs : EventArgs
{
    public new static readonly ScriptEventArgs Empty = new ScriptEventArgs();
    public EventType eventType;
}

public class EventArg<T> : ScriptEventArgs
{
    public T arg;
    public EventArg( T arg ) { this.arg = arg; }
}

public class EventArg<T0, T1> : ScriptEventArgs
{
    public T0 arg0;
    public T1 arg1;
    public EventArg( T0 arg0, T1 arg1 ) { this.arg0 = arg0; this.arg1 = arg1; }
}

public class EventArg<T0, T1, T2> : ScriptEventArgs
{
    public T0 arg0;
    public T1 arg1;
    public T2 arg2;
    public EventArg( T0 arg0, T1 arg1, T2 arg2 )
    {
        this.arg0 = arg0;
        this.arg1 = arg1;
        this.arg2 = arg2;
    }
}

public class HeroMoveEventArgs : ScriptEventArgs
{
    public Vector3 pos;
    public Vector3 velocity;
}
