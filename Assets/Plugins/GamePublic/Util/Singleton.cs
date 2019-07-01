using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 单例管理类，如果想使用某类唯一的单例，则通过此类提供的方法获取实例
/// </summary>
public class Singleton
{
    private static Dictionary<Type, object> m_singletons = new Dictionary<Type, object>();
    private static GameObject m_root;

    Singleton() { }

    public static T GetInstance<T>() where T : class
    {
        var type = typeof(T);

        // 继承MonoBehaviour的单例组件
        if (type.IsSubclassOf(typeof(MonoBehaviour)))
        {
            if (m_root == null)
            {
                m_root = new GameObject("SingletonComponentRoot");
                GameObject.DontDestroyOnLoad(m_root);
            }
            var com = m_root.GetComponent(type);
            if (com == null)
            {
                com = m_root.AddComponent(type);
            }
            return com as T;
        }

        // 非组件类单例
        if (!m_singletons.TryGetValue(type, out object singleton))
        {
            singleton = Activator.CreateInstance(type, true);
            m_singletons.Add(type, singleton);
        }

        return (T)singleton;
    }

    public static void Clear()
    {
        GameObject.Destroy(m_root);
        m_singletons.Clear();
    }
}

