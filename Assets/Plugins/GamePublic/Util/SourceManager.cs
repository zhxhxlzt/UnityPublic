using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SourceManager
{
    public static Transform LoadResource( string path )
    {
        return Resources.Load<GameObject>(path).transform;
    }
}
