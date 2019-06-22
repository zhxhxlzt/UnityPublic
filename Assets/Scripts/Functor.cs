using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class Functor
{
    public static Action Wrap<T0>( Action<T0> func, T0 t0 )
    {
        return delegate
        {
            func(t0);
        };
    }

    public static Action Wrap<T0, T1>( Action<T0, T1> func, T0 t0, T1 t1 )
    {
        return delegate
        {
            func(t0, t1);
        };
    }

    public static Action Wrap<T0, T1, T2>( Action<T0, T1, T2> func, T0 t0, T1 t1, T2 t2 )
    {
        return delegate
        {
            func(t0, t1, t2);
        };
    }

    public static Action Wrap<T0, T1, T2, T3>( Action<T0, T1, T2, T3> func, T0 t0, T1 t1, T2 t2, T3 t3 )
    {
        return delegate
        {
            func(t0, t1, t2, t3);
        };
    }
}


