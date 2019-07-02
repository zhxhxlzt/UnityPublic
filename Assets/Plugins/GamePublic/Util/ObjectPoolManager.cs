using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPoolManager
{
    private static Dictionary<Transform, Stack<Transform>> m_vacantPool = new Dictionary<Transform, Stack<Transform>>();
    private static Dictionary<Transform, Transform> m_usedPool = new Dictionary<Transform, Transform>();
    public static Transform Clone( Transform trans )
    {
        if (!m_vacantPool.TryGetValue(trans, out var pool))
        {
            pool = new Stack<Transform>();
            m_vacantPool.Add(trans, pool);
        }
        if (pool.Count == 0)
        {
            //pool.Push(Instantiate(trans));
        }
        var obj = pool.Pop();
        obj.gameObject.SetActive(true);
        m_usedPool.Add(obj, trans);
        return obj;
    }
    public static Transform Clone( string path )
    {
        return null;
    }
    public static bool Recycle(Transform trans)
    {
        trans.gameObject.SetActive(false);
        if (m_usedPool.TryGetValue(trans, out var p))
        {
            m_vacantPool[p].Push(trans);
            m_usedPool.Remove(trans);
            return true;
        }
        return false;
    }
}
